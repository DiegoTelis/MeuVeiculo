using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using MeuVeiculo.Model;
using Xamarin.Forms;



namespace MeuVeiculo.ViewModel
{
    public class LancamentoEventoViewModel: BaseObservavel
    {
        public ObservableCollection<Tipos> TiposPick { get; set; }
        public Command SalvarCommand{ get; set; }


        private bool valores;
        public bool Valores
        {
            get { return valores; }
            set { valores = value; OnProPertyChanged(); }
        }
        private bool habilitaTanqueCheio;
        public bool HabilitaTanqueCheio
        {
            get { return habilitaTanqueCheio; }
            set { habilitaTanqueCheio = value; OnProPertyChanged(); }
        }
        //public bool validaLitragem;
        //public bool podeValidar;

        //retornar veiculo para tela de Lançamento de Evento
        Model.AcessoDados.AcessoBD acess = new Model.AcessoDados.AcessoBD();
        public string VeiculoToString
        {
            get { return acess.RetornarVeiculo(this.codVeiculo).VeiculoToString; }
            //set { veiculoToString = 
            //    OnProPertyChanged(); }
        }

        #region Propriedades Evento
        private int codVeiculo;
        public int CodVeiculo
        {
            get { return codVeiculo; }
            set { codVeiculo = value; OnProPertyChanged(); }
        }

        private Tipos tipo;
        public Tipos Tipo
        {
            get { return tipo; }
            set
            {
                tipo = value;
                OnProPertyChanged();

                if (value.Codigo == 1)
                {
                    Model.AcessoDados.AcessoBD acesso = new Model.AcessoDados.AcessoBD();
                    Veiculo vei = acesso.RetornarVeiculo(this.CodVeiculo);
                    HabilitaTanqueCheio = (vei.Litragem + Convert.ToDouble(Quantidade)) > vei.LitragemBase ? true : false;
                    TanqueCheioSelecionado = HabilitaTanqueCheio ? TanqueCheioSelecionado : false;
                    Valores = true;
                }
                else
                    HabilitaTanqueCheio = false;

                Valores = value.Codigo == 6 || value.Codigo == 7 ? false : true;
            }
        }
        
        private DateTime data;
        public DateTime Data
        {
            get { return data; }
            set
            {
                data = value;
                OnProPertyChanged();
            }
        }

        private string observacao;
        public string Observacao
        {
            get { return observacao; }
            set
            {
                observacao = value;
                OnProPertyChanged();
            }
        }

        private string kilometragem;
        public string Kilometragem
        {
            get { return kilometragem; }
            set
            {
                kilometragem = value;
                OnProPertyChanged();
            }
        }

        private string valorUnitario;
        public string ValorUnitario
        {
            get { return valorUnitario.ToString(); }
            set
            {
                try
                {

                    double a = ToDouble(value);
                    valorUnitario = a.ToString();
                    OnProPertyChanged();

                    Total = (a * ToDouble(Quantidade)).ToString();

                }
                catch
                {
                    valorUnitario = "0";
                }
            }
        }
        private string quantidade;
        public string Quantidade
        {
            get { return quantidade.ToString(); }
            set
            {
                try
                {
                    //if (string.IsNullOrEmpty(value))
                    //{
                    //    quantidade = "0";
                    //    return;
                    //}
                    if (Tipo.Codigo == 1)
                    {
                        Model.AcessoDados.AcessoBD acesso = new Model.AcessoDados.AcessoBD();
                        Veiculo vei = acesso.RetornarVeiculo(this.CodVeiculo);
                        HabilitaTanqueCheio = (vei.Litragem + ToDouble(value)) > vei.LitragemBase ? true : false;
                        TanqueCheioSelecionado = HabilitaTanqueCheio ? TanqueCheioSelecionado : false;
                    }

                    double a = ToDouble(value);

                    quantidade = a.ToString();
                    OnProPertyChanged();

                    Total = (a * ToDouble(ValorUnitario)).ToString();


                    //////
                    
                }
                catch
                {
                    quantidade = "0";
                }
            }
        }
        private string total;
        public string Total
        {
            get { return total; }
            set { total = value; OnProPertyChanged(); }
        }
        private bool tanqueCheioSelecionado;
        public bool TanqueCheioSelecionado
        {
            get { return tanqueCheioSelecionado; }
            set { tanqueCheioSelecionado = value; OnProPertyChanged(); }
        }

        #endregion

        private double ToDouble(string valor)
        {
            if (string.IsNullOrEmpty(valor))
                return  0;

            return Convert.ToDouble(valor) > 0 ? Convert.ToDouble(valor.Replace('.', ',')) : 0;
        }

        public LancamentoEventoViewModel(int CodVeic)
        {
            this.CodVeiculo = CodVeic;
            TiposPick = new ObservableCollection<Tipos>();
            SalvarCommand = new Command(SalvarEvento);
            data = DateTime.Today;
            //
            HabilitaTanqueCheio = false;
            Valores = true;

            TiposPick.Add(new Model.Tipos { Codigo = 1, Descricao = "Combustivel" });
            TiposPick.Add(new Model.Tipos { Codigo = 2, Descricao = "Oficina" });
            TiposPick.Add(new Model.Tipos { Codigo = 3, Descricao = "Limpeza" });
            TiposPick.Add(new Model.Tipos { Codigo = 4, Descricao = "TrocaDePeca" });
            TiposPick.Add(new Model.Tipos { Codigo = 5, Descricao = "Revisao" });
            TiposPick.Add(new Model.Tipos { Codigo = 6, Descricao = "Saida" });
            TiposPick.Add(new Model.Tipos { Codigo = 7, Descricao = "Entrando Reserva" });
            TiposPick.Add(new Model.Tipos { Codigo = 8, Descricao = "Outro" });

        }

        private void SalvarEvento()
        {
            //validaKM = false; podeValidar = false; validaLitragem = false;

            Eventos evento = new Eventos();
            Model.AcessoDados.AcessoBD acesso = new Model.AcessoDados.AcessoBD();

            evento.CodVeiculo = this.CodVeiculo;
            evento.Tipo = this.Tipo.Descricao;
            DateTime dd = this.data;
            DateTime da = DateTime.Now;
            evento.Data = (this.Data.ToString("dd/MM/yyyy") == DateTime.Now.ToString("dd/MM/yyyy")) ? DateTime.Now : this.Data;
                //Convert.ToDateTime(this.Data, System.Globalization.CultureInfo.InvariantCulture);
            evento.Observacao = this.Observacao;
            evento.Kilometros = ToDouble(this.Kilometragem);
            evento.ValorUnitario = ToDouble(this.ValorUnitario);
            evento.Quantidade = ToDouble(this.Quantidade);
            evento.Total = ToDouble(this.Total);
            evento.TanqueCheio = TanqueCheioSelecionado;

            Veiculo veiculo = new Veiculo();
            veiculo = acesso.RetornarVeiculo(this.CodVeiculo);

            if(evento.Kilometros < veiculo.Kilometragem)
            {
                Application.Current.MainPage.DisplayAlert("Atenção", string.Format("Kilometragem inferior à atual do veiculo: '{0}' \nAjuste na tela Detalhes Veiculo", veiculo.Kilometragem), "OK");
                return;
            }

            //if (evento.Tipo == "Combustivel" && (evento.Quantidade + veiculo.Litragem) > veiculo.LitragemBase && !HabilitaTanqueCheio)
            //{
            //    var a = Application.Current.MainPage.DisplayAlert("Atenção", "Se o tanque foi completado marque a caixa de seleção a seguir", "OK");
            //    HabilitaTanqueCheio = true;
            //    return;
            //}

            acesso.InserirEvento(evento);
            AtualizarVeiculo(Tipo.Codigo, evento.Kilometros, evento.Quantidade, evento.TanqueCheio);

            Application.Current.MainPage.DisplayAlert("GRAVAÇÂO", string.Format("Lançamento de {0}, feito com sucesso",Tipo.Descricao) , "OK");
            LimparCampos();


        }

        private void LimparCampos()
        {
            this.Data = DateTime.Today;
            this.Observacao = "";
            this.Kilometragem = "";
            this.ValorUnitario = "";
            this.Quantidade = "";
            this.Total = "";
        }

        private void AtualizarVeiculo(int Tipo, double KM, double QTD, bool TanqueCheio)
        {
            Model.AcessoDados.AcessoBD acesso = new Model.AcessoDados.AcessoBD();
            Veiculo veiculo = acesso.RetornarVeiculo(this.CodVeiculo);

            double result=0;
            //Se Abastecimento
            if (Tipo == 1)
            {
                ////Atualizar Litragem Atual
                //if (veiculo.Litragem + QTD >= veiculo.LitragemBase)
                ////Alterado quando marcado tanque cheio Antes => A base marcada no cadastro; agora => A quantidade somada a atual
                //{ veiculo.Litragem = TanqueCheio ? (veiculo.Litragem + ToDouble(this.Quantidade)) : veiculo.LitragemBase * 0.95; }
                //else
                //{
                    veiculo.Litragem = QTD;
                //}

                //Atualizar Consumo medio
                if (TanqueCheio)
                {
                    if (acesso.Retornar_UltEventoAbastecimento() != null)
                    {
                        double QTDList = QTD;
                        Eventos evento = new Eventos();
                        evento = acesso.Retornar_UltEventoAbastecimento();

                        QTDList += acesso.RetornarEventosLista_Abastecimento().Where(c => c.Data > evento.Data).Sum(c => c.Quantidade);
                        veiculo.ConsumoMedio = (KM - evento.Kilometros) / QTDList;
                    }
                }
            }
            //Atualizar Litragem Atual  --  Definindo reserva
            else if (Tipo == 7)
            {
                //veiculo.Litragem = veiculo.Reserva;
                veiculo.Litragem = veiculo.Reserva;
                //mudar pra ult.vez entrando na reserva
                if (acesso.RetornarUltEvento_EntrouReserva() != null)
                {
                    //double QTDList = QTD;
                    //o correto vai ser,..
                    // double QTDList = veiculo.Reserva;
                    double QTDList = veiculo.Reserva;

                    Eventos evento = new Eventos();
                    evento = acesso.RetornarUltEvento_EntrouReserva();

                    QTDList += acesso.RetornarEventosLista_Abastecimento().Where(c => c.Data > evento.Data).Sum(c => c.Quantidade);
                    veiculo.ConsumoMedio = (KM - evento.Kilometros) / QTDList;
                }
            }
            else
            {
                result = veiculo.Litragem - ((KM - veiculo.Kilometragem) / veiculo.ConsumoMedio);
                veiculo.Litragem = (result <= 0) ? 0.1D : result;
            }
            veiculo.Kilometragem = KM;

            acesso.AlterarVeiculo(veiculo);

        }
    }
}
