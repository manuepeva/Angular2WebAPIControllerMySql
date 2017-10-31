using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using EmployeeDataAccess;
using System.Web.Http.Cors;
using System.Threading;

namespace EmployeeService.Controllers
{
    [EnableCorsAttribute("*", "*", "*")]
    public class EmployeesController : ApiController
    {
        [BasicAuthetication]
        public HttpResponseMessage Get(string gender="All")  
        {
            string username = Thread.CurrentPrincipal.Identity.Name;

            using(mydbEntities entities = new mydbEntities())
            {
                switch (username.ToLower())
                {

                    case "male":
                        return Request.CreateResponse(HttpStatusCode.OK,
                            entities.EMPLOYEESTABLEs.Where(e => e.GENDER.ToLower() =="male").ToList());
                    case "female":
                        return Request.CreateResponse(HttpStatusCode.OK,
                            entities.EMPLOYEESTABLEs.Where(e => e.GENDER.ToLower() == "female").ToList());
                    default:
                        return Request.CreateResponse(HttpStatusCode.BadRequest);
                }
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

        public HttpResponseMessage Delete(int id)
        {
            try
            {
                using (mydbEntities entities = new mydbEntities())
                {
                    var entity = entities.EMPLOYEESTABLEs.FirstOrDefault(e => e.ID == id);
                    if (entity == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Employee with ID = "
                            + id.ToString() + " not found to delete");
                    }
                    else
                    {
                        entities.EMPLOYEESTABLEs.Remove(entity);
                        entities.SaveChanges();
                        return Request.CreateResponse(HttpStatusCode.OK);
                    }
                }
            }
            catch(Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        public HttpResponseMessage Put([FromBody]int id, [FromUri]EMPLOYEESTABLE employee)
        {
            try {
                using (mydbEntities entities = new mydbEntities())
                {
                    var entity = entities.EMPLOYEESTABLEs.FirstOrDefault(e => e.ID == id);

                    if (entity == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Employee with ID = "
                            + id.ToString() + "not found to update");
                    }
                    else
                    {
                        entity.FIRST_NAME = employee.FIRST_NAME;
                        entity.LAST_NAME = employee.LAST_NAME;
                        entity.GENDER = employee.GENDER;
                        entity.SALARY = employee.SALARY;

                        entities.SaveChanges();
                        return Request.CreateResponse(HttpStatusCode.OK, entity);
                    }
                }
              }
            catch(Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
            }
    }
}
