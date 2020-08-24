using Laboratory.API.Business.Interfaces;
using Laboratory.API.Business.Models;
using Laboratory.API.Data;
using Laboratory.API.Data.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Dynamic.Core;
using Laboratory.API.Business.Models.Lab;

namespace Laboratory.API.Business
{
    public class LabService : ILabService
    {
        private IConfiguration _config;

        public LabService(IConfiguration config)
        {
            _config = config;
        }

        public PaginatedList<Lab> GetAllPaginatedWithDetailBySearchFilter(LabSearchFilter searchFilter)
        {
            PaginatedList<Lab> resultList = new PaginatedList<Lab>(new List<Lab>(), 0, searchFilter.CurrentPage, searchFilter.PageSize, searchFilter.SortOn, searchFilter.SortDirection);

            using (AppDBContext dbContext = new AppDBContext(_config))
            {
                var query = from a in dbContext.Lab
                           
                            select a;

                // filtering
                if (!string.IsNullOrEmpty(searchFilter.Filter_Name))
                {
                    query = query.Where(r => r.Name.Contains(searchFilter.Filter_Name));
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


                resultList = new PaginatedList<Lab>(
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

        public List<Lab> GetAll()
        {
            List<Lab> resultList = new List<Lab>();
            using (AppDBContext dbContext = new AppDBContext(_config))
            {
                resultList.AddRange(dbContext.Lab.AsNoTracking().ToList());
            }
            return resultList;
        }

        public Lab GetById(int id)
        {
            Lab result = null;

            using (AppDBContext dbContext = new AppDBContext(_config))
            {
                result = dbContext.Lab.Where(a => a.Id == id ).AsNoTracking().SingleOrDefault();
            }

            return result;
        }

        public int Add(Lab record)
        {
            int result = 0;
            record.CurrentApplianceCapacity = 0;
            using (AppDBContext dbContext = new AppDBContext(_config))
            {
                dbContext.Entry(record).State = EntityState.Added;
                result = dbContext.SaveChanges();
            }

            return result;
        }

        public int Update(Lab record)
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
