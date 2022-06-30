using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite.Net.Attributes;

namespace MeuVeiculo.Model
{
    public class Veiculo : BaseObservavel
    {
        private int codVeiculo;
        [PrimaryKey, AutoIncrement]
        public int CodVeiculo
        {
            get { return codVeiculo; }
            set { codVeiculo = value; OnProPertyChanged(); }
        }
        private string tipo;
        public string Tipo
        {
            get { return tipo; }
            set { tipo = value; OnProPertyChanged(); }
        }
        private string marca;
        public string Marca
        {
            get { return marca; }
            set { marca = value; OnProPertyChanged(); }
        }
        private string modelo;
        public string Modelo
        {
            get { return modelo; }
            set { modelo = value; OnProPertyChanged(); }
        }
        private int ano;
        public int Ano
        {
            get { return ano; }
            set { ano = value; OnProPertyChanged(); }
        }
        //private string cilindrada;
        //public string Cilindrada
        //{
        //    get { return cilindrada; }
        //    set { cilindrada = value; OnProPertyChanged(); }
        //}
        private double valor;
        public double Valor
        {
            get { return valor; }
            set { valor = value; OnProPertyChanged(); }
        }
        private double litragem;
        public double Litragem
        {
            get { return litragem; }
            set { litragem = value; OnProPertyChanged(); }
        }
        private double litragemBase;
        public double LitragemBase
        {
            get { return litragemBase; }
            set { litragemBase = value; OnProPertyChanged(); }
        }
        private double reserva;
        public double Reserva
        {
            get { return reserva; }
            set { reserva = value; OnProPertyChanged(); }
        }
        private double consumoMedio;
        public double ConsumoMedio
        {
            get { return consumoMedio; }
            set { consumoMedio = value; OnProPertyChanged(); }
        }
        private double kilometragem;
        public double Kilometragem
        {
            get { return kilometragem; }
            set { kilometragem = value; OnProPertyChanged(); }
        }

        private string veiculoToString;
        [Ignore]
        public string VeiculoToString
        {
            get { return string.Format("VEÍCULO: {0}, {1}", Marca, Modelo); }
        }

    }
}
