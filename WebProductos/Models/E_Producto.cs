using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebProductos.Models
{
    public class E_Producto
    {
        //Propiedades simples
        public int IdProducto { get; set; }

        public string Descripcion { get; set; }

        public decimal Precio { get; set; }

        public DateTime FechaIngreso { get; set; }

        public bool Disponible { get; set; }

        public string Vendedor { get; set; }
        public string Tienda { get; set; }

        //Propiedades full o de solo lecutura
        public DateTime FechaCaducidad
        {
            get
            {
                //La fecha de caducidad es la fecha de ingreso + 2 meses
                return FechaIngreso.AddMonths(2);
            }
        }

        public string DisponibleDescripcion
        {
            get
            {
                if (Disponible == true)
                    return "Si";
                else
                    return "No";
            }
        }
    }
}