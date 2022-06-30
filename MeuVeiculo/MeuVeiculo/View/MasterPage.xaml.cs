using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MeuVeiculo.View
{
    public partial class MasterPage : ContentPage
    {
        public ListView ListView { get { return listView; } }

        public int CodigoVeiculo
        {
            get
            {
                Model.Veiculo vei = (Model.Veiculo)this.pic.SelectedItem;
                return vei == null ? 0 : vei.CodVeiculo;
            }
        }

        ViewModel.MeuVeiculoViewModel meuVeiculoVM = new ViewModel.MeuVeiculoViewModel();
        public MasterPage()
        {
            InitializeComponent();

            BindingContext = meuVeiculoVM;

            this.pic.SelectedIndex = 0;
            //View.MeuVeiculoPage mg = new MeuVeiculoPage();
            //Lanc.TargetType = mg;.
        }




    }
}