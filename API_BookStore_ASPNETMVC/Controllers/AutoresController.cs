using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.Http.Cors;
using API_BookStore_ASPNETMVC.Models;

namespace API_BookStore_ASPNETMVC.Controllers
{
    [EnableCors(origins: "http://localhost:4200", headers: "*", methods: "*")]
    public class AutoresController : ApiController
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["cnx"].ConnectionString;

        [HttpGet]
        public IHttpActionResult GetAll()
        {
            List<Autor> autores = new List<Autor>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand("sp_lista_autores", connection);
                command.CommandType = CommandType.StoredProcedure;

                connection.Open();

                SqlDataReader dataReader = command.ExecuteReader();

                while (dataReader.Read())
                {
                    autores.Add(new Autor()
                    {
                        Id = dataReader.GetInt32(0),
                        Nombre = dataReader.GetString(1)
                    });
                }

                dataReader.Close();
            }

            return Ok(autores);
        }

    }
}
