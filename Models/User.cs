using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;


namespace CellariumAndroid.Models
{

	public class User
	{

		public int Usuario_id { get; set; }
		public string Nombre { get; set; }
		public int Perfil_Id { get; set; }
		public int MultiEmpresa { get; set; }
		public int MultiSucursal { get; set; }
		public int MultiAlmacen { get; set; }
	}
}
