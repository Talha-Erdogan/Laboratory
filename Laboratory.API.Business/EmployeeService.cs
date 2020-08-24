using Laboratory.API.Business.Interfaces;
using Laboratory.API.Business.Models;
using Laboratory.API.Business.Models.Employee;
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
    public class EmployeeService : IEmployeeService
    {
        private IConfiguration _config;

        public EmployeeService(IConfiguration config)
        {
            _config = config;
        }

        public PaginatedList<Employee> GetAllPaginatedWithDetailBySearchFilter(EmployeeSearchFilter searchFilter)
        {
            PaginatedList<Employee> resultList = new PaginatedList<Employee>(new List<Employee>(), 0, searchFilter.CurrentPage, searchFilter.PageSize, searchFilter.SortOn, searchFilter.SortDirection);

            using (AppDBContext dbContext = new AppDBContext(_config))
            {
                var query = from e in dbContext.Employee
                            select e;

                // filtering
                if (!string.IsNullOrEmpty(searchFilter.Filter_Name))
                {
                    query = query.Where(r => r.Name.Contains(searchFilter.Filter_Name));
                }
                if (!string.IsNullOrEmpty(searchFilter.Filter_LastName))
                {
                    query = query.Where(r => r.LastName.Contains(searchFilter.Filter_LastName));
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


                resultList = new PaginatedList<Employee>(
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

        public Employee GetByUserNameAndPassword(string userName, string password)
        {
            Employee result = null;
            using (AppDBContext dbContext = new AppDBContext(_config))
            {
                var query = from e in dbContext.Employee
                            where e.UserName == userName && e.Password == password
                            select e;

                result = query.AsNoTracking().FirstOrDefault();
            }
            return result;
        }

        public Employee GetById(int id)
        {
            Employee result = null;

            using (AppDBContext dbContext = new AppDBContext(_config))
            {
                result = dbContext.Employee.Where(a => a.Id == id).AsNoTracking().SingleOrDefault();
            }

            return result;
        }

        public int Add(Data.Entity.Employee record)
        {
            int result = 0;
            using (AppDBContext dbContext = new AppDBContext(_config))
            {
                dbContext.Entry(record).State = EntityState.Added;
                result = dbContext.SaveChanges();
            }
            return result;
        }

        public int Update(Data.Entity.Employee record)
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
