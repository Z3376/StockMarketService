﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockMarket.AccountService.NewFolder1
{
    public interface IAccountRepository<T>
    {
        bool Signup(T entity);
        Tuple<bool, string> Login(string username, string password);
        bool Logout();
    }
}
