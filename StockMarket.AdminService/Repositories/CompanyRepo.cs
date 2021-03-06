﻿using Microsoft.EntityFrameworkCore;
using Models;
using StockMarket.AdminService.Data;
using StockMarket.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockMarket.AdminService.Repositories
{
    public class CompanyRepo : ICompanyRepo<CompanyDto>
    {
        private AdminContext context;

        public CompanyRepo(AdminContext context)
        {
            this.context = context;
        }

        public bool Add(CompanyDto entity)
        {
            try
            {
                var company = new Company
                {
                    //Id = entity.Id,
                    Companyname = entity.Companyname,
                    Turnover = entity.Turnover,
                    Ceo = entity.Ceo,
                    Boardofdirectors = entity.Boardofdirectors,
                    Brief = entity.Brief,
                    //CompanyStockExchanges = new List<CompanyStockExchange>()
                };
                //context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [dbo].[company] ON ");
                context.Add(company);
                int updates = context.SaveChanges();
                if (updates == 0)
                {
                    return false;
                }

                var companyId = context.Companies.Where(c => c.Companyname == entity.Companyname).Select(c=>c.Id).ToList()[0];

                foreach (var exchangeId in entity.StockExchangeIds)
                {
                    context.CompanyStockExchanges.Add(new CompanyStockExchange() { CompanyId = companyId, StockExchangeId = exchangeId });
                }
                int updates2 = context.SaveChanges();
                if(updates2 > 0)
                {
                    return true;
                }
                context.Remove(company);
                context.SaveChanges();
                return false;
            }
            catch(Exception)
            {
                return false;
            }
        }

        public bool Delete(long key)
        {
            try
            {
                var company = context.Companies.Find(key);
                foreach (var companyStockExchange in context.CompanyStockExchanges.Where(c => c.CompanyId == key))
                {
                    context.CompanyStockExchanges.Remove(companyStockExchange);
                }
                context.SaveChanges();
                context.Companies.Remove(company);
                int updates = context.SaveChanges();
                if(updates==0)
                {
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
                //return false;
            }
        }

        public IEnumerable<CompanyDto> Get()
        {
            var companies = new List<CompanyDto>();
            foreach (var company in this.context.Companies)
            {
                CompanyDto companyDto = new CompanyDto
                {
                    Id = company.Id,
                    Companyname = company.Companyname,
                    Turnover = company.Turnover,
                    Ceo = company.Ceo,
                    Boardofdirectors = company.Boardofdirectors,
                    Brief = company.Brief,
                    StockExchangeIds = new List<string>()
                };
                foreach(var companyStockExchange in context.CompanyStockExchanges.Where(c=>c.CompanyId==company.Id))
                {
                    companyDto.StockExchangeIds.Add(companyStockExchange.StockExchangeId);
                }
                companies.Add(companyDto);
            }
            return companies;
        }

        public CompanyDto Get(object key)
        {
            var company = this.context.Companies.Find(key);
            CompanyDto companyDto = new CompanyDto
            {
                Id = company.Id,
                Companyname = company.Companyname,
                Turnover = company.Turnover,
                Ceo = company.Ceo,
                Boardofdirectors = company.Boardofdirectors,
                Brief = company.Brief,
                StockExchangeIds = new List<string>()
            };
            foreach (var companyStockExchange in context.CompanyStockExchanges.Where(c=>c.CompanyId==company.Id))
            {
                companyDto.StockExchangeIds.Add(companyStockExchange.StockExchangeId);
            }
            return companyDto;
        }

        public IEnumerable<string> GetNames()
        {
            var companies = new List<string>();
            foreach( var company in context.Companies)
            {
                companies.Add(company.Companyname);
            }
            return companies;
        }

        public IEnumerable<CompanyDto> GetMatching(string name)
        {
            var companies = this.context.Companies.Where(c => c.Companyname.Contains(name));
            List<CompanyDto> companyDtos = new List<CompanyDto>();           
            foreach (var company in companies)
            {
                CompanyDto companyDto = new CompanyDto
                {
                    Id = company.Id,
                    Companyname = company.Companyname,
                    Turnover = company.Turnover,
                    Ceo = company.Ceo,
                    Boardofdirectors = company.Boardofdirectors,
                    Brief = company.Brief,
                    StockExchangeIds = new List<string>()
                };
                foreach (var companyStockExchange in context.CompanyStockExchanges.Where(c=>c.CompanyId==company.Id))
                {
                    companyDto.StockExchangeIds.Add(companyStockExchange.StockExchangeId);
                }
                companyDtos.Add(companyDto);
            }
            return companyDtos;
        }

        public bool Update(CompanyDto entity)
        {
            try
            {
                Company company = context.Companies.Find(entity.Id);
                this.context.Entry(company).CurrentValues.SetValues(entity);
                context.SaveChanges();
                return true;
            }
            catch(Exception)
            {
                return false;
            }
        }
    }
}
