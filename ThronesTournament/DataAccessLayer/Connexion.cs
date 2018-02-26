using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    class Connexion
    {

        private static string connexionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename='C:\\Users\\youssefNIDA\\Documents\\ISIMA\\S2\\web Services\\Projet\\BD\\db_thrones.mdf';Integrated Security=True;Connect Timeout=30";

        private static SqlConnection _sqlConnection;

        private static Connexion _instance;

        private static readonly object padlock = new object();


        private Connexion()
        {
            _sqlConnection = new SqlConnection();
            _sqlConnection.ConnectionString = connexionString;
        }

        public SqlConnection SqlConnection
        {
            get { return _sqlConnection; }
        }

        public static Connexion Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (padlock)
                    {
                        if (_instance == null)
                        {
                            _instance = new Connexion();
                        }
                    }
                }
                _instance.SqlConnection.ConnectionString = connexionString;
                return _instance;
            }
        }


    }
}
