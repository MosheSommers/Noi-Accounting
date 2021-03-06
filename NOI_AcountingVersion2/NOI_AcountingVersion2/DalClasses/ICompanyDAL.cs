﻿using NOIAcountingVersion2.ModelClasses;

namespace NOIAcountingVersion2.DalClasses
{
    public interface ICompanyDAL
    {
        bool CreateNewCompany(Company c);

        bool ChangeCompanyName(Company c, string newName);

        bool ChangeCompanyPassword(Company c, string newPassword);
    }
}