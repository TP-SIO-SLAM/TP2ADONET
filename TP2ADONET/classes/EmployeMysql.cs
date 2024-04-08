using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeDatas.Mysql
{
    internal class EmployeMysql
    {
        String host;
        int port;
        string db;
        string login;
        string pwd;
        string connection;

        public EmployeMysql(string host, int port, string db, string login, string pwd)
        {
            this.host = host;
            this.port = port;
            this.db = db;
            this.login = login;
            this.pwd = pwd;
            this.connection = String.Format("Data Source= " +
                    "(DESCRIPTION = (ADDRESS = (PROTOCOL = TCP) (HOST = {0}) (PORT = {1}))" +
                    "(CONNECT_DATA = (SERVICE_NAME = {2}))); User Id = {3}; Password = {4};"
                    , host, port, db, login, pwd);
        }
        
    }
}
