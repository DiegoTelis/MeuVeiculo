using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MeuVeiculo.Model;
using Xamarin.Forms;
using System.Collections.ObjectModel;

namespace MeuVeiculo.ViewModel
{
    public class HitoricoEventosViewModel: BaseObservavel
    {
        Model.AcessoDados.AcessoBD acesso;
        List<Eventos> listTemp;
        int Count = 0;
        public ObservableCollection<Eventos> EventosHist{ get; set; }
        public Command AtualizarCommand { get; set; }
        public Command DeletarCommand { get; set; }

        private int CodVeiculo=0;
        public HitoricoEventosViewModel()
        {
            EventosHist = new ObservableCollection<Eventos>();
            AtualizarCommand = new Command(Atualizar);

            DeletarCommand = new Command(DeletarEvento);
            acesso = new Model.AcessoDados.AcessoBD();

            //CarregarEventosHist(this.CodVeiculo, 1);

            listTemp = new List<Eventos>();

        }

        private void DeletarEvento(object obj)
        {
            Eventos ev = (Eventos)obj;

            acesso.DeletarEvento(ev);
        }

        private void Atualizar(){
            CarregarEventosHist(this.CodVeiculo, 1);
        }
        //Definir diferenças no retorno
        public List<Eventos> retornaLista(int CodVeic, int TipoClasse)
        {
            CarregarEventosHist(CodVeic, TipoClasse);
            return listTemp.OrderByDescending(c => c.Data).ToList();
        }
        private void CarregarEventosHist(int CodVeic, int TipoClasse)
        {
            this.CodVeiculo = CodVeic;
            EventosHist.Clear();
            //List<Eventos> eventosList = new List<Eventos>();
            switch (TipoClasse)
            {
                case 1:
                    listTemp = acesso.RetornarEventosLista();
                    break;
                case 2:
                    listTemp = acesso.RetornarEventosLista().Where(c => c.CodVeiculo == CodVeic).ToList(); ;
                    break;
                case 3:
                    listTemp = acesso.RetornarEventosLista().Where(c => c.CodVeiculo == CodVeic && c.Tipo == "Combustivel").ToList(); ;
                    break;
                default:
                    listTemp = acesso.RetornarEventosLista();
                    break;
            }
            

            //foreach(Eventos Evento in eventosList)
            //{
            //    //DateTime a = Evento.Data;
            //    EventosHist.Add(Evento);
            //}
        }

        public Eventos RetornaEvento(int CodEvento)
        {
            return acesso.RetornarEvento(CodEvento);
        }

        public void PreencheLista(int CodV, int TipoC)
        {
            //retornaLista(CodV, TipoC)
            if (listTemp.Count() == 0)
                CarregarEventosHist(CodV, TipoC);

            int ContFinal = listTemp.Count > (Count + 15) ? Count + 15 : listTemp.Count();
            foreach (Model.Eventos item in listTemp.OrderByDescending(c => c.Data))
            {
                if (Count == ContFinal) { break; }

                if (!EventosHist.Contains(item))
                {
                    EventosHist.Add(item);
                    Count++;
                }
            }

        }

        //private void LimparTodos()
        //{
        //    List<Eventos> eventosList = new List<Eventos>();
        //    eventosList = acesso.RetornarEventosLista();
        //    acesso.AdiocionarTresHoras(eventosList);
        //}
    }
}
