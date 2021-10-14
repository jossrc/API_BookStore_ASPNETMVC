using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data;

using System.Web.Http.Cors;
using API_BookStore_ASPNETMVC.Models;

namespace API_BookStore_ASPNETMVC.Controllers
{
    [EnableCors(origins: "http://localhost:4200", headers: "*", methods: "*")]
    public class CategoriasController : ApiController
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["cnx"].ConnectionString;

        [HttpGet]
       public IHttpActionResult GetAll()
        {
            List<Categoria> categorias = new List<Categoria>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand("sp_lista_categorias", connection);
                command.CommandType = CommandType.StoredProcedure;

                connection.Open();

                SqlDataReader dataReader = command.ExecuteReader();

                while(dataReader.Read())
                {
                    categorias.Add(new Categoria()
                    {
                        Id = dataReader.GetInt32(0),
                        Descripcion = dataReader.GetString(1)
                    }) ;
                }

                dataReader.Close();
            }

            return Ok(categorias);
        }
    }
}
