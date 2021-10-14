using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using API_BookStore_ASPNETMVC.Models;
using API_BookStore_ASPNETMVC.Models.Request;

namespace API_BookStore_ASPNETMVC.Controllers
{
    [EnableCors(origins: "http://localhost:4200", headers:"*", methods: "*")]
    public class LibrosController : ApiController
    {
        public LibroRequest libroRequest = new LibroRequest();

        [HttpGet]
        public IHttpActionResult GetAll()
        {
            return Ok(libroRequest.Listado());
        }

        [HttpGet]
        public IHttpActionResult GetById(int id)
        {
            Libro libro = libroRequest.ObtenerPorId(id) ;

            string message;
            bool success;

            if (libro == null)
            {
                message = $"No se encontró el Libro con Id {id}";
                success = false;
            } else
            {
                message = "Libro Encontrado";
                success = true;
            }

            var data = new
            {
                success,
                message,
                book = libro
            };

            return Ok(data);
        }

        [HttpPost]
        public IHttpActionResult Add(Libro libro)
        {
            var (success, message) = libroRequest.Registrar(libro);           

            return Ok(new { 
                success,
                message
            });

        }

        [HttpPut]
        public IHttpActionResult Update(Libro libro)
        {
            var (success, message) = libroRequest.Actualizar(libro);

            return Ok(new
            {
                success,
                message
            });

        }

        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            string message;
            bool success;
            Libro libro = libroRequest.ObtenerPorId(id);

            if (libro == null)
            {
                message = $"No se encontró el Libro con Id {id}";
                success = false;
            }
            else
            {
                (success, message) = libroRequest.Eliminar(id);
            }

            return Ok(new
            {
                success,
                message
            });
        }
    }
}
