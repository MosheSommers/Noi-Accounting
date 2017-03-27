using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NOIAcountingVersion2.ModelClasses;

namespace NOIAcountingVersion2.DalClasses
{
    public interface IUserDal
    {
        bool CreateNewUser(User u);

        bool ChangeUserName(User u);
    }
}
