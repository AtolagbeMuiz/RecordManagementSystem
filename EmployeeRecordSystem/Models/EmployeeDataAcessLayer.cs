using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeRecordSystem.Models
{
    public class EmployeeDataAcessLayer : IEmployeeInterface
    {
        string connectionString = string.Empty;


        private readonly ConnectionStringConfig _connectionStringConfig;

        //Constructor
        public EmployeeDataAcessLayer(IOptions<ConnectionStringConfig> configAccessor)
        {
            _connectionStringConfig = configAccessor.Value;
            connectionString = _connectionStringConfig.DefaultConnection;

        }

        //To Add new employee record    
        public void AddEmployee(Employee employee)
        {
            //List<Employee> employees = new List<Employee>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                
                SqlCommand cmd = new SqlCommand("AddEmployee", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Name", employee.Name);
                cmd.Parameters.AddWithValue("@Gender", employee.Gender);
                cmd.Parameters.AddWithValue("@Department", employee.Department);
                cmd.Parameters.AddWithValue("@City", employee.City);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
               // employees.Add(employee);
            }
           //return employee;
            
        }


        //To View all employees details    
        public IEnumerable<Employee> GetAllEmployees()
        {
            List<Employee> lstemployee = new List<Employee>();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("GetAllEmployees", con);
                cmd.CommandType = CommandType.StoredProcedure;

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    Employee employee = new Employee();

                    employee.ID = Convert.ToInt32(rdr["ID"]);
                    employee.Name = rdr["Name"].ToString();
                    employee.Gender = rdr["Gender"].ToString();
                    employee.Department = rdr["Department"].ToString();
                    employee.City = rdr["City"].ToString();

                    lstemployee.Add(employee);
                }
                con.Close();
            }
            return lstemployee;
        }


        //To Update the records of a particluar employee  
        public void UpdateEmployee(Employee employee)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("UpdateEmployee", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@EmpId", employee.ID);
                cmd.Parameters.AddWithValue("@Name", employee.Name);
                cmd.Parameters.AddWithValue("@Gender", employee.Gender);
                cmd.Parameters.AddWithValue("@Department", employee.Department);
                cmd.Parameters.AddWithValue("@City", employee.City);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }


           //To gets an employee details
        public Employee GetEmployeeDetails(int? id)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                Employee emp = new Employee();
                //List<Employee> lst = new List<Employee>();
                SqlCommand cmd = new SqlCommand("GetEmployeeDetails", con);
                cmd.CommandType = CommandType.StoredProcedure;

                con.Open();
                cmd.Parameters.AddWithValue("@empID", id);

                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    emp.ID = Convert.ToInt32(rdr["ID"]);
                    emp.Name = rdr["Name"].ToString();
                    emp.Gender = rdr["Gender"].ToString();
                    emp.Department = rdr["Department"].ToString();
                    emp.City = rdr["City"].ToString();                  
                }
             
                return emp;

            }
        }

        //To Delete the record on a particular employee  
        public void DeleteEmployeeDetails(int? id)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                Employee emp = new Employee();
                //List<Employee> lst = new List<Employee>();
                SqlCommand cmd = new SqlCommand("DeleteEmployeeDetails", con);
                cmd.CommandType = CommandType.StoredProcedure;

                con.Open();
                cmd.Parameters.AddWithValue("@empID", id);
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        public string GetLogin(Login login)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("LoginDetails", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Name", login.Name);
                cmd.Parameters.AddWithValue("@Dept", login.Department);
                con.Open();
                int codereturn = (int)cmd.ExecuteScalar();
                if (codereturn == 1)
                {
                    return "1";
                }
                else
                {
                    return "-1";
                }
                con.Close();
            }
        }
    }
}
