using DAL.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class TreatmentAccess : IDataAccess<Treatment>
    {
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

        public bool Create(Treatment t)
        {
            String query = "INSERT INTO " +
                "treatment " +
                "(treatment, price) " +
                "VALUES " +
                "(@treatment, @price)";
            using (var conn = new SqlConnection(GetConnectionString()))
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = query;
                    cmd.Parameters.AddWithValue("@treatment", t.treatment);
                    cmd.Parameters.AddWithValue("@price", t.Price);
                    try
                    {
                        int result = cmd.ExecuteNonQuery();
                        if (result == 1)
                        {
                            return true;
                        }
                        return false;
                    }
                    catch (Exception ex)
                    {

                        throw;
                    }                
                   
                }
            }
        }

        public bool Delete(int id)
        {
            string query = "DELETE FROM " +
                "treatments " +
                "WHERE " +
                "treatmentId = @id";
            using (var conn = new SqlConnection(GetConnectionString()))
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = query;
                    cmd.Parameters.AddWithValue("@id", id);
                    int result = cmd.ExecuteNonQuery();
                    if (result == 1)
                    {
                        return true;
                    }
                    return false;
                }
            }
        }

        public IEnumerable<Treatment> GetAll()
        {
            String query = "select " +
            "treatmentId, " +
            "treatment, " +
            "price " +
            "FROM treatment ";
            List<Treatment> list = new List<Treatment>();
            using (var conn = new SqlConnection(GetConnectionString()))
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = query;
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        list.Add(new Treatment
                        {
                            treatmentId = reader.GetInt32(0),
                            treatment = reader.GetString(1),
                            Price = reader.GetDecimal(2)
                        });
                    }
                }
            }
            return list;
        }

        public Treatment GetById(int id)
        {
            String query = "select " +
            "treatmentId, " +
            "treatment, " +
            "price " +
            "FROM treatment " +
            "WHERE " +
            "treatmentId = @id";
            Treatment treatment = new Treatment();
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
                        treatment.treatmentId = reader.GetInt32(0);
                        treatment.treatment = reader.GetString(1);
                        treatment.Price = reader.GetDecimal(2);
                        return treatment;
                    }
                }
            }
            return treatment;
        }

        public bool Update(Treatment t)
        {
            throw new NotImplementedException();
        }
    }
}
