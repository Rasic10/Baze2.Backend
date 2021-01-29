using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Baze2.Backend.Controllers
{
    public class Broker
    {
        public NpgsqlConnection connection;
        public NpgsqlTransaction transaction;

        public Broker()
        {
            string connStr = "Server=127.0.0.1;Port=5432;User Id=postgres;Password=spa.Profi123;Database=Baze2;Pooling=false;CommandTimeout=120;";
            connection = new NpgsqlConnection(connStr);
        }

        public void OtvoriKonekciju()
        {
            connection.Open();
        }

        public void ZatvoriKonekciju()
        {
            connection.Close();
        }

        public void PokreniTransakciju()
        {
            transaction = connection.BeginTransaction();
        }

        public void Commit()
        {
            transaction.Commit();
        }

        public void Rollback()
        {
            transaction.Rollback();
        }
    }
}
