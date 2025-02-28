using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebProductos.Datos;
using WebProductos.Models;


namespace WebProductos.Controllers
{
    public class ProductoController : Controller
    {
        // GET: Producto
        public ActionResult Index()
        {
            //Declaramos la lista de productos vacia
            List<E_Producto> productos = new List<E_Producto>();
            try
            {
                //Crear un objeto de la capa de datos
                D_Producto datos = new D_Producto();
                //Obtenemos la lista de productos de la capa de datos
                productos = datos.ObtenerTodos();
                //Pasamos la lista productos como modelo a la vista
                return View("Principal", productos);
            }
            catch (Exception ex)
            {
                TempData["error"] = ex.Message;
                return View("Principal", productos);
            }
        }

        public ActionResult IrAgregar()
        {
            return View("VistaAgregar");
        }

        public ActionResult Agregar(E_Producto objProducto)
        {
            //Creamos objeto de la capa de datos
            D_Producto datos = new D_Producto();
            //Llamamos al metodo Agregar de la capa de datos
            datos.AgregarProducto(objProducto);

            //Tempadata para mostrar mensaje al usuario
            TempData["mensaje"] = $"El producto {objProducto.Descripcion} se registro correctamente.";

            //Redirigir hacia la accion Index
            return RedirectToAction("Index");
        }

        public ActionResult IrEditar(int idProducto)
        {
            //Creamos objeto de la capa de datos
            D_Producto datos = new D_Producto();

            //Obtener un objeto Producto de la capa de datos
            E_Producto producto = datos.ObtenerPorId(idProducto);

            //Pasamos el producto como modelo a la vista
            return View("VistaEditar", producto);
        }

        public ActionResult Actualizar(E_Producto producto)
        {
            //Creamos objeto de la capa de datos
            D_Producto datos = new D_Producto();

            //Mandamos a llamar el metodo Actualizar de la capa de datos
            datos.Actualizar(producto);

            TempData["mensaje"] = $"El producto con id:{producto.IdProducto} se modifico correctamente";

            return RedirectToAction("Index");
        }

        public ActionResult Eliminar(int idProducto)
        {
            //Creamos objeto de la capa de datos
            D_Producto datos = new D_Producto();

            //Mandamos a llamar el metodo Eliminar de la capa de datos
            datos.Eliminar(idProducto);

            TempData["mensaje"] = $"El producto con id:{idProducto} se elimino correctamente";

            return RedirectToAction("Index");
        }
    }
}