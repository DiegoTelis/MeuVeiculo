using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MeuVeiculo.View
{
	public partial class LancamentoEvento : ContentPage
	{
        ViewModel.LancamentoEventoViewModel lancamentoEvento;


    public LancamentoEvento (int CodigoVeiculo)
		{
            InitializeComponent();
            lancamentoEvento = new ViewModel.LancamentoEventoViewModel(CodigoVeiculo);
            BindingContext = lancamentoEvento;

        }

        private void btnSalvar_Clicked(object sender, EventArgs e)
        {
            //    for (bool i = lancamentoEvento.podeValidar; lancamentoEvento.podeValidar == false; i = lancamentoEvento.podeValidar)
            //    {
            //        Task.Delay(2);
            //    }
            //    if (!lancamentoEvento.ValidarKM())
            //    { DisplayAlert("Atenção", "Kilometragem inferior à atual do veiculo, confirme a mesma e tente novamente", "OK"); return; }
            //ValUnit.Text = "";
            //QTD.Text = "";


        }
    }
}