using DemoProc.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Data.SqlClient;
using System.Net;
using System.Web.Http.Common;
using Dapper;
using System.Linq;
using System.Configuration;

using System.Collections.Generic;
using DemoProc.Services;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;



namespace DemoProc.Services
{

    public class EmployeeService : IEmployeeService
    {
        private readonly IConfiguration _configuration;
        private string result;

        //private object command;

        public EmployeeService(IConfiguration configuration)
        {
            _configuration = configuration;
            ConnectionString = _configuration.GetConnectionString("DBConnection");
            ProvideName = "System.Data.SqlClient";
        }
        public string ConnectionString { get; set; }
        public string ProvideName { get; set; }
        public IDbConnection Connection
        {
            get { return new SqlConnection(ConnectionString); }
        }
       

        [HttpGet]
        //[Route("SaveEmployees")]
        public List<Employee> GetEmployeeList()
        {
            List<Employee> employees = new List<Employee>();
            try
            {
                using (IDbConnection dbConnection = Connection)
                {
                    dbConnection.Open();
                     employees = dbConnection.Query<Employee>("SaveEmployees",
                                       commandType: CommandType.StoredProcedure).ToList();
                    dbConnection.Close();
                    return employees;
                }
            }
            catch (Exception ex)
            {
                string errorMsg = ex.Message;
                return employees;
            }
        }
        //[HttpPost]
        public string InsertEmployee(Employee employee)
        {
            //throw new NotImplementedException();
            string result = "";
            try
            {
                using (IDbConnection dbConnection = Connection)
                {
                    dbConnection.Open();
                   var emp = dbConnection.Query<Employee>("AddEmployees",
                        new
                        {
                            first_name = employee.first_name,
                            last_name = employee.last_name,
                            department = employee.department
                        },
                        commandType: CommandType.StoredProcedure);
                    if (emp != null && emp.FirstOrDefault().Response =="added successfully")
                    {
                        result = "added successfully";
                    }
                    dbConnection.Close();
                    return result;
                }
            }
            catch (Exception ex)
            {
                string errorMsg = ex.Message;
                return errorMsg;
            }
        }
        //[HttpPut]
        public string UpdateEmployee(Employee employee)
        {
            //throw new NotImplementedException();
            string result = "";
            try
            {
                using (IDbConnection dbConnection = Connection)
                {
                    dbConnection.Open();
                    var emp = dbConnection.Query<Employee>("UpdateEmployees",
                        new
                        {
                             Id= employee.Id,
                            first_name = employee.first_name,
                            last_name = employee.last_name,
                            department = employee.department
                         },
                        commandType: CommandType.StoredProcedure);
                    //emp.parameters.AddWithValue("@firstName", employee.first_name);
                    if (emp != null)
                    {
                        result =  "Updated successfully";
                    }
                    dbConnection.Close();
                    return result;
                }
            }
            catch (Exception ex)
            {
                string errorMsg = ex.Message;
                return errorMsg;
            }
        }
        [HttpPost]
        public string DeleteEmployee(int Id)
        {
            //throw new NotImplementedException();
            string result = "";
            try
            {
                using (IDbConnection dbConnection = Connection)
                {
                    dbConnection.Open();
                    var emp = dbConnection.Query<Employee>("DeleteEmployee",
                        new
                        {
                            id = Id 
                        },
                        commandType: CommandType.StoredProcedure);
                    if (emp != null)
                    {
                        result = "deleted successfully";
                    }
                    dbConnection.Close();
                    return result;
                }
            }
            catch (Exception ex)
            {
                string errorMsg = ex.Message;
                return errorMsg;
            }
        }

    }
}









      

       
    
