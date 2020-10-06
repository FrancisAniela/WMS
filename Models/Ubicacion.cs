using System;
using System.Collections.Generic;
using System.Text;

namespace CellariumAndroid.Models
{
    public class Ubicacion
    {
        //USP: [Procesos].[usp_Conteo_Cantidad_Ubicacion],
        //Params: @idAlmacen => int


        public int IdEmpresa { get; set; }

        public int IdAlmacen { get; set; }

        public int IdUbicacion { get; set; }

        public int Estatus { get; set; }

        public int Tipo { get; set; }

        public string Codigo { get; set; }

        public string CodigoExtendido { get; set; }

        public string CodigoBarra { get; set; }

        public string Descripcion { get; set; }

        //Recordar añadir propiedades del : usp usp_Filtro_Restriccion_Articulo_Ubicacion para mejorar la sugerencia de 
        //ubicaciones compatibles, en base a la condicion generada
        // IdCondicion, DescripcionCondicion


        public int ubicaciones { get; set; } //deberia ir en minusculas según --- Acomodar el nombre de la Propiedad

        //Asumiendo que este atributo se convierte en un array al obtener la data
    }
}
