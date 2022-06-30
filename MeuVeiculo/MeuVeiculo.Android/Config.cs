using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using MeuVeiculo.Droid;
using SQLite.Net.Interop;
using Xamarin.Forms;

[assembly: Dependency(typeof(Config))]
namespace MeuVeiculo.Droid
{
    public class Config : IConfig
    {
        private string _diretorioSQLite;
        private ISQLitePlatform _plataforma;
        public string DiretorioSQLite
        {

            get
            {
                if (string.IsNullOrEmpty(_diretorioSQLite))
                {
                    _diretorioSQLite = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal); 
                }
                return _diretorioSQLite;
            }

        }

        public ISQLitePlatform Plataforma
        {
            get
            {
                if(_plataforma == null)
                {
                    _plataforma = new SQLite.Net.Platform.XamarinAndroid.SQLitePlatformAndroid();
                }
                return _plataforma;
            }

        }
    }
}