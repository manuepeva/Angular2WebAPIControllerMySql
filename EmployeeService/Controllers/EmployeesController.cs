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

        public HttpResponseMessage Get(int id)
        {
            using(mydbEntities entities = new mydbEntities())
            {
                var entity =  entities.EMPLOYEESTABLEs.FirstOrDefault(e => e.ID == id);

                if(entity != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, entity);
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Employee web Id = " + id.ToString() + "not found");
                }
            }
        }

        public HttpResponseMessage Post([FromBody] EMPLOYEESTABLE employee)
        {
            try
            {
                using (mydbEntities entities = new mydbEntities())
                {
                    entities.EMPLOYEESTABLEs.Add(employee);
                    entities.SaveChanges();

                    var message = Request.CreateResponse(HttpStatusCode.Created, employee);
                    message.Headers.Location = new Uri(Request.RequestUri + employee.ID.ToString());
                    return message;
                }
            }
            catch(Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
                
            }
        }
    }
}
