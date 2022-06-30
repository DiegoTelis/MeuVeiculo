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
    public partial class MeuVeiculoDetailPage : ContentPage
    {
        Model.Veiculo vei = new Model.Veiculo();
        public MeuVeiculoDetailPage(Model.Veiculo veiculoSelecionado)
        {
            this.vei.CodVeiculo = veiculoSelecionado.CodVeiculo;
            this.vei.Tipo = veiculoSelecionado.Tipo;
            this.vei.Marca = veiculoSelecionado.Marca;
            this.vei.Modelo = veiculoSelecionado.Modelo;
            this.vei.Ano = veiculoSelecionado.Ano;
            this.vei.Valor = veiculoSelecionado.Valor;
            this.vei.Litragem = veiculoSelecionado.Litragem;
            this.vei.LitragemBase = veiculoSelecionado.LitragemBase;
            this.vei.Reserva = veiculoSelecionado.Reserva;
            this.vei.ConsumoMedio = veiculoSelecionado.ConsumoMedio;
            this.vei.Kilometragem = veiculoSelecionado.Kilometragem;

            InitializeComponent();

            this.Stack.BindingContext = veiculoSelecionado;
            this.valor.Text = vei.Valor.ToString();
            this.litragemAt.Text = vei.Litragem.ToString();
            this.litragemTot.Text = vei.LitragemBase.ToString();
            this.reserva.Text = vei.Reserva.ToString();
            this.consumoMedio.Text = vei.ConsumoMedio.ToString();
            this.kilometragem.Text = vei.Kilometragem.ToString();

            this.Salvar.IsVisible = false;
        }
        private void valor_TextChanged(object sender, TextChangedEventArgs e)
        {
            HabilitaSalvar(vei.Valor, e.NewTextValue);
        }

        private void litragemAt_TextChanged(object sender, TextChangedEventArgs e)
        {
            HabilitaSalvar(vei.Litragem, e.NewTextValue);
        }

        private void litragemTot_TextChanged(object sender, TextChangedEventArgs e)
        {
            HabilitaSalvar(vei.LitragemBase, e.NewTextValue);
        }

        private void reserva_TextChanged(object sender, TextChangedEventArgs e)
        {
            HabilitaSalvar(vei.Reserva, e.NewTextValue);
        }

        private void consumoMedio_TextChanged(object sender, TextChangedEventArgs e)
        {
            HabilitaSalvar(vei.ConsumoMedio, e.NewTextValue);
        }

        private void kilometragem_TextChanged(object sender, TextChangedEventArgs e)
        {
            HabilitaSalvar(vei.Kilometragem, e.NewTextValue);
            //vei.Kilometragem = ToDouble(e.NewTextValue);
        }
        private double ToDouble(string valor)
        {
            if (string.IsNullOrEmpty(valor))
                return 0;

            return Convert.ToDouble(valor) > 0 ? Convert.ToDouble(valor.Replace('.', ',')) : 0;
        }
        private void HabilitaSalvar(double valorBase, string novoValor)
        {
            this.Salvar.IsVisible = (valorBase != ToDouble(novoValor)) ? true : false;
        }

        private void Salvar_Clicked(object sender, EventArgs e)
        {
            Model.AcessoDados.AcessoBD acesso = new Model.AcessoDados.AcessoBD();
            vei.Valor = ToDouble(this.valor.Text);
            vei.Litragem = ToDouble(this.litragemAt.Text);
            vei.LitragemBase = ToDouble(this.litragemTot.Text);
            vei.Reserva = ToDouble(this.reserva.Text);
            vei.ConsumoMedio = ToDouble(this.consumoMedio.Text);
            vei.Kilometragem = ToDouble(this.kilometragem.Text);

            acesso.AlterarVeiculo(vei);

            LimparCampos();
            Application.Current.MainPage.DisplayAlert("Alteração", string.Format("Alteração do {0}, feito com sucesso!", vei.VeiculoToString), "OK");
        }
        private void LimparCampos()
        {
            this.valor.Text = "";
            this.litragemAt.Text = "";
            this.litragemTot.Text = "";
            this.reserva.Text = "";
            this.consumoMedio.Text = "";
            this.kilometragem.Text = "";
        }
    }
}