﻿using NOIAcountingVersion2.ModelClasses;
using System.Collections.Generic;

namespace NOIAcountingVersion2.DalClasses
{
    public interface ITransactionDAL
    {
        bool CreateTransaction(Transaction t,  User u);

        List<Transaction> GetCompanyTransactions(User u);

        List<Transaction> GetCompanyExpenses(User u);

        List<Transaction> GetCompanyRevenue(User u);

        List<Transaction> GetExpensesForTimePeriod(User u, TimePeriod t);

        List<Transaction> GetRevenueForTimePeriod(User u, TimePeriod t);
    }
}