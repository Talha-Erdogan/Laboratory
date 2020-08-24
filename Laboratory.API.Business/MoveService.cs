using Laboratory.API.Business.Interfaces;
using Laboratory.API.Business.Models;
using Laboratory.API.Business.Models.Move;
using Laboratory.API.Data;
using Laboratory.API.Data.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Dynamic.Core;

namespace Laboratory.API.Business
{
    public class MoveService : IMoveService
    {
        private IConfiguration _config;

        public MoveService(IConfiguration config)
        {
            _config = config;
        }

        public PaginatedList<MoveWithDetail> GetAllPaginatedWithDetailBySearchFilter(MoveSearchFilter searchFilter)
        {
            PaginatedList<MoveWithDetail> resultList = new PaginatedList<MoveWithDetail>(new List<MoveWithDetail>(), 0, searchFilter.CurrentPage, searchFilter.PageSize, searchFilter.SortOn, searchFilter.SortDirection);

            using (AppDBContext dbContext = new AppDBContext(_config))
            {
                var query = from m in dbContext.Move
                            from a in dbContext.Appliance.Where(x => x.Id == m.ApplianceId).DefaultIfEmpty()
                            from l in dbContext.Lab.Where(x => x.Id == m.LabId).DefaultIfEmpty()
                            select new MoveWithDetail()
                            {
                                Id = m.Id,
                                LabId = m.LabId,
                                ApplianceId = m.ApplianceId,
                                EntranceDate = m.EntranceDate,
                                ExitDate = m.ExitDate,

                                Appliance_Name = a == null ? String.Empty : a.Name,
                                Lab_Name = l == null ? String.Empty : l.Name
                            };

                // filtering
                if (!string.IsNullOrEmpty(searchFilter.Filter_Appliance_Name))
                {
                    query = query.Where(r => r.Appliance_Name.Contains(searchFilter.Filter_Appliance_Name));
                }

                if (!string.IsNullOrEmpty(searchFilter.Filter_Lab_Name))
                {
                    query = query.Where(r => r.Lab_Name.Contains(searchFilter.Filter_Lab_Name));
                }

                // asnotracking
                query = query.AsNoTracking();

                //total count
                var totalCount = query.Count();

                //sorting
                if (!string.IsNullOrEmpty(searchFilter.SortOn))
                {
                    // using System.Linq.Dynamic.Core; nuget paketi ve namespace eklenmelidir, dynamic order by yapmak icindir
                    query = query.OrderBy(searchFilter.SortOn + " " + searchFilter.SortDirection.ToUpper());
                }
                else
                {
                    // deefault sıralama vermek gerekiyor yoksa skip metodu hata veriyor ef 6'da -- 28.10.2019 15:40
                    // https://stackoverflow.com/questions/3437178/the-method-skip-is-only-supported-for-sorted-input-in-linq-to-entities
                    query = query.OrderBy(r => r.Id);
                }

                //paging
                query = query.Skip((searchFilter.CurrentPage - 1) * searchFilter.PageSize).Take(searchFilter.PageSize);


                resultList = new PaginatedList<MoveWithDetail>(
                    query.ToList(),
                    totalCount,
                    searchFilter.CurrentPage,
                    searchFilter.PageSize,
                    searchFilter.SortOn,
                    searchFilter.SortDirection
                    );
            }

            return resultList;
        }

        public List<Move> GetAll()
        {
            List<Move> resultList = new List<Move>();
            using (AppDBContext dbContext = new AppDBContext(_config))
            {
                resultList.AddRange(dbContext.Move.AsNoTracking().ToList());
            }
            return resultList;
        }

        public Move GetById(int id)
        {
            Move result = null;

            using (AppDBContext dbContext = new AppDBContext(_config))
            {
                result = dbContext.Move.Where(a => a.Id == id).AsNoTracking().SingleOrDefault();
            }

            return result;
        }

        public MoveWithDetail GetByIdWithDetail(int id)
        {
            MoveWithDetail result = null;

            using (AppDBContext dbContext = new AppDBContext(_config))
            {
                var query = from m in dbContext.Move
                            from a in dbContext.Appliance.Where(x => x.Id == m.ApplianceId).DefaultIfEmpty()
                            from l in dbContext.Lab.Where(x => x.Id == m.LabId).DefaultIfEmpty()
                            select new MoveWithDetail()
                            {
                                Id = m.Id,
                                LabId = m.LabId,
                                ApplianceId = m.ApplianceId,
                                EntranceDate = m.EntranceDate,
                                ExitDate = m.ExitDate,

                                Appliance_Name = a == null ? String.Empty : a.Name,
                                Lab_Name = l == null ? String.Empty : l.Name
                            };

                result = query.AsNoTracking().FirstOrDefault();
            }

            return result;
        }

        public int Add(Move record)
        {
            int result = 0;
            using (AppDBContext dbContext = new AppDBContext(_config))
            {
                dbContext.Entry(record).State = EntityState.Added;
                result = dbContext.SaveChanges();
            }

            return result;
        }

        public int Update(Move record)
        {
            int result = 0;

            using (AppDBContext dbContext = new AppDBContext(_config))
            {
                dbContext.Entry(record).State = EntityState.Modified;
                result = dbContext.SaveChanges();
            }

            return result;
        }
    }
}
