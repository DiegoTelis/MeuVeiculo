using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite.Net.Attributes;

namespace MeuVeiculo.Model
{
    public class Tipos
    {
        public int Codigo { get; set; }
        public string Descricao { get; set; }
    }

    public class Eventos: BaseObservavel
    {
        private int codigo;
        [PrimaryKey, AutoIncrement]
        public int Codigo
        {
            get{ return codigo; }
            set{ codigo = value; OnProPertyChanged(); }
        }
        private int codVeiculo;
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
        private DateTime data;
        public DateTime Data
        {//Esta retornando com UTC 0 tirei 3hrs para compensar o valor retornado
            //get { return data.AddHours(-3); }
            //voltei ao anterior.....
            get { return data; } 
            
            set { data = value; OnProPertyChanged();}
        }
        private string observacao;
        public string Observacao
        {
            get { return observacao; }
            set { observacao = value; OnProPertyChanged(); }
        }
        private double kilometros;
        public double Kilometros
        {
            get { return kilometros; }
            set { kilometros = value; OnProPertyChanged(); }
        }
        private double valorUnitario;
        public double ValorUnitario
        {
            get { return valorUnitario; }
            set { valorUnitario = value; OnProPertyChanged(); }
        }
        private double quantidade;
        public double Quantidade
        {
            get { return quantidade; }
            set { quantidade = value; OnProPertyChanged(); }
        }
        private double total;
        public double Total
        {
            get { return total; }
            set { total = value; OnProPertyChanged(); }
        }

        private bool tanqueCheio;
        public bool TanqueCheio
        {
            get { return tanqueCheio; }
            set { tanqueCheio = value;OnProPertyChanged(); }
        }
    }
}
