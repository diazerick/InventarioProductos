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

        public string Gerente { get; set; }

        //Propiedades de solo lectura o full
        public DateTime FechaCaducidad
        {
            get
            {
                return FechaIngreso.AddMonths(2);
            }
        }
        public string DisponibleTexto 
        { 
            get 
            {
                if(Disponible == true)
                {
                    return "Si";
                }
                else
                {
                    return "No";
                }
            } 
        }
        public string DisponibleHtml
        {
            get
            {
                if (Disponible == true)
                {
                    return "<span class='badge text-bg-success'>Si</span>";
                }
                else
                {
                    return "<span class='badge text-bg-danger'>No</span>";
                }
            }
        }

    }
}
