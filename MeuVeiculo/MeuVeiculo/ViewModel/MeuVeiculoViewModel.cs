using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MeuVeiculo.Model;
using Xamarin.Forms;

namespace MeuVeiculo.ViewModel
{
    public class MeuVeiculoViewModel
    {
        Model.AcessoDados.AcessoBD acesso;
        public ObservableCollection<Veiculo> Veiculos{ get; set; }
        public Command CarregarCommand { get; set; }
        public Command ApagarCommand { get; set; }
        //public Command DetalhesCommand { get; set; }
        //public Command DeletarCommand { get; set; }

        public MeuVeiculoViewModel()
        {
            acesso = new Model.AcessoDados.AcessoBD();
            Veiculos = new ObservableCollection<Veiculo>();
            CarregarCommand = new Command(CarregarVeiculos);
            ApagarCommand = new Command(ApagarVeiculos);
            //DetalhesCommand = new Command(DetalhesVeiculo);
            //DeletarCommand = new Command(DeletarVeiculo);

            CarregarVeiculos();
        }
        private void CarregarVeiculos()
        {
            try
            {
                Veiculos.Clear();
                List<Veiculo> list = acesso.RetornaVeiculosLista();

                if (list.Count() > 0)
                {
                    foreach (Veiculo item in list)
                    {
                        Veiculos.Add(item);
                    }
                }
            }
            catch (Exception) { }
        }
        private void ApagarVeiculos()
        {
            acesso.DeletarTodosVeiculo();
        }

        //N deu certo uso de Binding
        //private void DetalhesVeiculo(object ob)
        //{
        //    Veiculo v = new Veiculo();
        //    v = (Veiculo)ob;
        //}

        //private void DeletarVeiculo(object ob)
        //{
        //    Veiculo v = new Veiculo();
        //    v = (Veiculo)ob;
        //}


        public void ApagarVeiculo(Veiculo veiculo)
        {
            acesso.DeletarVeiculo(veiculo);
        }
    }
}
