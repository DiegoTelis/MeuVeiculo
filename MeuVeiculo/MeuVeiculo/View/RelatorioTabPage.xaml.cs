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
    public partial class RelatorioTabPage : TabbedPage
    {
        ViewModel.RelatorioTabViewModel relatorio = new ViewModel.RelatorioTabViewModel();
        public RelatorioTabPage ()
        {
            InitializeComponent();
            BindingContext = relatorio;
        }

        private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            listVeiw.SelectedItem = null;
        }

        private void lstViewMensal_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            lstViewMensal.SelectedItem = null;
        }
    }
}