using MeuVeiculo.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MeuVeiculo.ViewModel
{
    public class CadastroVeiculoViewModel : BaseObservavel
    {
        Dictionary<string, List<string>> Veiculos;
        public Command SalvarVeiculoCommand { get; set; }
        public ObservableCollection<string> TipoList { get; set; }
        public ObservableCollection<string> MarcasList { get; set; }
        public ObservableCollection<string> ModelosList { get; set; }
        public ObservableCollection<string> AnoList { get; set; }

        private string tipoSelecionado;
        public string TipoSelecionado
        {
            get { return tipoSelecionado; }
            set
            {
                tipoSelecionado = value;
                Veiculos.Clear();
                MarcasList.Clear();
                ModelosList.Clear();
                if (value== "CARRO")
                {
                    CarregarCarros();
                }
                else if(value =="MOTO")
                {
                    CarregarMotos();
                }
                else { OnProPertyChanged(); return; }

                foreach (string item in Veiculos.Keys.ToList())
                {
                    MarcasList.Add(item);
                }
                OnProPertyChanged();
            }
        }

        #region Propriedades Veiculo
        private string marcaSelecionada;
        public string MarcaSelecionada
        {
            get { return marcaSelecionada; }
            set
            {
                marcaSelecionada = value;
                if (string.IsNullOrEmpty(value)) { OnProPertyChanged(); return; }

                ModelosList.Clear();
                List<string> a = Veiculos[value].ToList();
                foreach (var item in a)
                {
                    ModelosList.Add(item.ToString());
                }

                OnProPertyChanged();
            }
        }
        private string modeloSelecionado;
        public string ModeloSelecionado
        {
            get { return modeloSelecionado; }
            set { modeloSelecionado = value; OnProPertyChanged(); }
        }

        private int anoSelecionado;
        public int AnoSelecionado
        {
            get { return anoSelecionado; }
            set { anoSelecionado = value; OnProPertyChanged(); }
        }
        //private string cilindrada;
        //public string Cilindrada
        //{
        //    get { return cilindrada; }
        //    set { cilindrada = value; OnProPertyChanged(); }
        //}
        private string valor;
        public string Valor
        {
            get { return valor; }
            set { valor = value; OnProPertyChanged(); }
        }

        private string litragem;
        public string Litragem
        {
            get { return litragem; }
            set { litragem = value; OnProPertyChanged(); }
        }
        private string litragemBase;
        public string LitragemBase
        {
            get { return litragemBase; }
            set { litragemBase = value; OnProPertyChanged(); }
        }
        private string resreva;
        public string Reserva
        {
            get { return resreva; }
            set { resreva = value; OnProPertyChanged(); }
        }
                private string consumoMedio;
        public string ConsumoMedio
        {
            get { return consumoMedio; }
            set { consumoMedio = value; OnProPertyChanged(); }
        }
        private string kilometragem;
        public string Kilometragem
        {
            get { return kilometragem; }
            set { kilometragem = value; OnProPertyChanged(); }
        }

#endregion

        public CadastroVeiculoViewModel()
        {
            if (Veiculos == null) { Veiculos = new Dictionary<string, List<string>>(); }

            SalvarVeiculoCommand = new Command(SalvarVeiculo);

            TipoList = new ObservableCollection<string>();
            TipoList.Add("CARRO");
            TipoList.Add("MOTO");
            MarcasList = new ObservableCollection<string>();
            MarcasList.Add("Selecionar Tipo de Veículo!");
            ModelosList = new ObservableCollection<string>();
            ModelosList.Add("Selecionar Marca do Veículo!");
            AnoList = new ObservableCollection<string>();


            CarregarData();
            //Model.AcessoDados.AcessoBD acesso = new Model.AcessoDados.AcessoBD();
            //acesso.DeletarTodosVeiculo();

        }
        private void SalvarVeiculo()
        {
            try
            {
                Model.AcessoDados.AcessoBD acesso = new Model.AcessoDados.AcessoBD();
                Veiculo veiculo = new Veiculo();

                veiculo.Tipo = this.TipoSelecionado;
                veiculo.Marca = this.MarcaSelecionada;
                veiculo.Modelo = this.ModeloSelecionado;
                veiculo.Ano = this.AnoSelecionado;
                veiculo.Valor = ToDouble(this.Valor);
                veiculo.Litragem = ToDouble(this.Litragem);
                veiculo.LitragemBase = ToDouble(this.LitragemBase);
                veiculo.Reserva = ToDouble(this.Reserva);
                veiculo.ConsumoMedio = ToDouble(this.ConsumoMedio);
                veiculo.Kilometragem = ToDouble(this.Kilometragem);

                acesso.InserirVeiculo(veiculo);

                LimparCampos();
            }
            catch(Exception)
            {
                
            }
        }
        //substitui o "."Ponto trocando por virgula "," para aceitar a casa decimal no double
        private double ToDouble(string valor)
        {
            
            return string.IsNullOrEmpty(valor)?0:Convert.ToDouble(valor)>0?Convert.ToDouble(valor.Replace('.', ',')):0;
        }
        private void CarregarData()
        {
            int AnoAtual = DateTime.Now.Year;
            for (int AnoInicial = (AnoAtual - 35); AnoInicial <= AnoAtual; AnoAtual--)
            {
                AnoList.Add(AnoAtual.ToString());
            }
        }
        private void CarregarCarros()
    {
        string[] Fiat = { "PUNTO", "SIENA", "UNO", "PALIO", "MOBI", "FIAT", "TORO", "STRADA", "IDEA", "PALIO WEEKEND" };
        List<string> FiatList = new List<string>(Fiat);
        Veiculos.Add("FIAT", FiatList);

        string[] CHEVROLET = { "CORSA", "CELTA", "ONIX", "PRISMA", "CAPTIVA", "CLASSIC", "VECTRA", "COBALT", "AGILE" };
        List<string> CHEVROLETList = new List<string>(CHEVROLET);
        Veiculos.Add("CHEVROLET", CHEVROLETList);

        string[] VOLKSWAGEN = { "GOL", "GOLF", "FOX", "UP", "SAVEIRO", "VOYAGE", "FUSCA" };
        List<string> VOLKSWAGENList = new List<string>(VOLKSWAGEN);
        Veiculos.Add("VOLKSWAGEN", VOLKSWAGENList);

        string[] FORD = { "FOCUS", "ECOSPORT", "FORD", "FIESTA", "KA" };
        List<string> FORDList= new List<string>(FORD);
        Veiculos.Add("FORD", FORDList);

        string[] RENAULT = { "SANDERO", "LOGAN", "CLIO", "DUSTER", "KWID", "CAPTUR" };
        List<string> RENAULTList = new List<string>(RENAULT);
        Veiculos.Add("RENAULT ", RENAULTList);

        string[] HYUNDAI = { "HB20", "I30", "TUCSON" };
        List<string> HYUNDAIList = new List<string>(HYUNDAI);
        Veiculos.Add("HYUNDAI  ", HYUNDAIList);

        string[] TOYOTA = { "COROLLA", "HILUX", "ETIOS" };
        List<string> TOYOTAList = new List<string>(TOYOTA);
        Veiculos.Add("TOYOTA  ", TOYOTAList);

        string[] HONDA = { "FIT", "CIVIC", "HR-V" };
        List<string> HONDAList = new List<string>(HONDA);
        Veiculos.Add("HONDA  ", HONDAList);

        string[] OUTROS = { "CITROËN C3", "AUDI A3", "JEEP RENEGADE", "PEUGEOT 207", "NISSAN KICKS", "NISSAN MARCH" };
        List<string> OUTROSList = new List<string>(OUTROS);
        Veiculos.Add("OUTROS  ", OUTROSList);
        }

        private void CarregarMotos()
        {
            string[] HONDA = { "Pop 100",  "Lead 110", "Nova BIZ 125", "CG 125 Fan", "CG 125 Cargo",
                "CG 150 Fan", "CG 150 Titan Mix", "NXR 150 Bros", "CRF 230F", "XRE 300", "CB 300R",
                "CB 600F Hornet", "XL 700V Transalp", "Shadow 750", "CBR 600RR",
                "CBR 1000RR Fireblade", "VFR 1200F", "GL 1800 Gold Wing" };
            List<string> HONDAList = new List<string>(HONDA);
            Veiculos.Add("HONDA", HONDAList);

            string[] YAMAHA = { "T115 CRYPTON", "NEO AT 115", "FACTOR YBR 125", "FAZER 150", "FAZER YS250", "XTZ 125 X", "XJ6 N", "XJ6 F", "XTZ 125",
            "LANDER XTZ 250", "XTZ 250 TÉNÉRÉ", "XT 660R", "TT-R 125E", "TT-R 125LWE", "TT-R 230", "XVS950A MIDNIGHT STAR",
            "XT 1200Z SUPER TÉNÉRÉ", "YZF-R1", "YZ 85 LW", "YZ 250 F", "YZ 450 F", "WR 250 F", "WR 450 F" };
            List<string> YAMAHAList = new List<string>(YAMAHA);
            Veiculos.Add("YAMAHA", YAMAHAList);


        }

        private void LimparCampos()
        {
            this.TipoSelecionado = "";
            //this.MarcaSelecionada = "";
            //this.ModeloSelecionado = "";
            this.AnoSelecionado = DateTime.Now.Year;
            this.Valor = "";
            this.Litragem = "";
            this.LitragemBase = "";
            this.ConsumoMedio = "";
            this.Kilometragem = "";
        }

    }
}
