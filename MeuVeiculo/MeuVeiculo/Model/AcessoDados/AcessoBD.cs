using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using SQLite.Net;

namespace MeuVeiculo.Model.AcessoDados
{
    public class AcessoBD: IDisposable
    {
        private SQLiteConnection conexaoSQLite;
        public AcessoBD()
        {
            var config = DependencyService.Get<IConfig>();
            if (config == null) { return; }
            conexaoSQLite = new SQLiteConnection(config.Plataforma, Path.Combine(config.DiretorioSQLite, "MeuVeiculo.db3"));
            conexaoSQLite.CreateTable<Veiculo>();
            conexaoSQLite.CreateTable<Eventos>();
        }
        #region Veículo
        public void InserirVeiculo(Veiculo veiculo)
        {
            conexaoSQLite.Insert(veiculo);
        }
        public void DeletarVeiculo(Veiculo veiculo)
        {
            conexaoSQLite.Delete(veiculo);
        }
        public void DeletarTodosVeiculo()
        {
            conexaoSQLite.DeleteAll<Veiculo>();
        }
        public void AlterarVeiculo(Veiculo veiculo)
        {
            conexaoSQLite.Update(veiculo);
        }
        public Veiculo RetornarVeiculo(int codVeiculo)
        {
            return conexaoSQLite.Table<Veiculo>().FirstOrDefault(c => c.CodVeiculo == codVeiculo);
        }
        public List<Veiculo> RetornaVeiculosLista()
        {
            return conexaoSQLite.Table<Veiculo>().ToList();
        }
        public double RetornarLitragemBase(int codVeiculo)
        {
            Veiculo veiculo;
            veiculo = conexaoSQLite.Table<Veiculo>().FirstOrDefault(c => c.CodVeiculo == codVeiculo);
            return veiculo.LitragemBase;
        }

        #endregion

        #region Eventos
        public void InserirEvento(Eventos evento)
        {
            conexaoSQLite.Insert(evento);
        }
        public void DeletarEvento(Eventos evento)
        {
            conexaoSQLite.Delete(evento);
        }
        public void DeletarTodosEvento()
        {
            conexaoSQLite.DeleteAll<Eventos>();
        }
        public void AlterarEvento(Eventos evento)
        {
            conexaoSQLite.Update(evento);
        }
        public void AdiocionarTresHoras(List<Eventos> eventos)
        {
            foreach (Eventos item in eventos)
            {
                DateTime d = item.Data.AddHours(3);
                item.Data = d;
                DateTime aa = item.Data;
                conexaoSQLite.Update(item);
            }
            
        }
        public Eventos  RetornarEvento(int codigo)
        {
            return conexaoSQLite.Table<Eventos>().FirstOrDefault(c => c.Codigo == codigo);
        }
        public List<Eventos> RetornarEventosLista()
        {
            return conexaoSQLite.Table<Eventos>().OrderBy(c => c.Data).ToList();
        }
        public List<Eventos> RetornarEventosLista_Abastecimento()
        {
            //confirmar o codigo de Combustivel

            return conexaoSQLite.Table<Eventos>().Where(c => c.Tipo == "Combustivel").ToList();
        }
        public Eventos Retornar_UltEventoAbastecimento()
        {

            return conexaoSQLite.Table<Eventos>().Where(c => c.Tipo == "Combustivel").OrderByDescending(c => c.Data).FirstOrDefault();
        }
        public Eventos RetornarUltEvento_EntrouReserva()
        {
            return conexaoSQLite.Table<Eventos>().Where(c => c.Tipo == "Entrando Reserva").OrderByDescending(c => c.Data).FirstOrDefault();
        }

        #endregion


        public void Dispose()
        {
            conexaoSQLite.Dispose();
        }
    }
}
