using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using webapiEMS.Models;

namespace webapiEMS.Controllers
{
    public class loginController : ApiController
    {

        [Route("api/login/GetPassWord")]
        [HttpGet]
        public HttpResponseMessage GetPassWord(login log)
        {
            string query = @"
                     select Password from dbo.login where  UserName='" + log.UserName + @"'
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


        public string Post(login s)
        {
            try
            {
                string query = @"
                    insert into dbo.Login values
                    ( 
                      '" + s.UserName + @"',
                      '" + s.Password + @"',
                      '" + s.Mobile + @"',
                      '" + s.email + @"'
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

                return "SignUp Successfull";
            }
            catch (Exception)
            {
                return "Failed to Add!!!";
            }

        }

        public HttpResponseMessage Get()
        {
            string query = @"
                     select UserName,Password,Mobile,email from dbo.Login
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
    }
}
