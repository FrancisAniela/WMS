using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;


namespace CellariumAndroid.Models
{

	public class UserAct
	{


		public static int Usuario_id { get; set; } = 0;
		public static string Nombre { get; set; }
		public static int Perfil_Id { get; set; }
		public static bool MultiEmpresa { get; set; }
		public static bool MultiSucursal { get; set; }
		public static bool MultiAlmacen { get; set; }
		public static string Maquina { get; set; }
		public static string Ip { get; set; }

	}
}
