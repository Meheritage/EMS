using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using webapiEMS.Models;

namespace webapiEMS.Controllers
{
    public class EmployeeController : ApiController
    {
        public HttpResponseMessage Get()
        {
            string query = @"
            select EmpID,EmpName,Department,Gender,convert(varchar(10),DOB,120)as DOB,IDProofType,IDProofNumber,Phone,BloodGroup,emailID,EmpAddress,PhotoName from dbo.Employee
            ";
            DataTable table = new DataTable();
            using (var con =new SqlConnection(ConfigurationManager.
                ConnectionStrings["EmployeeDB"].ConnectionString))
                using (var cmd =new SqlCommand(query,con))
            using(var da=new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }
            return Request.CreateResponse(HttpStatusCode.OK, table);
              
        }

        public HttpResponseMessage Get(int id)
        {
            string query = @"
            select EmpID,EmpName,Department,Gender,convert(varchar(10),DOB,120)as DOB,IDProofType,IDProofNumber,Phone,BloodGroup,emailID,EmpAddress,PhotoName from dbo.Employee where 
            EmpID=" + id + @" 
            ";
            DataTable table = new DataTable();
            using (var con = new SqlConnection(ConfigurationManager.
                ConnectionStrings["EmployeeDB"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }
            return Request.CreateResponse(HttpStatusCode.OK, table);

        }

        public string Post(details emp)
        {
            try
            {
                string query = @"
                    insert into dbo.Employee values
                    (
                      '" + emp.EmpName + @"',
                      '" + emp.Department + @"',
                      '" + emp.Gender + @"',
                      '" + emp.DOB + @"',
                      '" + emp.IDProofType + @"',
                      '" + emp.IDProofNumber + @"',
                      '" + emp.Phone + @"',
                      '" + emp.BloodGroup + @"',
                      '" + emp.emailID + @"',
                      '" + emp.EmpAddress+ @"',
                      '" + emp.PhotoName + @"'                     
                   )";
                DataTable table = new DataTable();
                using (var con = new SqlConnection(ConfigurationManager.
                    ConnectionStrings["EmployeeDB"].ConnectionString))
                using (var cmd = new SqlCommand(query, con))
                using (var da = new SqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.Text;
                    da.Fill(table);
                }
                return "Added Successfully!";
            }
            catch
            {
                return "Failed to Add!";
            }
        }
        public string Put(details emp)
        {
            try
            {
                string query = @"
                    update dbo.Employee set EmpName= '" + emp.EmpName + @"',
                                            Department= '" + emp.Department + @"',
                                            Gender='" + emp.Gender + @"',
                                            DOB='" + emp.DOB + @"',
                                            IDProofType='" + emp.IDProofType + @"',
                                            IDProofNumber= '" + emp.IDProofNumber + @"',
                                            Phone='" + emp.Phone + @"',
                                            BloodGroup='" + emp.BloodGroup + @"',
                                            emailID='" + emp.emailID + @"',
                                            EmpAddress='" + emp.EmpAddress + @"',
                                            PhotoName= '" + emp.PhotoName + @"'
                    where EmpID=" + emp.EmpID+@"  
                 ";
                DataTable table = new DataTable();
                using (var con = new SqlConnection(ConfigurationManager.
                    ConnectionStrings["EmployeeDB"].ConnectionString))
                using (var cmd = new SqlCommand(query, con))
                using (var da = new SqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.Text;
                    da.Fill(table);
                }
                return "Updated Successfully!";
            }
            catch
            {
                return "Failed to Update!";
            }
        }

        public string Delete(int id)
        {
            try
            {
                string query = @"
                   delete  from dbo.Employee where EmpID="+id+@"
                   ";
                DataTable table = new DataTable();
                using (var con = new SqlConnection(ConfigurationManager.
                    ConnectionStrings["EmployeeDB"].ConnectionString))
                using (var cmd = new SqlCommand(query, con))
                using (var da = new SqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.Text;
                    da.Fill(table);
                }
                return "Deleted Successfully!";
            }
            catch
            {
                return "Failed to Delete!";
            }
        }
        [Route("api/Employee/savefile")]
        public string savefile()
        {
            try
            {
                var httpRequest = HttpContext.Current.Request;
                var postedFile = httpRequest.Files[0];
                string filename = postedFile.FileName;
                var physicalPath = HttpContext.Current.Server.MapPath("~/Photos/" + filename);
                postedFile.SaveAs(physicalPath);
                return filename;
            }
            catch (Exception)
            {
                return "anonymous.png";
            }
        }
        [Route("api/Employee/GetaAllDepartmentName")]
        [HttpGet]
        public HttpResponseMessage GetAllDeparttmentName()
        {
            string query = @"
                   select Name  from dbo.Department";
            DataTable table = new DataTable();
            using (var con = new SqlConnection(ConfigurationManager.
                ConnectionStrings["EmployeeDB"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }
            return Request.CreateResponse(HttpStatusCode.OK,table);
        }

    }
}
