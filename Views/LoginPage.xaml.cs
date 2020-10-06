using CellariumAndroid.Helpers;
using CellariumAndroid.Models;
using CellariumAndroid.ViewModels;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CellariumAndroid.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LoginPage : ContentPage
	{
		public LoginPage()
		{
			InitializeComponent();
			LoginViewModel.Navigation = Navigation;

			if(Settings.IpAdress != string.Empty && Settings.ServerDB != string.Empty && Settings.Database != string.Empty && Settings.Username != string.Empty && Settings.Password != string.Empty)
			{
				Server.IpAddress = Settings.IpAdress + Settings.Puerto;
				Server.ServerDB = Settings.ServerDB;
				Server.Database = Settings.Database;
				Server.UsernameDB = Settings.Username;
				Server.PasswordDB = Settings.Password;
			}
		}

		private  void  LoginButton_Clicked(object sender, EventArgs e)
		{
			if (ValidarCampos() && ValidarCredencialesBD())
			{
				LoginViewModel.UsernameText = usernameText;
				LoginViewModel.passwordText = passwordText;
				LoginViewModel.pruebaConexion(Server.IpAddress, Server.ServerDB, Server.Database, Server.UsernameDB, Server.PasswordDB);
			}
		}

		private   void LoginButtonSalir_Clicked(object sender, EventArgs e) 
		{
			if (Device.RuntimePlatform == Device.Android)
			{
				DependencyService.Get<IAndroidMethods>().CloseApp();
			}
		}

		private bool ValidarCredencialesBD() 
		{
			if (Settings.IpAdress.IsNullOrEmpty() || Settings.ServerDB.IsNullOrEmpty() || Settings.Database.IsNullOrEmpty() || Settings.Username.IsNullOrEmpty() || Settings.Password.IsNullOrEmpty())
			{
				ErrorWarningModal error = new ErrorWarningModal("Debe introducir y guardar credenciales del servidor");
				this.Navigation.PushModalAsync(error);
				return false;
			}
			return true;

		}

		private bool ValidarCampos()
		{
			if (usernameText.Text.IsNullOrEmpty())
			{
				ErrorWarningModal error = new ErrorWarningModal("Debe introducir un nombre de usuario");
				this.Navigation.PushModalAsync(error);
				return false;
			}
			else if (passwordText.Text.IsNullOrEmpty())
			{
				ErrorWarningModal error = new ErrorWarningModal("Debe introducir una contraseña");
				this.Navigation.PushModalAsync(error);
				return false;
			}
			return true;
		}

		private void usernameText_Completed(object sender, EventArgs e)
		{
			if(!usernameText.Text.IsNullOrEmpty())
			{
				passwordText.Focus();
			}
		}

	}
}