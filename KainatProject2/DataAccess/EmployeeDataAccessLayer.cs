using KainatProject2.Interfaces;
using KainatProject2.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace KainatProject2.DataAccess
{
    public class EmployeeDataAccessLayer : IEmployee
    {
        private string connectionString;
        public EmployeeDataAccessLayer(IConfiguration configuration)
        {
            connectionString = configuration["ConnectionStrings:DefaultConnection"];
        }
        public IEnumerable<Employee> GetAllEmployees()
        {
            try
            {
                List<Employee> anemp = new List<Employee>();

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("spAllEmployees", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    con.Open();
                    SqlDataReader abc = cmd.ExecuteReader();

                    while (abc.Read())
                    {
                        Employee employee = new Employee();

                        employee.EmployeeId = Convert.ToInt32(abc["EmployeeID"]);
                        employee.Name = abc["Name"].ToString();
                        employee.Gender = abc["Gender"].ToString();
                        employee.Department = abc["Department"].ToString();
                  

                        anemp.Add(employee);
                    }
                    con.Close();
                }
                return anemp;
            }
            catch
            {
                throw;
            }
        }

        public int AddEmployee(Employee employee)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("AddEmployee", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Name", employee.Name);
                    cmd.Parameters.AddWithValue("@Gender", employee.Gender);
                    cmd.Parameters.AddWithValue("@Department", employee.Department);
              

                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
                return 1;
            }
            catch
            {
                throw;
            }
        }

      
        public int UpdateEmployee(Employee employee)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("UpdateEmployee", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@EmpId", employee.EmployeeId);
                    cmd.Parameters.AddWithValue("@Name", employee.Name);
                    cmd.Parameters.AddWithValue("@Gender", employee.Gender);
                    cmd.Parameters.AddWithValue("@Department", employee.Department);
             

                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
                return 1;
            }
            catch
            {
                throw;
            }
        }

     
        public Employee GetEmployeeData(int id)
        {
            try
            {
                Employee employee = new Employee();

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    string sqlQuery = "SELECT * FROM Employees WHERE EmployeeId= " + id;
                    SqlCommand cmd = new SqlCommand(sqlQuery, con);

                    con.Open();
                    SqlDataReader abc = cmd.ExecuteReader();

                    while (abc.Read())
                    {
                        employee.EmployeeId = Convert.ToInt32(abc["EmployeeId"]);
                        employee.Name = abc["Name"].ToString();
                        employee.Gender = abc["Gender"].ToString();
                        employee.Department = abc["Department"].ToString();
              
                    }
                }
                return employee;
            }
            catch
            {
                throw;
            }
        }

        public int DeleteEmployee(int id)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("DeleteEmployee", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@EmpId", id);

                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
                return 1;
            }
            catch
            {
                throw;
            }
        }

        public int Logincheck(Admin ad)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    SqlCommand com = new SqlCommand("admin_login", con);
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.AddWithValue("@username", ad.username);
                    com.Parameters.AddWithValue("@password", ad.password);
                    SqlParameter oblogin = new SqlParameter();
                    oblogin.ParameterName = "@Isvalid";
                    oblogin.SqlDbType = SqlDbType.Bit;
                    oblogin.Direction = ParameterDirection.Output;
                    com.Parameters.Add(oblogin);
                    con.Open();
                    com.ExecuteNonQuery();
                    int res = Convert.ToInt32(oblogin.Value);
                    con.Close();
                    return res;
                }
            }
            catch
            {
                throw;
            }
        }

    }
}
