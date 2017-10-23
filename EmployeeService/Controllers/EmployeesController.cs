using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using EmployeeDataAccess;

namespace EmployeeService.Controllers
{
    public class EmployeesController : ApiController
    {
        public IEnumerable<EMPLOYEESTABLE> Get()  
        {
            using(mydbEntities entities = new mydbEntities())
            {
                return entities.EMPLOYEESTABLEs.ToList();
            }
        }

        public EMPLOYEESTABLE Get(int id)
        {
            using(mydbEntities entities = new mydbEntities())
            {
                return entities.EMPLOYEESTABLEs.FirstOrDefault(e => e.ID == id);
            }
        }
    }
}
