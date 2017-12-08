using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using EmployeeWebAPI.DAL;
using EmployeeWebAPI.Models;

namespace EmployeeWebAPI.Controllers
{
    public class PeopleController : ApiController
    {
        private EmployeeContext db = new EmployeeContext();
        Repository<Employee> repo = new Repository<Employee>();

        public IEnumerable<EmployeeViewModel> GetPerson()
        {

            List<EmployeeViewModel> emps = new List<EmployeeViewModel>();

            try
            {
                var model = repo.GetAll();
                if (model != null)
                {
                    foreach (var item in model)
                    {
                        emps.Add(new EmployeeViewModel
                        {
                            PersonId = item.PersonId,
                            FirstName = item.Person.FirstName,
                            LastName = item.Person.LastName,
                            BirthDate = item.Person.BirthDate,
                            EmployeeId = item.EmployeeId,
                            EmployeeNum = item.EmployeeNum,
                            EmployeeDate = item.EmployeeDate,
                            Terminated = item.Terminated

                        });
                    }
                }
            }
            catch(Exception ex)
            {
                ex.Message.ToString();
            }      
            return emps.ToList();
        }

        // GET: api/People/5
        [ResponseType(typeof(EmployeeViewModel))]
        public IHttpActionResult GetPerson(int id)
        {
            EmployeeViewModel employee = new EmployeeViewModel();
            try
            {
                var model = repo.GetById(id);
                if(model != null)
                {
                    employee.PersonId = model.Person.PersonId;
                    employee.FirstName = model.Person.FirstName;
                    employee.LastName = model.Person.LastName;
                    employee.BirthDate = model.Person.BirthDate;
                    employee.EmployeeDate = model.EmployeeDate;
                    employee.EmployeeId = model.EmployeeId;
                    employee.EmployeeNum = model.EmployeeNum;
                    if(model.Terminated != null)
                    {
                        employee.Terminated = model.Terminated;
                    }
                }                            
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
            if (employee == null)
            {
                return NotFound();
            }

            return Ok(employee);
        }

        // PUT: api/People/5
        [ResponseType(typeof(EmployeeViewModel))]
        public IHttpActionResult Put(int id,EmployeeViewModel employeeViewModel)
        {
           
            Employee employee = new Employee();
            employee.Person = new Person();      
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (employee != null)
                {
                    employee.Person.PersonId = employeeViewModel.PersonId;
                    employee.Person.FirstName = employeeViewModel.FirstName;
                    employee.Person.LastName = employeeViewModel.LastName;
                    employee.Person.BirthDate = employeeViewModel.BirthDate;
                    employee.EmployeeId = employeeViewModel.EmployeeId;
                    employee.EmployeeNum = employeeViewModel.EmployeeNum;
                    employee.EmployeeDate = employeeViewModel.EmployeeDate;
                    employee.PersonId = employeeViewModel.PersonId;
                    if(employeeViewModel.Terminated != null)
                    {
                        employee.Terminated = employeeViewModel.Terminated;
                    }

                    repo.Update(employee);
                    repo.Save();

                }
             
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PersonExists(employee.PersonId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/People
        [ResponseType(typeof(Employee))]
        public IHttpActionResult PostPerson(EmployeeViewModel employeeViewModel)
        {                
            Employee employee = new Employee();
            employee.Person = new Person();
            
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (employee!= null)
                {
                    employee.Person.FirstName = employeeViewModel.FirstName;
                    employee.Person.LastName = employeeViewModel.LastName;
                    employee.Person.BirthDate = employeeViewModel.BirthDate;
                    employee.EmployeeNum = employeeViewModel.EmployeeNum;
                    employee.EmployeeDate = employeeViewModel.EmployeeDate;
                    repo.Insert(employee);
                    repo.Save();
                } 
                else
                {
                    throw new Exception("Employee object empty!!");
                }
            }
            catch(Exception ex)
            {
                ex.Message.ToString();
            }
            return CreatedAtRoute("DefaultApi", new { id = employee.PersonId }, employee);
        }

        // DELETE: api/People/5
        public void Delete(int id)
        {
            try
            {
                if (id == null)
                {
                    throw new Exception("Id is null!!");
                }
                else
                {
                    repo.Delete(id);
                    repo.Save();
                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }

        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PersonExists(int id)
        {
            return db.Person.Count(e => e.PersonId == id) > 0;
        }
    }
}