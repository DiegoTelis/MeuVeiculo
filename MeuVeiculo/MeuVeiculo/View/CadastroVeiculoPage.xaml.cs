using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MeuVeiculo.View
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CadastroVeiculoPage : ContentPage
	{
        ViewModel.CadastroVeiculoViewModel cadastroVeiculoVM = new ViewModel.CadastroVeiculoViewModel();
		public CadastroVeiculoPage ()
		{
			InitializeComponent ();

            BindingContext = cadastroVeiculoVM;


        }


	}
}