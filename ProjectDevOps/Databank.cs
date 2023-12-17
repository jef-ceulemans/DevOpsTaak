using System;
using System.Data.SQLite;
using System.Windows.Forms;

namespace ProjectDevOps
{
    public class Databank
    {
        private readonly string connectionString;


        public Databank()
        {
            //maken we de connectie-eigenschappen duidelijk
            connectionString = "Data Source=voetbal.db;Version=3;New=True;Compress=True;";
        }

        public SQLiteConnection InitializeDatabase()
        {
            SQLiteConnection connectionSQL = new SQLiteConnection(connectionString);

            try
            {
                connectionSQL.Open();
            }
            catch (Exception ex)
            {
                //als de databank niet kan worden geopend zal het deze error geven
                MessageBox.Show($"Database kan niet worden geopend: {ex.Message}");
            }

            return connectionSQL;
        }

        public void CloseConnection(SQLiteConnection connection)
        {
            try
            {
                if (connection != null && connection.State == System.Data.ConnectionState.Open)
                {
                    connection.Close();
                    connection.Dispose();
                }
            }
            catch (Exception ex)
            {//als db niet kan worden gesloten zal er deze error komen
                MessageBox.Show($"Er is een fout tijdens het sluiten van de databank: {ex.Message}");
            }
        }
    }
}
