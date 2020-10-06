using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using CellariumAndroid.ViewModels;
using System.Threading.Tasks;
using CellariumAndroid.Models;
using System.Linq;

namespace CellariumAndroid.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EsperaPortal : ContentPage
    {
        public static EsperaPortalViewModel viewModel;

        public EsperaPortal()
        {
            InitializeComponent();
            BindingContext = viewModel;
            EsperaPortalViewModel.Navigation = Navigation;
            ErrorText.Text = "Por Espera del Portal ";
            Informaciontext.Text = "";
            ProcesoAct();
        }

        public static void ProcesoAct() 
        {
            //Verificacion del sts
            viewModel.EstatusActualUnidadCargaAsync()
                .ContinueWith((arg) => {                        
                    Device.BeginInvokeOnMainThread(() => {
                        if (EstatusReadOnly.EstatusActualProceso != EstatusReadOnly.Transito)
                        {
                            viewModel.LblInformation = "Proceso Finalizado";
                            viewModel.Visible = true;
                            viewModel.IsRunning = false;
                        }
                        else
                            ProcesoAct();
                    });
                });
        }

        private async void Continuar_Clicked(object sender, EventArgs e)
        {
            await viewModel.SetAccion(Opaque, defaultActivityIndicatorReload);

        }
        
        internal void OnResume()
        {

        }
        protected override void OnAppearing()
        {
            TareaViewModel.portal.IsRunning = true;
            TareaViewModel.portal.Visible = false;
            ProcesoAct();
            base.OnAppearing();

        }

    }

}