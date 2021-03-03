using SQLite;
using SURA.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SURA.Services.Database
{
    
    public class SURADatabase
    {
        readonly SQLiteConnection database;
        public SURADatabase(SQLiteConnection dbPath)
        {
            database = dbPath;
            database.CreateTable<ModeloNotificaciones>();
        }

        public List<ModeloNotificaciones> GetNotifications()
        {
            try
            {
                return database.Table<ModeloNotificaciones>().Where(x => x.ID >= 0).ToList();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.ToString());
                //TO DO
                throw ex;
            }
        }

        public int GuardarNotificaciones(ModeloNotificaciones pushNotification)
        {
            try
            {
                var Notificaciones = database.Table<ModeloNotificaciones>().ToList();

                return database.Insert(pushNotification);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.ToString());
                //TO DO
                throw ex;
            }
        }

        public int EliminarNotificacion(ModeloNotificaciones item)
        {
            try
            {
                return database.Delete<ModeloNotificaciones>(item.ID);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.ToString());
                //TO DO
                throw ex;
            }
        }

        public int EliminarNotificaciones()
        {
            try
            {
                return database.DeleteAll<ModeloNotificaciones>();
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
