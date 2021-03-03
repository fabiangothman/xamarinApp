using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using SURA.Droid.Services.Database;
using Xamarin.Forms;

[assembly: Dependency(typeof(SQLiteDb))]
namespace SURA.Droid.Services.Database
{
    public class SQLiteDb : SURA.Services.Database.ISQLiteDb
    {
        public SQLite.SQLiteConnection GetConnection()
        {
            try
            {
                var documentsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments);
                var path = Path.Combine(documentsPath, "SURA_BD.db3");

                return new SQLite.SQLiteConnection(path);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.ToString());
                throw ex;
                //TO DO
            }
        }
    }
}