using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using WebProductos.Models;

namespace WebProductos.Datos
{
    public class D_Producto
    {
        //Credenciales para la conexion
        private string cadenaConexion = ConfigurationManager.ConnectionStrings["sql"].ConnectionString;

        public List<E_Producto> ObtenerTodos()
        {
            //Crear una lista de productos vacia
            List<E_Producto> lista = new List<E_Producto>();
            //Crear un objeto para conectarnos a la BD
            SqlConnection conexion = new SqlConnection(cadenaConexion);
            try
            {
                //Abrir la conexion
                conexion.Open();
                //crear el query a ejecutar
                string query = "SELECT idproducto,descripcion,precio,fechaIngreso,disponible FROM Productos";
                //Crear un objeto(SqlCommand) para ejecutar el query
                SqlCommand comando = new SqlCommand(query, conexion);

                //Declaramos objeto SqlDataReader para almacenar los resultados del query al ejecutarlo
                SqlDataReader reader = comando.ExecuteReader();
                //Recorremos los resultados del reader con un ciclo while
                while (reader.Read())
                {
                    //Creamos un producto
                    E_Producto producto = new E_Producto();
                    producto.IdProducto = Convert.ToInt32(reader["idproducto"]);
                    producto.Descripcion = Convert.ToString(reader["descripcion"]);
                    producto.Precio = Convert.ToDecimal(reader["precio"]);
                    producto.FechaIngreso = Convert.ToDateTime(reader["fechaIngreso"]);
                    producto.Disponible = Convert.ToBoolean(reader["disponible"]);

                    //Agregamos el producto a la lista
                    lista.Add(producto);
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
            finally
            {
                //Cerrar la conexion
                conexion.Close();
            }

            //regresamos la lista
            return lista;
        }


        public void AgregarProducto(E_Producto producto)
        {
            //Objeto para conectanos a la BD
            SqlConnection conexion = new SqlConnection(cadenaConexion);
            try
            {
                //Abrir la conexion
                conexion.Open();
                //query a ejecutar
                string query = "INSERT INTO Productos(descripcion,precio,fechaIngreso,disponible) " +
                                            "VALUES(@descripcion,@precio,@fechaIngreso,@disponible)";
                //Objeto para ejecutar el query
                SqlCommand comando = new SqlCommand(query, conexion);

                //Asignar valores a los parametros del query
                comando.Parameters.AddWithValue("@descripcion", producto.Descripcion);
                comando.Parameters.AddWithValue("@precio", producto.Precio);
                comando.Parameters.AddWithValue("@fechaIngreso", producto.FechaIngreso);
                comando.Parameters.AddWithValue("@disponible", producto.Disponible);

                //Ejecutar el query
                comando.ExecuteNonQuery();
            }
            catch(Exception ex)
            {
                throw ex;
            }
            finally
            {
                //Cerrar la conexion
                conexion.Close();
            }  
        }

        public E_Producto ObtenerPorId(int idProducto)
        {
            //Crear una producto vacio
            E_Producto producto = new E_Producto();
            //Crear un objeto para conectarnos a la BD
            SqlConnection conexion = new SqlConnection(cadenaConexion);
            //Abrir la conexion
            conexion.Open();
            //crear el query a ejecutar
            string query = "SELECT idproducto,descripcion,precio,fechaIngreso,disponible FROM Productos " +
                            "WHERE idProducto = @idProducto";
            //Crear un objeto(SqlCommand) para ejecutar el query
            SqlCommand comando = new SqlCommand(query, conexion);

            //Asigar valor al parametro del query
            comando.Parameters.AddWithValue("@idProducto", idProducto);

            //Declaramos objeto SqlDataReader para almacenar los resultados del query al ejecutarlo
            SqlDataReader reader = comando.ExecuteReader();
            //Si hay un registro por leer lo leemos
            if (reader.Read())
            {
                //Asignamos valores a las propiedades del producto
                producto.IdProducto = Convert.ToInt32(reader["idproducto"]);
                producto.Descripcion = Convert.ToString(reader["descripcion"]);
                producto.Precio = Convert.ToDecimal(reader["precio"]);
                producto.FechaIngreso = Convert.ToDateTime(reader["fechaIngreso"]);
                producto.Disponible = Convert.ToBoolean(reader["disponible"]);
            }
            //Cerrar la conexion
            conexion.Close();
            //regresamos la lista
            return producto;
        }

        public void Actualizar(E_Producto producto)
        {
            //Crear un objeto para conectarnos a la BD
            SqlConnection conexion = new SqlConnection(cadenaConexion);
            //Abrir la conexion
            conexion.Open();
            //crear el query a ejecutar
            string query = "UPDATE Productos SET descripcion=@descripcion,precio=@precio," +
                            "fechaIngreso=@fechaIngreso,disponible=@disponible " +
                            "WHERE idProducto = @idProducto";

            SqlCommand comando = new SqlCommand(query, conexion);

            //Asignar valores a los parametros del query
            comando.Parameters.AddWithValue("@descripcion", producto.Descripcion);
            comando.Parameters.AddWithValue("@precio", producto.Precio);
            comando.Parameters.AddWithValue("@fechaIngreso", producto.FechaIngreso);
            comando.Parameters.AddWithValue("@disponible", producto.Disponible);
            comando.Parameters.AddWithValue("@idProducto", producto.IdProducto);

            comando.ExecuteNonQuery();

            conexion.Close();
        }

        public void Eliminar(int idProducto)
        {
            //Crear un objeto para conectarnos a la BD
            SqlConnection conexion = new SqlConnection(cadenaConexion);
            //Abrir la conexion
            conexion.Open();
            //crear el query a ejecutar
            string query = "DELETE Productos WHERE idProducto = @idProducto";

            SqlCommand comando = new SqlCommand(query, conexion);

            //Asignar valores a los parametros del query
            comando.Parameters.AddWithValue("@idProducto", idProducto);

            comando.ExecuteNonQuery();

            conexion.Close();
        }



    }
}