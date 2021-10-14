using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using API_BookStore_ASPNETMVC.Models;

namespace API_BookStore_ASPNETMVC.Models.Request
{
    public class LibroRequest
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["cnx"].ConnectionString;

        public List<Libro> Listado()
        {
            List<Libro> autores = new List<Libro>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand("sp_lista_libros", connection);
                command.CommandType = CommandType.StoredProcedure;

                connection.Open();

                SqlDataReader dataReader = command.ExecuteReader();

                while (dataReader.Read())
                {
                    autores.Add(new Libro()
                    {
                        Id = dataReader.GetInt32(0),
                        Isbn = dataReader.GetString(1),
                        Titulo = dataReader.GetString(2),
                        Descripcion = dataReader.GetString(3),
                        Paginas = dataReader.GetInt32(4),
                        Stock = dataReader.GetInt32(5),
                        Precio = dataReader.GetDecimal(6),
                        IdAutor = dataReader.GetInt32(7),
                        IdCategoria = dataReader.GetInt32(8)
                    });
                }

                dataReader.Close();
            }

            return autores;
        }

        public Libro ObtenerPorId(int id)
        {
            Libro libro = null;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand("sp_buscar_libro_por_id", connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add("@id", SqlDbType.Int).Value = id;

                connection.Open();

                SqlDataReader dataReader = command.ExecuteReader();

                if (dataReader.Read())
                {
                    libro = new Libro()
                    {
                        Id = dataReader.GetInt32(0),
                        Isbn = dataReader.GetString(1),
                        Titulo = dataReader.GetString(2),
                        Descripcion = dataReader.GetString(3),
                        Paginas = dataReader.GetInt32(4),
                        Stock = dataReader.GetInt32(5),
                        Precio = dataReader.GetDecimal(6),
                        IdAutor = dataReader.GetInt32(7),
                        IdCategoria = dataReader.GetInt32(8)
                    };
                }

                dataReader.Close();
            }

            return libro;
        }

        public (bool, string) Registrar(Libro libro)
        {
            string message;
            bool success;
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();

            try
            {
                SqlCommand command = new SqlCommand("sp_registrar_libro", connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add("@isbn", SqlDbType.Char, 13).Value = libro.Isbn;
                command.Parameters.Add("@titulo", SqlDbType.VarChar, 250).Value = libro.Titulo;
                command.Parameters.Add("@descripcion", SqlDbType.VarChar, 1000).Value = libro.Descripcion;
                command.Parameters.Add("@paginas", SqlDbType.Int).Value = libro.Paginas;
                command.Parameters.Add("@stock", SqlDbType.Int).Value = libro.Stock;
                command.Parameters.Add("@precio", SqlDbType.Decimal).Value = libro.Precio;
                command.Parameters.Add("@id_autor", SqlDbType.Int).Value = libro.IdAutor;
                command.Parameters.Add("@id_cat", SqlDbType.Int).Value = libro.IdCategoria;

                var i = command.ExecuteNonQuery();

                if (i > 0)
                {
                    message = $"El libro {libro.Titulo} se ah registrado correctamente";
                    success = true;
                }
                else
                {
                    message = "Oops, no se pudo registrar el libro";
                    success = false;
                }

            }
            catch (Exception e)
            {
                message = "Error Inesperado, hable con el Administrador";
                success = false;

            }
            finally
            {
                connection.Close();
            }

            return (success, message);
        }

        public (bool, string) Actualizar(Libro libro)
        {
            string message;
            bool success;
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();

            try
            {
                SqlCommand command = new SqlCommand("sp_actualizar_libro", connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add("@id", SqlDbType.Int).Value = libro.Id;
                command.Parameters.Add("@isbn", SqlDbType.Char, 13).Value = libro.Isbn;
                command.Parameters.Add("@titulo", SqlDbType.VarChar, 250).Value = libro.Titulo;
                command.Parameters.Add("@descripcion", SqlDbType.VarChar, 1000).Value = libro.Descripcion;
                command.Parameters.Add("@paginas", SqlDbType.Int).Value = libro.Paginas;
                command.Parameters.Add("@stock", SqlDbType.Int).Value = libro.Stock;
                command.Parameters.Add("@precio", SqlDbType.Decimal).Value = libro.Precio;
                command.Parameters.Add("@id_autor", SqlDbType.Int).Value = libro.IdAutor;
                command.Parameters.Add("@id_cat", SqlDbType.Int).Value = libro.IdCategoria;

                var i = command.ExecuteNonQuery();

                if (i > 0)
                {
                    message = $"El libro {libro.Titulo} se actualizó correctamente";
                    success = true;
                }
                else
                {
                    message = "Oops, no se pudo actualizar el libro";
                    success = false;
                }

            }
            catch (Exception e)
            {
                message = "Error Inesperado, hable con el Administrador";
                success = false;

            }
            finally
            {
                connection.Close();
            }

            return (success, message);
        }

        public (bool, string) Eliminar(int id)
        {
            string message;
            bool success;
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();

            try
            {
                SqlCommand command = new SqlCommand("sp_eliminar_libro", connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add("@id_libro", SqlDbType.Int).Value = id;

                var i = command.ExecuteNonQuery();

                if (i > 0)
                {
                    message = $"El libro se eliminó correctamente";
                    success = true;
                }
                else
                {
                    message = "Oops, no se pudo eliminar el libro";
                    success = false;
                }
            }
            catch (Exception e)
            {
                message = "Error Inesperado, hable con el Administrador";
                success = false;
            }
            finally
            {
                connection.Close();
            }

            return (success, message);
        }
    }
}