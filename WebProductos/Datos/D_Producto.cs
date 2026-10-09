using Microsoft.Data.SqlClient;
using WebProductos.Models;

namespace WebProductos.Datos
{
    public class D_Producto
    {
        private string cadenaConexion = "server=localhost;database=generacion44;user=sa;password=devo123;TrustServerCertificate=true";
        public List<E_Producto> ObtenerProductos()
        {
            //Creamos una lista de productos
            List<E_Producto> lista = new List<E_Producto>();
            //Creamos objeto para conectarnos a la BD
            SqlConnection conexion = new SqlConnection(cadenaConexion);
            try
            {
                //Abrir la conexion
                conexion.Open();
                //Query a ejecutar
                string query = "SELECT idProducto,descripcion,precio,fechaIngreso,disponible FROM Productos";
                //Creamos objeto para ejecutar el query
                SqlCommand comando = new SqlCommand(query, conexion);
                //Creamos un objeto para almacenar los resultados (SqlDataReader)
                //Ejecutamos el metodo con ExecuteReader porque es un Select y los resultados los guardamos en reader
                SqlDataReader reader = comando.ExecuteReader();
                //Recorremos los resultados de reader con un ciclo
                while (reader.Read())
                {
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
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                //Cerrar la conexion
                conexion.Close();
            }                      
            //regresamos la lista de productos
            return lista;
        }

        public void AgregarProducto(E_Producto producto)
        {
            SqlConnection conexion = new SqlConnection(cadenaConexion);
            conexion.Open();
            //Creamos el query con parametros de SQL (@nombreParametro)
            string query = "INSERT INTO Productos(descripcion,precio,fechaIngreso,disponible) " +
                                     "VALUES(@descripcion,@precio,@fechaIngreso,@disponible)";
            SqlCommand comando = new SqlCommand(query, conexion);
            //Asginamos valores a los parametros del query
            comando.Parameters.AddWithValue("@descripcion", producto.Descripcion);
            comando.Parameters.AddWithValue("@precio", producto.Precio);
            comando.Parameters.AddWithValue("@fechaIngreso", producto.FechaIngreso);
            comando.Parameters.AddWithValue("@disponible", producto.Disponible);
            //Ejecutamos el query
            comando.ExecuteNonQuery();
            conexion.Close();
        }

        public E_Producto ObtenerProductoPorId(int idProducto)
        {
            E_Producto producto = new E_Producto();
            SqlConnection conexion = new SqlConnection(cadenaConexion);
            conexion.Open();
            string query = "SELECT idProducto,descripcion,precio,fechaIngreso,disponible " +
                            "FROM Productos WHERE idProducto = @idProducto";
            SqlCommand comando = new SqlCommand(query, conexion);
            comando.Parameters.AddWithValue("@idProducto", idProducto);
            SqlDataReader reader = comando.ExecuteReader();
            if (reader.Read())
            {
                producto.IdProducto = Convert.ToInt32(reader["idproducto"]);
                producto.Descripcion = Convert.ToString(reader["descripcion"]);
                producto.Precio = Convert.ToDecimal(reader["precio"]);
                producto.FechaIngreso = Convert.ToDateTime(reader["fechaIngreso"]);
                producto.Disponible = Convert.ToBoolean(reader["disponible"]);
            }
            conexion.Close();
            return producto;
        }

        public void EditarProducto(E_Producto producto)
        {
            SqlConnection conexion = new SqlConnection(cadenaConexion);
            try
            {
                conexion.Open();
                string query = "UPDATE Productos SET descripcion=@descripcion, precio=@precio," +
                                "fechaIngreso=@fechaIngreso, disponible=@disponible " +
                                "WHERE idProducto=@idProducto";
                SqlCommand comando = new SqlCommand(query, conexion);
                comando.Parameters.AddWithValue("@descripcion", producto.Descripcion);
                comando.Parameters.AddWithValue("@precio", producto.Precio);
                comando.Parameters.AddWithValue("@fechaIngreso", producto.FechaIngreso);
                comando.Parameters.AddWithValue("@disponible", producto.Disponible);
                comando.Parameters.AddWithValue("@idProducto", producto.IdProducto);
                comando.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conexion.Close();
            }                        
        }

        public void EliminarProducto(int idProducto)
        {
            SqlConnection conexion = new SqlConnection(cadenaConexion);
            try
            {
                conexion.Open();
                string query = "DELETE Productos WHERE idProducto = @idProducto";
                SqlCommand comando = new SqlCommand(query, conexion);
                comando.Parameters.AddWithValue("@idProducto", idProducto);
                comando.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conexion.Close();
            }                       
        }
    }
}
