using DAL.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class OwnerAccess : IDataAccess<Owner>
    {
        private string query = "SELECT " +
            "ownerId, " +
            "firstname, " +
            "lastname, " +
            "address, " +
            "owner.zip AS zip, " +
            "bynavn as city " +
            "FROM owner INNER JOIN postby ON (owner.zip = postby.postnr) ";

        private string GetConnectionString()
        {
            // IP Addresses of db server
            string HomeIP = "192.168.1.7";
            string WorkIp = "10.130.64.202";
            // Check which network we currently are on
            string DbServerAddress = string.Empty;
            string hostName = Dns.GetHostName(); ;
            string CurrentIp = Dns.GetHostEntry(hostName).AddressList[1].ToString();
            //string CurrentIp = Dns.GetHostByName(hostName).AddressList[1].ToString();
            if (CurrentIp.StartsWith("192."))
            {
                DbServerAddress = HomeIP;
            }
            else if (CurrentIp.StartsWith("10."))
            {
                DbServerAddress = WorkIp;
            }
            string ConnectionString = $"Server={DbServerAddress};" +
            "Database=Vet;" +
            "Uid=Mac;" +
            "Pwd = 1234;";
            return ConnectionString;
        }

        public bool Create(Owner t)
        {
            string query = "INSERT INTO " +
                "owner " +
                "(" +
                "firstname, " +
                "lastname," +
                "address," +
                "zip" +
                ") " +
                "VALUES " +
                "(" +
                "@firstname, " +
                "@lastname," +
                "@address," +
                "@zip" +
                ")";
            bool output = false;
            using (var conn = new SqlConnection(GetConnectionString()))
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = query;
                    cmd.Parameters.AddWithValue("@firstname", t.firstname);
                    cmd.Parameters.AddWithValue("@lastname", t.lastname);
                    cmd.Parameters.AddWithValue("@address", t.address);
                    cmd.Parameters.AddWithValue("@zip", t.zip);
                    if (cmd.ExecuteNonQuery() == 1)
                    {
                        output = true;
                    }
                }
            }
            return output;
        }

        public bool Delete(int id)
        {
            string query = "DELETE FROM " +
                "owner " +
                "WHERE ownerId = @id";
            bool output = false;
            using (var conn = new SqlConnection(GetConnectionString()))
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = query;
                    cmd.Parameters.AddWithValue("@id", id);
                    if (cmd.ExecuteNonQuery() == 1)
                    {
                        output = true;
                    }
                }
            }
            return output;
        }

        public IEnumerable<Owner> GetAll()
        {
            List<Owner> list = new List<Owner>();
            using (var conn = new SqlConnection(GetConnectionString())) {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = query;
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        list.Add(new Owner
                        {
                            ownerId = reader.GetInt32(0),
                            firstname = reader.GetString(1),
                            lastname = reader.GetString(2),
                            address = reader.GetString(3),
                            zip = reader.GetString(4),
                            city = reader.GetString(5)
                        });
                    }
                }
            }
            return list;
        }

        public Owner GetById(int id)
        {
            query += " WHERE ownerId = @id";
            Owner owner = new Owner();
            using (var conn = new SqlConnection(GetConnectionString()))
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = query;
                    cmd.Parameters.AddWithValue("@id", id);
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        owner.ownerId = reader.GetInt32(0);
                        owner.firstname = reader.GetString(1);
                        owner.lastname = reader.GetString(2);
                        owner.address = reader.GetString(3);
                        owner.zip = reader.GetString(4);
                        owner.city = reader.GetString(5);
                    }
                }
            }
            return owner;
        }

        public bool Update(Owner t)
        {
            query = "UPDATE owner " +
                "SET " +
                "firstname = @firstname, " +
                "lastname = @lastname, " +
                "address = @address, " +
                "zip = @zip " +
                "WHERE ownerId = @Id";
            bool output = false;
            using (var conn = new SqlConnection(GetConnectionString()))
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = query;
                    cmd.Parameters.AddWithValue("@firstname", t.firstname);
                    cmd.Parameters.AddWithValue("@lastname", t.lastname);
                    cmd.Parameters.AddWithValue("@address", t.address);
                    cmd.Parameters.AddWithValue("@zip", t.zip);
                    cmd.Parameters.AddWithValue("@Id", t.ownerId);
                    if (cmd.ExecuteNonQuery() == 1)
                    {
                        output = true;
                    }

                }
            }
            return output;
        }
    }
}
