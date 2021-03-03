using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using SURA.iOS.Services.Database;
using UIKit;
using Xamarin.Forms;

[assembly: Dependency(typeof(ISQLiteDb))]
namespace SURA.iOS.Services.Database
{
    public class ISQLiteDb : SURA.Services.Database.ISQLiteDb
    {
        public SQLite.SQLiteConnection GetConnection()
        {
            try
            {
                var documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                var path = System.IO.Path.Combine(documentsPath, "SURA_BD.db3");

                return new SQLite.SQLiteConnection(path);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.ToString());
                //TO DO
                throw ex;
            }
        }
    }
}