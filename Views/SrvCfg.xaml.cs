using CellariumAndroid.Helpers;
using CellariumAndroid.ViewModels;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CellariumAndroid.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SrvCfg : ContentPage
	{
		public static bool Connnected;
		SrvCfgViewModel serverdata = new SrvCfgViewModel();

		//public bool LoadingIsVisible { get; set; }

		public SrvCfg()
		{
			InitializeComponent();

			if (Settings.IpAdress != string.Empty && Settings.ServerDB != string.Empty && Settings.Database != string.Empty && Settings.Username != string.Empty && Settings.Password != string.Empty)
			{
				IpAdress.Text = Settings.IpAdress;
				ServerText.Text = Settings.ServerDB;
				Database.Text = Settings.Database;
				Username.Text = Settings.Username;
				Password.Text = Settings.Password;
			}
		}


		private void SaveButton_Clicked(object sender, EventArgs e)
		{
			if (ValidarInformacionDB())
				//LoadingIsVisible = true;

				Device.InvokeOnMainThreadAsync 
					( async () =>
						await SrvCfgViewModel.pruebaConexion(defaultActivityIndicator, IpAdress.Text , ServerText.Text, Database.Text, Username.Text, Password.Text, Opaque)
					);
				//LoadingIsVisible = false;
		}





		private bool ValidarInformacionDB() 
		{
			bool confirmar= false;
			if (IpAdress.Text.IsNullOrEmpty() && ServerText.Text.IsNullOrEmpty() && Database.Text.IsNullOrEmpty() && Username.Text.IsNullOrEmpty() && Password.Text.IsNullOrEmpty())
			{
				ErrorWarningModal modal = new ErrorWarningModal("Todos los campos están vacíos, debe introducir un valor en cada uno de ellos");
				Navigation.PushModalAsync(modal);
			}
			else if (IpAdress.Text.IsNullOrEmpty())
			{
				ErrorWarningModal modal = new ErrorWarningModal("Debe introducir una dirección IP");
				Navigation.PushModalAsync(modal);
			}
			else if (ServerText.Text.IsNullOrEmpty())
			{
				ErrorWarningModal modal = new ErrorWarningModal("Debe introducir un servidor");
				Navigation.PushModalAsync(modal);
			}
			else if (Database.Text.IsNullOrEmpty())
			{
				ErrorWarningModal modal = new ErrorWarningModal("Debe introducir una base de datos");
				Navigation.PushModalAsync(modal);
			}
			else if (Username.Text.IsNullOrEmpty())
			{
				ErrorWarningModal modal = new ErrorWarningModal("Debe introducir un nombre de usuario");
				Navigation.PushModalAsync(modal);
			}
			else if (Password.Text.IsNullOrEmpty())
			{
				ErrorWarningModal modal = new ErrorWarningModal("Debe introducir una contraseña");
				Navigation.PushModalAsync(modal);
			}
			else if (IpAdress.Text != null && ServerText.Text != null && Database.Text != null && Username.Text != null && Password.Text != null)
			{
				confirmar = true;
			}
			return confirmar;
		}

		private void IpAdress_Completed(object sender, EventArgs e)
		{
			if(IpAdress.Text.IsNullOrEmpty())
				ServerText.Focus();
			
		}

		private void ServerText_Completed(object sender, EventArgs e)
		{
			if (ServerText.Text.IsNullOrEmpty())
				Database.Focus();
			
		}

		private void Database_Completed(object sender, EventArgs e)
		{
			if (Database.Text.IsNullOrEmpty())
				Username.Focus();
			
		}

		private void Username_Completed(object sender, EventArgs e)
		{
			if (Username.Text.IsNullOrEmpty())
				Password.Focus();
			
		}
	}
}