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
    public partial class HistoricoPage : TabbedPage
    {
        int TodosMov = 1, VeiculoMov = 2, CombustivelMov = 3;
        public HistoricoPage (int CodVeiculo)
        {
            InitializeComponent();
            //var todosMovNavigationPage = new NavigationPage(new HistoricoEventosPage(CodVeiculo));
            //todosMovNavigationPage.Title = "Movimentação Geral";

            //var veicMovNavigationPage = new NavigationPage(new HistoricoEventosPage(CodVeiculo, VeiculoMov));
            //veicMovNavigationPage.Icon = "schedule.png";
            //veicMovNavigationPage.Title = "Movimentação do Veiculo";

            //var combMovNavigationPage = new NavigationPage(new HistoricoEventosPage(CodVeiculo, CombustivelMov));
            //combMovNavigationPage.Icon = "schedule.png";
            //combMovNavigationPage.Title = "Movimentação de combustivel";

            ////Children.Add(new TodayPageCS());
            Children.Add(new HistoricoEventosPage(CodVeiculo, TodosMov) { Title="Todos lançamentos"});
            Children.Add(new HistoricoEventosPage(CodVeiculo, VeiculoMov) { Title = "Todos do Veiculo" });
            Children.Add(new HistoricoEventosPage(CodVeiculo, CombustivelMov) { Title = "Abastecimentos do Veiculo" });
        }
    }
}