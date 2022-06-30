using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MeuVeiculo.View
{
    public partial class MeuVeiculoPage : ContentPage
    {
        ViewModel.MeuVeiculoViewModel meuVeiculoVM = new ViewModel.MeuVeiculoViewModel();
        public MeuVeiculoPage()
        {
            InitializeComponent();
            BindingContext = meuVeiculoVM;

            VeiculosListView.ItemSelected += VeiculosListView_ItemSelect;

        }

        private void VeiculosListView_ItemSelect(object sender, SelectedItemChangedEventArgs e)
        {
            Model.Veiculo vei = (Model.Veiculo)e.SelectedItem;
            if (vei == null) return;

            //MasterPage mPage = new MasterPage();
            //mPage.LabelNome = "testess";
            Model.AcessoDados.AcessoBD ac = new Model.AcessoDados.AcessoBD();

            Model.Veiculo VeiculoSelecionado = ac.RetornarVeiculo(vei.CodVeiculo);
            Navigation.PushAsync(new MeuVeiculoDetailPage(VeiculoSelecionado));
            VeiculosListView.SelectedItem = null;
        }

        private void Detalhes_Clicked(object sender, EventArgs e)
        {
            try
            {
                var mi = ((MenuItem)sender);
                var item = mi.CommandParameter;

                //Jogar para pagina de detalhes do Veiculo
                var item2 = (Model.Veiculo)mi.CommandParameter;
                //var t2 = item2.Marca;
                //Debug.WriteLine("Mostrnado valor:" + t2);

                Navigation.PushAsync(new MeuVeiculoDetailPage(item2));

            }
            catch (Exception ex)
            { Debug.WriteLine("Detalhes: " + ex.Message); }
        }

        private void Delete_Clicked(object sender, EventArgs e)
        {
            var mi = (MenuItem)sender;
            Model.Veiculo veiculo = (Model.Veiculo)mi.CommandParameter;

            meuVeiculoVM.ApagarVeiculo(veiculo);
        }
    }
}