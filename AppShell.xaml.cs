using CellariumAndroid.ViewModels;
using CellariumAndroid.Views;
using CellariumAndroid.Views.Recepcion;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CellariumAndroid
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AppShell : Shell
	{
		public AppShell()
		{
			InitializeComponent();
			BaseViewModel.Navigation = Navigation;
			Routing.RegisterRoute("PaseProduccion", typeof(PaseProduccion));
			Routing.RegisterRoute("Almacenaje", typeof(Almacenaje));
			//Routing.RegisterRoute("AlmacenajeArticulo", typeof(AlmacenajeArticulo));
			Routing.RegisterRoute("PaseProduccionUbiCuna", typeof(PaseProduccionUbiCuna));
			Routing.RegisterRoute("AuditoriaCuna", typeof(AuditoriaCuna));
			Routing.RegisterRoute("IdentificarUnidadCarga",typeof(IdentificarUnidadCarga));
			Routing.RegisterRoute("Despacho_Cuna", typeof(IdentificarCunaDespacho));
            Routing.RegisterRoute("Portal", typeof(EsperaPortal));
			Routing.RegisterRoute("FinalizarDespacho", typeof(FinalizarDespacho));
			//Routing.RegisterRoute("ConfirmarPicking", typeof(ConfirmarPicking));
			Routing.RegisterRoute("TomaInventario", typeof(TomaDeInventario));
            Routing.RegisterRoute("Picking", typeof(Picking));
            Routing.RegisterRoute("Predespacho",typeof(Predespacho));
            Routing.RegisterRoute("DespachoArticulo", typeof(DespachoArticulo));
            Routing.RegisterRoute("OC", typeof(OrdenesDisponibles));
			Routing.RegisterRoute("RollosCuna", typeof(RollosCuna));
            Routing.RegisterRoute("ArticuloDespacho", typeof(ArticuloCunaDespacho));
			Routing.RegisterRoute("DetalleOrdenRecepcion", typeof(DetalleOrdenRecepcion));
			Routing.RegisterRoute("CalidadRecepcion", typeof(CalidadRecepcion));
		}
    }
}