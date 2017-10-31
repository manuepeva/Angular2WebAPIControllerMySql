using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EmployeeDataAccess;

namespace EmployeeService
{
    public class EmployeeSecurity
    {
        public static bool Login(string username, string password)
        {
            using (mydbEntities entities = new mydbEntities())
            {
                return entities.Users1.Any(user => user.UserName.Equals(username,
                    StringComparison.OrdinalIgnoreCase) && user.Password == password);
            }
        }
    }
}