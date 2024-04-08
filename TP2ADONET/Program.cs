using EmployeDatas.Oracle;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP2ADONET
{
    internal class Program
    {
        static void Main(string[] args)
        {
            String host = "10.10.2.10";
            int port = 1521;
            string sid = "slam";
            string login = "gilabertado";
            string pwd = "sio";
            try
            {
                EmployeOracle empOracle = new EmployeOracle(host, port, sid, login, pwd);
                empOracle.Ouvrir();

                // empOracle.AfficherTousLesCours();
                // empOracle.AfficherNbProjets();
                // empOracle.AfficherSalaireMoyenParProjet();
                // empOracle.AugmenterSalaireCurseur();
                // empOracle.AfficheEmployesSalaire(10000);
                // empOracle.AfficheSalaireEmploye(21);
                empOracle.InsereCours("BR099", "Apprentissage JDBC", 4);

                empOracle.Fermer();
            }
            catch(OracleException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
