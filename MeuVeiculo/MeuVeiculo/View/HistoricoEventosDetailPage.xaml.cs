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
    public partial class HistoricoEventosDetailPage : ContentPage
    {
        bool Valores = true;
        Model.Eventos ev = new Model.Eventos();
        public HistoricoEventosDetailPage(Model.Eventos eventoSelecionado)
        {
            try
            {
                this.ev.Codigo = eventoSelecionado.Codigo;
                this.ev.Tipo = eventoSelecionado.Tipo;
                this.ev.CodVeiculo = eventoSelecionado.CodVeiculo;
                this.ev.Data = eventoSelecionado.Data;
                this.ev.Observacao = eventoSelecionado.Observacao;
                this.ev.Kilometros = eventoSelecionado.Kilometros;
                this.ev.Quantidade = eventoSelecionado.Quantidade;
                this.ev.ValorUnitario = eventoSelecionado.ValorUnitario;
                this.ev.TanqueCheio = eventoSelecionado.TanqueCheio;
                this.ev.Total = eventoSelecionado.Total;

                
                InitializeComponent();
                BindingContext = eventoSelecionado;

                Valores = eventoSelecionado.Tipo == "Saida" || eventoSelecionado.Tipo == "Entrando Reserva" ? false : true;

                // atribuindo valores passado por parametros. Desabilitado Modo TwoWay do Binding 
                lblKilometragem.Text = ev.Kilometros.ToString();
                lblQuantidade.Text = ev.Quantidade == 0 ? "" : ev.Quantidade.ToString();
                lblValor.Text = ev.ValorUnitario == 0 ? "" : ev.ValorUnitario.ToString();
                lblTotal.Text = ev.Total == 0 ? "": ev.Total.ToString();
                //

                this.btnSalvar.IsVisible = false;
            }
            catch (Exception e) { }
        }


        private void btnSalvar_Clicked(object sender, EventArgs e)
        {
            Model.AcessoDados.AcessoBD acesso = new Model.AcessoDados.AcessoBD();
            ev.CodVeiculo = Convert.ToInt32(this.lblCodVeiculo.Text);
            ev.Data = this.dpData.Date == ev.Data.Date ? ev.Data: this.dpData.Date;
            ev.Observacao = lblObservacao.Text;
            ev.Kilometros = ToDouble(lblKilometragem.Text);
            ev.ValorUnitario = ToDouble(lblValor.Text);
            ev.Quantidade = ToDouble(lblQuantidade.Text);
            ev.TanqueCheio = Tank.IsToggled;
            ev.Total = ToDouble(lblTotal.Text);

            acesso.AlterarEvento(ev);
            Application.Current.MainPage.DisplayAlert("Alteração", "Alteração realizada com sucesso.", "OK");
            Navigation.RemovePage(this);
        }

        private void lblCodVeiculo_TextChanged(object sender, TextChangedEventArgs e)
        {
            HabilitaSalvar(ev.CodVeiculo, e.NewTextValue);
        }

        private void dpData_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            var oq = e.ToString();
        }

        private void lblObservacao_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.btnSalvar.IsVisible = ev.Observacao != e.NewTextValue ? true : false;
        }

        private void lblKilometragem_TextChanged(object sender, TextChangedEventArgs e)
        {
            HabilitaSalvar(ev.Kilometros, e.NewTextValue);
        }

        private void lblValor_TextChanged(object sender, TextChangedEventArgs e)
        {
            HabilitaSalvar(ev.ValorUnitario, e.NewTextValue);
        }
        string a="0";
        private void lblQuantidade_TextChanged(object sender, TextChangedEventArgs e)
        {
            //if (!a.Equals("0")) return;
            HabilitaSalvar(ev.Quantidade, e.NewTextValue);
            //a = e.NewTextValue;
        }

        private void lblTotal_TextChanged(object sender, TextChangedEventArgs e)
        {
            HabilitaSalvar(ev.Total, e.NewTextValue);
        }

        private void HabilitaSalvar(double valorBase, string novoValor)
        {
            this.btnSalvar.IsVisible = (valorBase.ToString() != novoValor) ? true : false;
        }
        private double ToDouble(string valor)
        {
            if (string.IsNullOrEmpty(valor))
                return 0;

            return Convert.ToDouble(valor) > 0 ? Convert.ToDouble(valor.Replace('.', ',')) : 0;
        }

      
    }
}