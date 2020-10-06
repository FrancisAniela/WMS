using CellariumAndroid.Helpers;
using CellariumAndroid.Models;
using CellariumAndroid.Views;
using CellariumAndroid.Repository;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using Xamarin.Forms;
using System.Threading.Tasks;
using System.Net.Sockets;

namespace CellariumAndroid.ViewModels
{
	public class SrvCfgViewModel : BaseViewModel
	{
		private static  string[] Puertos = PuertoConexion.GetPuertos();
		public SrvCfgViewModel()
		{

		}

		#region Methods

		private static void GuardarCredenciales(string Ip, string Serv, string DBNom, string User, string Pass, string Puerto)
		{
			Server.IpAddress = Ip + Puerto.ToString();
			Server.ServerDB = Serv;
			Server.Database = DBNom;
			Server.UsernameDB = User;
			Server.PasswordDB = Pass;
			
			Settings.IpAdress = Ip;
			Settings.ServerDB = Serv;
			Settings.Database = DBNom;
			Settings.Username = User;
			Settings.Password = Pass;
			Settings.Puerto = Puerto;
		}

		public static async Task pruebaConexion(ActivityIndicator defaultActivityIndicator, string Ip, string Serv, string DBNom, string User, string Pass, BoxView Opaque)
		{
			HttpClient client ;
			ErrorWarningModal error;

			if (!AccesoRed.GetConexion())
			{
				error = new ErrorWarningModal("Debe conectarse a una red local");
				await Navigation.PushModalAsync(error);
				return;
			}

			client = new HttpClient();
			var postData = new List<KeyValuePair<string, string>>();
			client.Timeout = TimeSpan.FromSeconds(10);
			postData.Add(new KeyValuePair<string, string>("server", Serv));
			postData.Add(new KeyValuePair<string, string>("database", DBNom));
			postData.Add(new KeyValuePair<string, string>("usernamesql", User));
			postData.Add(new KeyValuePair<string, string>("passwordsql", Pass));
			var content = new FormUrlEncodedContent(postData);

			for (int i = 0; i < Puertos.Length; i++)
			{
				try
				{
					Opaque.IsVisible = true;
					defaultActivityIndicator.IsRunning = true;

					var response = await client.PostAsync("http://" + Ip + Puertos[i] + "/Cellarium/conexiontry.php", content);
					var jsonresult = response.Content.ReadAsStringAsync().Result;
					var responseObject = JsonConvert.DeserializeObject(jsonresult);

					if (responseObject == null)
					{
						GuardarCredenciales(Ip, Serv, DBNom, User, Pass, Puertos[i]);
						error = new ErrorWarningModal("Conexión exitosa");
						defaultActivityIndicator.IsRunning = false;
						Opaque.IsVisible = false;
						await Navigation.PushModalAsync(error);
						content.Dispose();
						client.Dispose();
						break;
					}
				}

				catch (System.Exception ex)
				{
					Console.WriteLine(ex.Message);
					content.Dispose();
					client.Dispose();

					defaultActivityIndicator.IsRunning = false;
					Opaque.IsVisible = false;
					error = new ErrorWarningModal("Error en la conexión con el servidor");
					await Navigation.PushModalAsync(error);
					break;
				}


			}

			content.Dispose();
			client.Dispose();
			defaultActivityIndicator.IsRunning = false;
			Opaque.IsVisible = false;

		}

			
		#endregion

	}
}
