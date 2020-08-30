﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Models;

namespace StockMarket.UserService.Repositories
{
    public class CompanyRepository : IRepository<Company>
    {
        private StockMarketContext context;

        public CompanyRepository(StockMarketContext context)
        {
            this.context = context;
        }
        public bool Add(Company entity)
        {
            try
            {
                //insert 
                context.Companies.Add(entity);
                int updates = context.SaveChanges();
                if (updates > 0)
                {
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Delete(Company entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Company> Get()
        {
            var companies = context.Companies;
            return companies;
        }

        public Company Get(object key)
        {
            throw new NotImplementedException();
        }

        public bool Update(Company entity)
        {
            throw new NotImplementedException();
        }
    }
}
