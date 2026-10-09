using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Diagnostics;
using WebProductos.Datos;
using WebProductos.Models;

namespace WebProductos.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            //Creamos el modelo
            IndexModel modelo = new IndexModel();
            modelo.Productos = new List<E_Producto>();
            modelo.Sucursal = new E_Sucursal();
            try
            {
                //Crear un objeto de la capa de datos para poder usar sus metodos
                D_Producto datos = new D_Producto();
                //Obtenemos la lista de productos con la capa de datos
                List<E_Producto> productos = datos.ObtenerProductos();

                //Crear una sucursal
                E_Sucursal sucursal = new E_Sucursal();
                sucursal.Nombre = "Sucursal Coyoacan";
                sucursal.Direccion = "Calzada del Hueso 105";
                sucursal.Gerente = "Carlos Perez";

                //Asignamos valores al modelo
                modelo.Productos = productos;
                modelo.Sucursal = sucursal;

                return View("Index", modelo);
            }
            catch (Exception ex)
            {
                TempData["error"] = ex.Message;
                return View("Index", modelo);
            }
        }

        public IActionResult ObtenerProductos()
        {
            //Crear un objeto de la capa de datos para poder usar sus metodos
            D_Producto datos = new D_Producto();
            List<E_Producto> lista = datos.ObtenerProductos();
            return Json(lista);
        }


        [HttpGet]
        public IActionResult Agregar()
        {
            return View("VistaAgregar");
        }

        [HttpPost]
        public IActionResult Agregar([FromBody] E_Producto producto)
        {
            //validaciones
            string mensajeValidaciones = string.Empty;
            if (producto.Descripcion.Count() < 3)
            {
                mensajeValidaciones += "La <b>descripción</b> debe ser de almenos 4 caracteres<br>";
            }
            if (producto.Precio < 0)
            {
                mensajeValidaciones += "El <b>precio</b> debe ser un número positivo<br>";
            }
            if (producto.FechaIngreso > DateTime.Today)
            {
                mensajeValidaciones += "La <b>Fecha Ingreso</b> no puede ser mayor a la fecha actual<br>";
            }

            if (string.IsNullOrEmpty(mensajeValidaciones))
            {
                //Si mensajeValidaciones esta vacio entonces cumple las reglas y agregamos
                //Creamos objeto de la capa de datos
                D_Producto datos = new D_Producto();
                datos.AgregarProducto(producto);
                //TempData["mensaje"] = $"El producto {producto.Descripcion} se registro correctamente";
                object respuesta = new
                {
                    exito = true
                };
                return Json(respuesta);
            }
            else
            {
                //mensajeValidaciones no esta vacio por que no cumplio alguna regla, no agregamos y mostramos mensaje
                //TempData["validaciones"] = mensajeValidaciones;
                object respuesta = new
                {
                    exito = false,
                    mensaje = mensajeValidaciones
                };
                return Json(respuesta);
            }
        }

        [HttpGet]
        public IActionResult Editar(int idProducto)
        {
            D_Producto datos = new D_Producto();

            //Obtenemos los datos del producto a partir de su id
            E_Producto producto = datos.ObtenerProductoPorId(idProducto);
            //Pasamos el producto como modelo a la vista
            return Json(producto);
        }

        [HttpPost]
        public IActionResult Editar([FromBody] E_Producto producto)
        {
            D_Producto datos = new D_Producto();

            datos.EditarProducto(producto);

            //TempData["mensaje"] = $"El producto cond id: {producto.IdProducto} se actualizó correctamente";

            object respuesta = new
            {
                exito = true
            };
            return Json(respuesta);
        }

        [HttpGet]
        public IActionResult Eliminar(int idProducto)
        {
            D_Producto datos = new D_Producto();
            datos.EliminarProducto(idProducto);
            object respuesta = new
            {
                exito = true
            };
            return Json(respuesta);
        }
    }
}
