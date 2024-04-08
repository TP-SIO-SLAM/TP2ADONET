using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace EmployeDatas.Oracle
{
    internal class EmployeOracle
    {
        String host;
        int port;
        string db;
        string login;
        string pwd;
        OracleConnection connection;

        public EmployeOracle(string host, int port, string db, string login, string pwd)
        {
            this.host = host;
            this.port = port;
            this.db = db;
            this.login = login;
            this.pwd = pwd;
            this.connection = new OracleConnection(String.Format("Data Source= " +
                    "(DESCRIPTION = (ADDRESS = (PROTOCOL = TCP) (HOST = {0}) (PORT = {1}))" +
                    "(CONNECT_DATA = (SERVICE_NAME = {2}))); User Id = {3}; Password = {4};"
                    , host, port, db, login, pwd));
            
        }

        public void Ouvrir()
        {
            connection.Open();
        }
        public void Fermer()
        {
            connection.Close();
        }

        public void AfficherTousLesCours()
        {
            string requete = @"select * from cours";
            OracleCommand cmdOrclEmploye = new OracleCommand();
            cmdOrclEmploye.Connection = connection;
            cmdOrclEmploye.CommandType = System.Data.CommandType.Text;
            cmdOrclEmploye.CommandText = requete;

            OracleDataReader reader = cmdOrclEmploye.ExecuteReader();

            while (reader.Read())
            {
                Console.WriteLine(reader.GetValue(0));
            }
        }

        public void AfficherNbProjets()
        {
            string requete = @"select count(*) from projet";
            OracleCommand cmdOrclEmploye = new OracleCommand();
            cmdOrclEmploye.Connection = connection;
            cmdOrclEmploye.CommandType = System.Data.CommandType.Text;
            cmdOrclEmploye.CommandText = requete;

            var nb = cmdOrclEmploye.ExecuteScalar();

            Console.WriteLine("Il y a " + nb + " projets");
        }

        public void AfficherSalaireMoyenParProjet()
        {
            string requete = @"select coalesce(employe.codeprojet,'Aucun'), AVG(employe.salaire) as avgsalaire,
                                count(*) as nbemploye, coalesce(projet.nomprojet, 'null')
                                from employe left join projet on employe.codeprojet=projet.codeprojet
                                group by coalesce(employe.codeprojet,'Aucun'), coalesce(projet.nomprojet, 'null')";
            
            OracleCommand oracleCommand = new OracleCommand();
            oracleCommand.Connection = connection;
            oracleCommand.CommandType = System.Data.CommandType.Text;
            oracleCommand.CommandText = requete;

            OracleDataReader reader = oracleCommand.ExecuteReader();
            
            while (reader.Read())
            {
                Console.WriteLine(reader.GetValue(0));
            }
        }

        public void AugmenterSalaireCurseur()
        {
            string requete = @"update employe set salaire = (salaire * 1.3) where employe.codeprojet = 'PR1'";
            OracleCommand oracleCommand = new OracleCommand();
            oracleCommand.Connection = connection;
            oracleCommand.CommandType = System.Data.CommandType.Text;
            oracleCommand.CommandText = requete;

            var nb = oracleCommand.ExecuteNonQuery();

            Console.WriteLine(nb + " ont été mis à jour");
        }

        public void AfficheEmployesSalaire(int salaireEmp)
        {
            string requete = $"select numemp, nomemp, prenomemp, salaire from employe where salaire > {salaireEmp}";
            OracleCommand oracleCommand = new OracleCommand();
            oracleCommand.Connection = connection;
            oracleCommand.CommandType = System.Data.CommandType.Text;
            oracleCommand.CommandText = requete;

            OracleDataReader reader = oracleCommand.ExecuteReader();

            while (reader.Read())
            {
                Console.WriteLine("---------------------------");

                Console.Write("\t");
                Console.WriteLine(reader.GetValue(0));
                Console.Write("\t");
                Console.WriteLine(reader.GetValue(1));
                Console.Write("\t");
                Console.WriteLine(reader.GetValue(2));
                Console.Write("\t");
                Console.WriteLine(reader.GetValue(3));
            }
        }

        public void AfficheSalaireEmploye(int numemp)
        {
            string requete = $"select numemp, nomemp, prenomemp, salaire from employe where numemp = {numemp}";
            OracleCommand oracleCommand = new OracleCommand();
            oracleCommand.Connection = connection;
            oracleCommand.CommandType = System.Data.CommandType.Text;
            oracleCommand.CommandText = requete;

            OracleDataReader reader = oracleCommand.ExecuteReader();

            while (reader.Read())
            {
                Console.WriteLine("---------------------------");

                Console.Write("\t");
                Console.WriteLine(reader.GetValue(0));
                Console.Write("\t");
                Console.WriteLine(reader.GetValue(1));
                Console.Write("\t");
                Console.WriteLine(reader.GetValue(2));
                Console.Write("\t");
                Console.WriteLine(reader.GetValue(3));
            }
        }

        public void InsereCours(string codecours, string libelleCours, int nbJours)
        {
            string requete = $"insert into cours values({codecours}, {libelleCours}, {nbJours})";
            OracleCommand oracleCommand = new OracleCommand();
            oracleCommand.Connection = connection;
            oracleCommand.CommandType = System.Data.CommandType.Text;
            oracleCommand.CommandText = requete;

            var reader = oracleCommand.ExecuteNonQuery();

            Console.WriteLine($"Cours : {codecours} inséré !");
        }
    }
}
