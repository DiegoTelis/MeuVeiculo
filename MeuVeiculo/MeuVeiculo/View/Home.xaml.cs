using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MeuVeiculo.View
{
    public partial class Home : MasterDetailPage
    {
        public Home()
        {
            InitializeComponent();

            masterPage.ListView.ItemSelected += OnItemSelected;

        }

        void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = e.SelectedItem as View.MasterPageItem;
            if (item != null)
                //{
                //    Type a = item.TargetType;
                //    Type b = typeof(HistoricoEventosPage);

                if (item.TargetType == typeof(LancamentoEvento))
                {
                    if (Convert.ToInt32(masterPage.CodigoVeiculo) > 0)
                    {
                        Detail = new NavigationPage(new LancamentoEvento(masterPage.CodigoVeiculo));
                    }
                    else
                    { DisplayAlert("Atenção!", "Selecionar Veiculo para realizar a ação", "OK"); }
                }
            //retirado
                else if (item.TargetType == typeof(HistoricoEventosPage))
                {
                    if (Convert.ToInt32(masterPage.CodigoVeiculo) > 0)
                    {
                        Detail = new NavigationPage(new HistoricoEventosPage(masterPage.CodigoVeiculo, 2));
                    }
                    else
                    { DisplayAlert("Atenção!", "Selecionar Veiculo para realizar a ação", "OK"); }
                }
                //ADD
                else if (item.TargetType == typeof(HistoricoPage))
                {
                    if (Convert.ToInt32(masterPage.CodigoVeiculo) > 0)
                    {
                        Detail = new NavigationPage(new HistoricoPage(masterPage.CodigoVeiculo));
                    }
                    else
                    { Detail = new NavigationPage(new HistoricoPage(0)); }
                }
                else
                {
                    Detail = new NavigationPage((Page)Activator.CreateInstance(item.TargetType));
                }
               

            //var a = item.TargetType;
            //Detail = new NavigationPage(new MeuVeiculoPage());
            masterPage.ListView.SelectedItem = null;
            IsPresented = false;

            //Detail = new NavigationPage.(Activator.CreateInstance())
        }
    }
}
