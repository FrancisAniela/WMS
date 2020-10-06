using CellariumAndroid.Models;
using CellariumAndroid.Views;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using Xamarin.Forms;
using CellariumAndroid.Repository;

namespace CellariumAndroid.ViewModels
{
	public class LoginViewModel : BaseViewModel
	{

		private static ObservableCollection<Almacenes> _allItems;
		public ObservableCollection<Almacenes> AllItems { get => _allItems; set => _allItems = value; }
		public static Entry UsernameText;
		public static Entry passwordText;
		public static List<Almacenes> almacenes;
		public static Almacenes almacen;
		public bool _activatyIndicatorAvaible;
		public bool _opaque;
		public static LoginViewModel Login = new LoginViewModel();
		public bool ActivatyIndicatorAvaible { get => _activatyIndicatorAvaible; set { _activatyIndicatorAvaible = value; OnPropertyChanged(); } }

		//Esta propiedad se usa para el Binding en la vista correspondiente a la seleccion del Almacen. Y activa o desactiva el modal.
		public bool IsOpaque { get => _opaque; set { _opaque = value; OnPropertyChanged(); } }

		public LoginViewModel()
		{
			_allItems = new ObservableCollection<Almacenes>();
		}

		#region Methods
		private static string ObtenerHash(string sPassword)
		{
			string _result = "";
			using (MD5 hasher = MD5.Create())
			{
				byte[] dbytes = hasher.ComputeHash(Encoding.UTF8.GetBytes(sPassword));
				StringBuilder sBuilder = new StringBuilder();

				for (int i = 0; i < dbytes.Length; i++)
				{
					sBuilder.Append(dbytes[i].ToString("X2"));
				}
				_result = sBuilder.ToString();
			}
			return _result;
		}

		// El miembro: Opaque, pertenece al BaseViewModel, y se usa a traves de todos los viewmodels para las pantallas de carga.
		// Se puede acceder a sus propiedades directamente.

		public static async void pruebaConexion(string Ip, string Serv, string DBNom, string User, string Pass)
		{
			ErrorWarningModal error;
			if (!AccesoRed.GetConexion())
			{
				error = new ErrorWarningModal("Debe conectarse a una red local");
				await Navigation.PushModalAsync(error);
				return;
			}
			HttpClient client = new HttpClient();
			var postData = new List<KeyValuePair<string, string>>();
			client.Timeout = TimeSpan.FromSeconds(10);
			postData.Add(new KeyValuePair<string, string>("server", Serv));
			postData.Add(new KeyValuePair<string, string>("database", DBNom));
			postData.Add(new KeyValuePair<string, string>("usernamesql", User));
			postData.Add(new KeyValuePair<string, string>("passwordsql", Pass));
			var content = new FormUrlEncodedContent(postData);
			try
			{
				Login.IsOpaque = true; 
				Login.ActivatyIndicatorAvaible= true;
				var response = await client.PostAsync("http://" + Ip + "/Cellarium/conexiontry.php", content);
				var jsonresult = response.Content.ReadAsStringAsync().Result;
				var responseObject = JsonConvert.DeserializeObject(jsonresult);

				if (responseObject == null)
				{
					defaultActivityIndicator.IsRunning = false;
					Opaque.IsVisible = false;
					UserInfo(UsernameText, passwordText);

				}
				else
				{
					error = new ErrorWarningModal("Error en la conexión con el servidor");
					await Navigation.PushModalAsync(error);
				}
			}
			catch (Exception ex)
			{
				Login.IsOpaque = false;
				Login.ActivatyIndicatorAvaible = false;
				Console.WriteLine(ex.Message);
				error = new ErrorWarningModal("Error en la conexión con el servidor");
				await Navigation.PushModalAsync(error);
			}
			content.Dispose();
			client.Dispose();
		}

		public static async void UserInfo(Entry usernameText, Entry passwordText)
		{
			ErrorWarningModal error;
			if (!AccesoRed.GetConexion())
			{
				error = new ErrorWarningModal("Debe conectarse a una red local");
				await Navigation.PushModalAsync(error);
				return;
			}
			HttpClient client = new HttpClient();
			var postData = new List<KeyValuePair<string, string>>();
			client.Timeout = TimeSpan.FromSeconds(10);
			postData.Add(new KeyValuePair<string, string>("server", Server.ServerDB));
			postData.Add(new KeyValuePair<string, string>("database", Server.Database));
			postData.Add(new KeyValuePair<string, string>("usernamesql", Server.UsernameDB));
			postData.Add(new KeyValuePair<string, string>("passwordsql", Server.PasswordDB));
			postData.Add(new KeyValuePair<string, string>("username", usernameText.Text.ToUpper()));
			postData.Add(new KeyValuePair<string, string>("password", ObtenerHash(passwordText.Text)));
			var content = new FormUrlEncodedContent(postData);

			try
			{
				User user = null;
				var response = await client.PostAsync("http://" + Server.IpAddress + "/Cellarium/login.php", content);
				var jsonresult = response.Content.ReadAsStringAsync().Result;
				var jsonObject = JsonConvert.SerializeObject(jsonresult);
				try
				{

					if (jsonresult == "")
					{
						usernameText.Text = "";
						passwordText.Text = "";
						error = new ErrorWarningModal("Usuario o contraseña incorrectos");
						await Navigation.PushModalAsync(error);
						Login.IsOpaque = false;
						Login.ActivatyIndicatorAvaible = false;
					}
					else
					{
						user = JsonConvert.DeserializeObject<User>(jsonresult);
						Login.IsOpaque = false;
						Login.ActivatyIndicatorAvaible = false;
						UserAct.Usuario_id = user.Usuario_id;
						UserAct.Nombre = user.Nombre;
						UserAct.Perfil_Id = user.Perfil_Id;
						UserAct.MultiAlmacen = Convert.ToBoolean(user.MultiAlmacen);
						UserAct.MultiEmpresa = Convert.ToBoolean(user.MultiEmpresa);
						UserAct.MultiSucursal = Convert.ToBoolean(user.MultiSucursal);
						UserAct.Maquina = Environment.MachineName.ToString();
						string auxIP = "";
						IPHostEntry _host = Dns.GetHostEntry(Dns.GetHostName());
						foreach (IPAddress ip in _host.AddressList)
						{
							if (ip.AddressFamily.ToString() == "InterNetwork")
							{
								auxIP = ip.ToString();
							}
						}
						UserAct.Ip = auxIP;
						if (UserAct.Usuario_id != 0)
						{
							passwordText.Text = "";
							usernameText.Text = "";
							ConsultaAlmacenes();

						}
					}
				}
				catch (Exception exc)
				{
					Login.IsOpaque = false;
					Login.ActivatyIndicatorAvaible = false;
					Console.WriteLine(exc.Message);
					usernameText.Text = "";
					passwordText.Text = "";
					error = new ErrorWarningModal("Error al iniciar sesión");
					await Navigation.PushModalAsync(error);
				}

			}
			catch (HttpRequestException exc)
			{
				Login.IsOpaque = false;
				Login.ActivatyIndicatorAvaible = false;
				Console.WriteLine(exc.Message);
				error = new ErrorWarningModal("Error en la conexión a la base de datos, revise la configuración de credenciales del servidor");
				await Navigation.PushModalAsync(error);
			}
			content.Dispose();
			client.Dispose();

		}

		public static async void ConsultaAlmacenes()
		{
			ErrorWarningModal error;
			if (!AccesoRed.GetConexion())
			{
				error = new ErrorWarningModal("Debe conectarse a una red local");
				await Navigation.PushModalAsync(error);
				return;
			}
			HttpClient client = new HttpClient();
			almacenes = new List<Almacenes>();
			almacen = null;
			string query = String.Format("SELECT Id_Almacen id, " +
				"rTrim(Cod)+'-' +rtrim(descripcion) almacen " +
				", almacen.Id_Empresa idEmpresa," +
				" almacen.Id_Sucursal idSucursal , " +
				"usuario.MultiEmpresa " +
				"from Alm.Almacen almacen" +
				" inner join Acces.ManejoDependencias manejo on manejo.IdDependencia = almacen.Id_Almacen and manejo.Tipo = 3" +
				" inner join[Acces].[UsrSys] usuario on usuario.Usuario_id = manejo.IdUsuario"
				+ " Where manejo.IdUsuario = {0} and Sel={1}", UserAct.Usuario_id, 1);

			var postData = new List<KeyValuePair<string, string>>();
			client.Timeout = TimeSpan.FromSeconds(4);
			postData.Add(new KeyValuePair<string, string>("server", Server.ServerDB));
			postData.Add(new KeyValuePair<string, string>("database", Server.Database));
			postData.Add(new KeyValuePair<string, string>("usernamesql", Server.UsernameDB));
			postData.Add(new KeyValuePair<string, string>("passwordsql", Server.PasswordDB));
			postData.Add(new KeyValuePair<string, string>("query", query));
			var content = new FormUrlEncodedContent(postData);
			try
			{
				Opaque.IsVisible = true;
				defaultActivityIndicator.IsRunning = true;
				var response = await client.PostAsync("http://" + Server.IpAddress + "/Cellarium/getdata.php", content);
				var jsonresult = response.Content.ReadAsStringAsync().Result;

				try
				{
					if (jsonresult.ToString().Length == 0)
					{
						defaultActivityIndicator.IsRunning = false;
						Opaque.IsVisible = false;
						error = new ErrorWarningModal("Error obteniendo dependencias del usuario");
						await Navigation.PushModalAsync(error);
						return;
					}

					defaultActivityIndicator.IsRunning = false;
					Opaque.IsVisible = false;

					if (jsonresult.Split(",{").Length > 1)
					{
						almacenes = JsonConvert.DeserializeObject<List<Almacenes>>(jsonresult);
						await Shell.Current.GoToAsync("///SelectAlmacen");
					}
					else
					{
						almacen = JsonConvert.DeserializeObject<Almacenes>(jsonresult);
						EmpresaSucursalAct.Almacen = almacen.id;
						EmpresaSucursalAct.Empresa = almacen.idEmpresa;
						EmpresaSucursalAct.Sucursal = almacen.idSucursal;
						UserAct.MultiAlmacen = false;
						UserAct.MultiEmpresa = almacen.multiEmpresa;
						UserAct.MultiSucursal = false;
						await Shell.Current.GoToAsync("///MenuPrincipal");
					}
				}
				catch (Exception exc)
				{
					defaultActivityIndicator.IsRunning = false;
					Opaque.IsVisible = false;
					Console.WriteLine(exc.HResult);
					error = new ErrorWarningModal("Error obteniendo la información de los almacenes");
					await Navigation.PushModalAsync(error);
				}
			}
			catch (HttpRequestException exc)
			{
				defaultActivityIndicator.IsRunning = false;
				Opaque.IsVisible = false;
				Console.WriteLine(exc.Message);
				error = new ErrorWarningModal("Error en la conexión a la base de datos, revise la configuración de credenciales del servidor");
				await Navigation.PushModalAsync(error);
			}
			content.Dispose();
			client.Dispose();
		}

		public void almacenLista()
		{
			for (int i = 0; i < almacenes.Count; i++)
			{
				_allItems.Add(new Almacenes
				{
					Almacen = almacenes[i].Almacen,
					id = almacenes[i].id,
					idEmpresa = almacenes[i].idEmpresa
				,
					idSucursal = almacenes[i].idSucursal,
				});
			}
		}

		public async void almacenSelected(Almacenes almacen)
		{
			EmpresaSucursalAct.Almacen = almacen.id;
			EmpresaSucursalAct.Empresa = almacen.idEmpresa;
			EmpresaSucursalAct.Sucursal = almacen.idSucursal;
			UserAct.MultiAlmacen = true;
			UserAct.MultiEmpresa = almacen.multiEmpresa;
			UserAct.MultiSucursal = true;
			await Shell.Current.GoToAsync("///MenuPrincipal");
		}
		#endregion
	}
}

