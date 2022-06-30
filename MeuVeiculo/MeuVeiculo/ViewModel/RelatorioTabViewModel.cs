using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MeuVeiculo.ViewModel
{
    public class RelatorioTabViewModel: Model.BaseObservavel
    {
        int qtd = 0;
        Model.AcessoDados.AcessoBD acesso;
        List<Model.Eventos> lstEventos;
        Model.Eventos ultEventoM;
        Model.Eventos ultEventoY;
        Dictionary<string,  List<Model.Resultado>> ResultMesDic;
        public ObservableCollection<Model.Resultado> ResultadoMensal { get; set; }
        public ObservableCollection<Model.Resultado> ResultadoAnual { get; set; }
        public ObservableCollection<Model.Tipos>  ReferenciaPick { get; set; }
        private Model.Tipos referencia;
        public Model.Tipos Referencia
        {
            get { return referencia; }
            set
            {
                try
                {
                    referencia = value; OnProPertyChanged();
                    ResultadoMensal.Clear();
                    foreach (Model.Resultado item in ResultMesDic[value.Descricao])
                    {
                        ResultadoMensal.Add(item);
                    }
                }
                catch (Exception ex) { }
            }
        }
                

        public RelatorioTabViewModel()
        {
            acesso = new Model.AcessoDados.AcessoBD();
            lstEventos = new List<Model.Eventos>();
            lstEventos = acesso.RetornarEventosLista().Where(c => c.CodVeiculo == 9).ToList();
            ultEventoM = new Model.Eventos();
            ultEventoY = new Model.Eventos();
            ResultadoMensal = new ObservableCollection<Model.Resultado>();
            ResultadoAnual = new ObservableCollection<Model.Resultado>();
            ReferenciaPick = new ObservableCollection<Model.Tipos>();
            ResultMesDic = new Dictionary<string, List<Model.Resultado>>();
            CarregarTipos(9);

            //for (int i = 0; i < 10; i++)
            //{
            //    Model.Tipos t = new Model.Tipos();
            //    t.Codigo = i;
            //    t.Descricao = "Teste " + i;
            //    ReferenciaPick.Add(t);
            //}
        }

        private void CarregarTipos(int CodVeiculo)
        {
            try
            {
                var tipos = acesso.RetornarEventosLista().Where(c => c.CodVeiculo == CodVeiculo).GroupBy(x => x.Tipo).ToDictionary(g => g.Key, x => x.ToList());
                foreach (KeyValuePair<string, List<Model.Eventos>> tipo in tipos)
                {
                    var anos = tipo.Value.GroupBy(x => x.Data.Year).ToDictionary(x => x.Key, f => f.ToList());
                    foreach (KeyValuePair<Int32, List<Model.Eventos>> ano in anos)
                    {
                        List<Model.Resultado> lstResult = new List<Model.Resultado>();
                        Model.Resultado resultAno = new Model.Resultado();
                        resultAno.Tipo = tipo.Key + " - " + ano.Key;
                        string keyDic = tipo.Key + " - " + ano.Key;

                        qtd++;
                        Model.Tipos t = new Model.Tipos();
                        t.Codigo = qtd;
                        t.Descricao = keyDic;
                        ReferenciaPick.Add(t);

                        ultEventoY = lstEventos.Where(c => c.Tipo == "Saida" && c.Data.Year == ano.Key + 1).OrderBy(x => x.Data).FirstOrDefault();
                        //null
                        if (ultEventoY == null)
                            ultEventoY = lstEventos.Where(c => c.Tipo == "Saida" && c.Data.Year == ano.Key).OrderByDescending(c => c.Data).FirstOrDefault();

                        if (tipo.Key == "Saida")
                            resultAno.Base = "Total KM: " + (ultEventoY.Kilometros - ano.Value[0].Kilometros);
                        else if (tipo.Key == "Entrando Reserva")
                            resultAno.Base = "Total QTD: " + ano.Value.Count;
                        else
                            resultAno.Base = "Total: R$ " + ano.Value.Sum(x => x.Total);
                        //resultAno.Base = "Total: R$" + ano.Value.Sum(c => c.Total).ToString();

                        var meses = ano.Value.GroupBy(c => c.Data.Month).ToDictionary(x => x.Key, f => f.ToList());
                        foreach (KeyValuePair<int, List<Model.Eventos>> mes in meses)
                        {
                            Model.Resultado r = new Model.Resultado();
                            //    ev.Tipo = item.Key;
                            //    ev.Total = item.Value;

                            double KMFinal;
                            if(lstEventos.Where(c => c.Tipo == "Saida" && c.Data.Year == ano.Key && c.Data.Month == mes.Key + 1).Count() > 0 )
                                KMFinal = lstEventos.Where(c => c.Tipo == "Saida" && c.Data.Year == ano.Key && c.Data.Month == mes.Key + 1).OrderBy(c => c.Data).FirstOrDefault().Kilometros;
                            else
                                KMFinal = lstEventos.Where(c => c.Tipo == "Saida" && c.Data.Year == ano.Key && c.Data.Month == mes.Key).OrderByDescending(c => c.Data).FirstOrDefault().Kilometros;



                            string km;
                            switch (mes.Key)
                            {
                                case 1:
                                    if (tipo.Key == "Saida")
                                        resultAno.Ref1 =  KMFinal - Convert.ToDouble(mes.Value.Select(x =>  x.Kilometros));
                                    else if (tipo.Key == "Entrando Reserva")
                                        resultAno.Ref1 = mes.Value.Count;
                                    else
                                        resultAno.Ref1 = mes.Value.Sum(x => x.Total);
                                    r.Tipo = "Janeiro";
                                    break;
                                case 2:
                                    if (tipo.Key == "Saida")
                                        resultAno.Ref2 = KMFinal - Convert.ToDouble(mes.Value.Select(x => x.Kilometros));
                                    else if (tipo.Key == "Entrando Reserva")
                                        resultAno.Ref2 = mes.Value.Count;
                                    else
                                        resultAno.Ref2 = mes.Value.Sum(x => x.Total);
                                    r.Tipo = "Fevereiro";
                                    break;
                                case 3:
                                    if (tipo.Key == "Saida")
                                        resultAno.Ref3 = KMFinal - Convert.ToDouble(mes.Value.Select(x => x.Kilometros));
                                    else if (tipo.Key == "Entrando Reserva")
                                        resultAno.Ref3 = mes.Value.Count;
                                    else
                                        resultAno.Ref3 = mes.Value.Sum(x => x.Total);
                                    r.Tipo = "Março";
                                    break;
                                case 4:
                                    if (tipo.Key == "Saida")
                                        resultAno.Ref4 = KMFinal - Convert.ToDouble(mes.Value.Select(x => x.Kilometros));
                                    else if (tipo.Key == "Entrando Reserva")
                                        resultAno.Ref4 = mes.Value.Count;
                                    else
                                        resultAno.Ref4 = mes.Value.Sum(x => x.Total);
                                    r.Tipo = "Abril";
                                    break;
                                case 5:
                                    if (tipo.Key == "Saida")
                                        resultAno.Ref5 = KMFinal - Convert.ToDouble(mes.Value.Select(x => x.Kilometros));
                                    else if (tipo.Key == "Entrando Reserva")
                                        resultAno.Ref5 = mes.Value.Count;
                                    else
                                        resultAno.Ref5 = mes.Value.Sum(x => x.Total);
                                    r.Tipo = "Maio";
                                    break;
                                case 6:
                                    if (tipo.Key == "Saida")
                                        resultAno.Ref6 = KMFinal - Convert.ToDouble(mes.Value.Select(x => x.Kilometros));
                                    else if (tipo.Key == "Entrando Reserva")
                                        resultAno.Ref6 = mes.Value.Count;
                                    else
                                        resultAno.Ref6 = mes.Value.Sum(x => x.Total);
                                    r.Tipo = "Junho";
                                    break;
                                case 7:
                                    if (tipo.Key == "Saida")
                                        resultAno.Ref7 = KMFinal - Convert.ToDouble(mes.Value.Select(x => x.Kilometros));
                                    else if (tipo.Key == "Entrando Reserva")
                                        resultAno.Ref7 = mes.Value.Count;
                                    else
                                        resultAno.Ref7 = mes.Value.Sum(x => x.Total);
                                    r.Tipo = "Julho";
                                    break;
                                case 8:
                                    if (tipo.Key == "Saida")
                                        resultAno.Ref8 = KMFinal - Convert.ToDouble(mes.Value.Select(x => x.Kilometros));
                                    else if (tipo.Key == "Entrando Reserva")
                                        resultAno.Ref8 = mes.Value.Count;
                                    else
                                        resultAno.Ref8 = mes.Value.Sum(x => x.Total);
                                    r.Tipo = "Agosto";
                                    break;
                                case 9:
                                    if (tipo.Key == "Saida")
                                        resultAno.Ref9 = KMFinal - Convert.ToDouble(mes.Value.Select(x => x.Kilometros));
                                    else if (tipo.Key == "Entrando Reserva")
                                        resultAno.Ref9 = mes.Value.Count;
                                    else
                                        resultAno.Ref9 = mes.Value.Sum(x => x.Total);
                                    r.Tipo = "Setembro";
                                    break;
                                case 10:
                                    if (tipo.Key == "Saida")
                                        resultAno.Ref10 = KMFinal - Convert.ToDouble(mes.Value.Select(x => x.Kilometros));
                                    else if (tipo.Key == "Entrando Reserva")
                                        resultAno.Ref10 = mes.Value.Count;
                                    else
                                        resultAno.Ref10 = mes.Value.Sum(x => x.Total);
                                    r.Tipo = "Outubro";
                                    break;
                                case 11:
                                    if (tipo.Key == "Saida")
                                        resultAno.Ref11 = KMFinal - Convert.ToDouble(mes.Value.Select(x => x.Kilometros));
                                    else if (tipo.Key == "Entrando Reserva")
                                        resultAno.Ref11 = mes.Value.Count;
                                    else
                                        resultAno.Ref11 = mes.Value.Sum(x => x.Total);
                                    r.Tipo = "Novembro";
                                    break;
                                case 12:
                                    if (lstEventos.Where(c => c.Tipo == "Saida" && c.Data.Year == ano.Key + 1 && c.Data.Month == 1).Count() > 0)
                                        KMFinal = lstEventos.Where(c => c.Tipo == "Saida" && c.Data.Year == ano.Key + 1 && c.Data.Month == 1).OrderBy(c => c.Data).FirstOrDefault().Kilometros;
                                    else
                                        KMFinal = lstEventos.Where(c => c.Tipo == "Saida" && c.Data.Year == ano.Key  && c.Data.Month == mes.Key).OrderByDescending(c => c.Data).FirstOrDefault().Kilometros;
                                    
                                        
                                    if (tipo.Key == "Saida")
                                        resultAno.Ref12 = KMFinal - Convert.ToDouble(mes.Value.Select(x => x.Kilometros));
                                    else if (tipo.Key == "Entrando Reserva")
                                        resultAno.Ref12 = mes.Value.Count;
                                    else
                                        resultAno.Ref12 = mes.Value.Sum(x => x.Total);
                                    r.Tipo = "Dezembro";
                                    break;
                            }
                            if (mes.Key == 12)
                                ultEventoM = lstEventos.Where(c => c.Data.Month == 1 && c.Data.Year == ano.Key + 1 && c.Tipo == "Saida").OrderBy(c => c.Data).FirstOrDefault();
                            else
                                ultEventoM = lstEventos.Where(c => c.Data.Month == mes.Key + 1 && c.Data.Year == ano.Key && c.Tipo == "Saida").OrderBy(c => c.Data).FirstOrDefault();

                            if (ultEventoM == null)
                                ultEventoM = lstEventos.Where(c => c.Data.Month == mes.Key && c.Data.Year == ano.Key && c.Tipo == "Saida").OrderByDescending(c => c.Data).FirstOrDefault();

                            if (tipo.Key == "Saida")
                                r.Base = "Total KM: " + (ultEventoM.Kilometros - mes.Value[0].Kilometros);
                            else if (tipo.Key == "Entrando Reserva")
                                r.Base = "QTD: " + mes.Value.Count;
                            else
                                r.Base = "R$: " + mes.Value.Sum(x => x.Total);

                            lstResult.Add(r);

                            //Int32 Tipo = mes.Key;
                            //Int32 soma = mes.Value.Sum(x => x.total);
                        }
                        ResultadoAnual.Add(resultAno);

                        ResultMesDic.Add(keyDic, lstResult);
                    }

                }
            }
            catch (Exception ex)
            {
                Application.Current.MainPage.DisplayAlert("Erro", string.Format("Erro ao carregar Detalhes:\n{0}", ex.Message), "OK");
            }
        }
    }
}
