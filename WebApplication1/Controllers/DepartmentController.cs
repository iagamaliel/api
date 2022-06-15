using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {

        private readonly IConfiguration _configuration;
        public DepartmentController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        //public class Departamento
        //{
        //    public int Id { get; set; }
        //    public string title { get; set; }
        //    public string description { get; set; }
        //    public string actions { get; set; }
        //}

        //[HttpGet]
        //public JsonResult Get()
        //{
        //    string query = @"
        //                    select DepartmentId, DepartmentName from
        //                    dbo.Department
        //                    ";

        //    //DataTable table = new DataTable();
        //    ////string sqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");
        //    ////SqlDataReader myReader;
        //    ////using (SqlConnection myCon = new SqlConnection(sqlDataSource))
        //    ////{
        //    ////    myCon.Open();
        //    ////    using (SqlCommand myCommand = new SqlCommand(query, myCon))
        //    ////    {
        //    ////        myReader = myCommand.ExecuteReader();
        //    ////        table.Load(myReader);
        //    ////        myReader.Close();
        //    ////        myCon.Close();
        //    ////    }
        //    ////}
        //    //table.Load();

        //    List<Departamento> departamentos = new List<Departamento>(); ;
        //    departamentos.Add(new Departamento { Id = 1, description = "ss", actions = "1", title = "goal" });

        //    return new JsonResult(departamentos);
        //}
        public class Departamento
        {
            public int DepartmentId { get; set; }
            public string DepartmentName { get; set; }

        }

        [HttpGet]
        public JsonResult Get()
        {
            string query = @"
                            select DepartmentId, DepartmentName from
                            dbo.Department
                            ";

            //DataTable table = new DataTable();
            ////string sqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");
            ////SqlDataReader myReader;
            ////using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            ////{
            ////    myCon.Open();
            ////    using (SqlCommand myCommand = new SqlCommand(query, myCon))
            ////    {
            ////        myReader = myCommand.ExecuteReader();
            ////        table.Load(myReader);
            ////        myReader.Close();
            ////        myCon.Close();
            ////    }
            ////}
            //table.Load();

            List<Departamento> departamentos = new List<Departamento>();;
            departamentos.Add(new Departamento { DepartmentId = 1, DepartmentName = "ss" });
            departamentos.Add(new Departamento { DepartmentId = 2, DepartmentName = "asasasasa" });
            departamentos.Add(new Departamento { DepartmentId = 3, DepartmentName = "ssassasassasasass" });
            departamentos.Add(new Departamento { DepartmentId = 4, DepartmentName = "swewewewes" });
            departamentos.Add(new Departamento { DepartmentId = 5, DepartmentName = "sewewewewewewewes" });
            departamentos.Add(new Departamento { DepartmentId = 6, DepartmentName = "rrrrrrrrrrr" });

            return new JsonResult(departamentos);
        }


        [HttpGet("{id}")]
        public JsonResult GetDepartment(int id)
        {
            string query = @"
                            select DepartmentId, DepartmentName from
                            dbo.Department where DepartmentId=@DepartmentId
                            ";


            Department dato = new Department();
            string sqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@DepartmentId", id);
                    myReader = myCommand.ExecuteReader();
                    while (myReader.Read())
                    {
                        dato.DepartmentId = myReader.GetInt32("DepartmentId");
                        dato.DepartmentName = myReader.GetString("DepartmentName");
                    }

                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult(dato);
        }

        [HttpGet("Department/{DepartmentName}")]
        public JsonResult GetDepartment(string DepartmentName)
        {
            string query = @"
                            select DepartmentId, DepartmentName from
                            dbo.Department where DepartmentName like @DepartmentName
                            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@DepartmentName", "%"+DepartmentName+"%");
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult(table);
        }

        [HttpPost]
        public JsonResult Post(Department dep)
        {
            string query = @"
                           insert into dbo.Department
                           values (@DepartmentName)
                            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@DepartmentName", dep.DepartmentName);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult("Added Successfully");
        }


        [HttpPut]
        public JsonResult Put(Department dep)
        {
            string query = @"
                           update dbo.Department
                           set DepartmentName= @DepartmentName
                            where DepartmentId=@DepartmentId
                            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@DepartmentId", dep.DepartmentId);
                    myCommand.Parameters.AddWithValue("@DepartmentName", dep.DepartmentName);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult("Updated Successfully");
        }

        [HttpDelete("{id}")]
        public JsonResult Delete(int id)
        {
            string query = @"
                           delete from dbo.Department
                            where DepartmentId=@DepartmentId
                            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@DepartmentId", id);

                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult("Deleted Successfully");
        }


    }
}
