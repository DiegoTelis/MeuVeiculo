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
    public partial class HistoricoEventosPage : ContentPage
    {
        Model.AcessoDados.AcessoBD acesso = new Model.AcessoDados.AcessoBD();
        ViewModel.HitoricoEventosViewModel historicoEventosVM = new ViewModel.HitoricoEventosViewModel();
        //public HistoricoEventosPage(int CodVei)
        int CodVeiculo, TipoClasse;
        public HistoricoEventosPage(int CodVei, int TipoClasse)
        {
            this.CodVeiculo = CodVei;
            this.TipoClasse = TipoClasse;

            InitializeComponent();
            BindingContext = historicoEventosVM;

            //List<Model.Eventos> eventList = new List<Model.Eventos>();
            //eventList = historicoEventosVM.EventosHist.ToList();
            //Definindo Tipo de Classe - Movimento

            //if (TipoClasse == 1)
            //    EventosHistListView.ItemsSource = eventList.ToList();
            //else if (TipoClasse == 2)
            //    EventosHistListView.ItemsSource = eventList.Where(c => c.CodVeiculo == CodVei).ToList();
            //else if (TipoClasse == 3)
            //    EventosHistListView.ItemsSource = eventList.Where(c => c.CodVeiculo == CodVei && c.Tipo == "Combustivel").ToList();

            ////EventosHistListView.ItemsSource = historicoEventosVM.retornaLista(CodVei, TipoClasse);

            historicoEventosVM.PreencheLista(CodVei, TipoClasse);

            EventosHistListView.ItemSelected += EventosHistListView_ItemSelected;
        }

        private void EventosHistListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            
            Model.Eventos eventoSelecionado = (Model.Eventos)e.SelectedItem;
            if (eventoSelecionado == null) return;

            eventoSelecionado = historicoEventosVM.RetornaEvento(eventoSelecionado.Codigo);
            Navigation.PushAsync(new HistoricoEventosDetailPage(eventoSelecionado));
            EventosHistListView.SelectedItem = null;
        }

        private void Detalhes_Clicked(object sender, EventArgs e)
        {
            
        }

        private void Deletar_Clicked(object sender, EventArgs e)
        {
            var a = sender;
            var b = e;
        }

        private void EventosHistListView_ItemAppearing(object sender, ItemVisibilityEventArgs e)
        {
            var items = this.EventosHistListView.ItemsSource as IList<Model.Eventos>;
            if (items != null && e.Item == items[items.Count - 1])
            {
                //Atualizar grid Add itens
                historicoEventosVM.PreencheLista(this.CodVeiculo, this.TipoClasse);
            }
        }
    }
}