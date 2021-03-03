using System;
using System.Collections.Generic;
using System.Text;

namespace SURA.Services.Database
{
    public interface ISQLiteDb
    {
        SQLite.SQLiteConnection GetConnection();
    }
}
