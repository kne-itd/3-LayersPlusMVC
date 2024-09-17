using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DataAccess
    {
        private string GetConnectionString()
        {
            // IP Addresses of db server
            string HomeIP = "172.16.226.7";
            string WorkIp = "10.130.64.202";
            // Check which network we currently are on
            string DbServerAddress = string.Empty;
            string hostName = Dns.GetHostName(); ;
            string CurrentIp = Dns.GetHostEntry(hostName).AddressList[1].ToString();
            //string CurrentIp = Dns.GetHostByName(hostName).AddressList[1].ToString();
            if (CurrentIp.StartsWith("172."))
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
        
        public List<Patient> GetData()
        {
            List<Patient> patients = new List<Patient>();
            using (var conn = new SqlConnection(GetConnectionString()))
            {
                conn.Open();

                using (SqlCommand cmd = conn.CreateCommand())
                {
                    string sql = "SELECT * FROM Patient";
                    cmd.CommandText = sql;
                    SqlDataReader Results = cmd.ExecuteReader();

                    while (Results.Read())
                    {
                        patients.Add(new Patient
                        {
                            animalType = Results.GetInt32(4),
                            patientId = Results.GetInt32(0),
                            patientName = Results.GetString(1),
                            dateOfBirth = Results.GetDateTime(2)
                       });
                        //Console.WriteLine(Results.GetInt32(0) + " " + Results.GetString(1) + " " + Results.GetDateTime(2));
                    }
                }
            }
            return patients;
        }

        public bool Update(Patient patient)
        {
            // Sanitize patient
            using (var conn = new SqlConnection(GetConnectionString()))
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    string query = "UPDATE Patient SET patientName = @Name, dateOfBirth = @DOB " +
                        "WHERE patientId = @Id";

                    cmd.CommandText = query;
                    cmd.Parameters.AddWithValue("@Name", patient.patientName);
                    cmd.Parameters.AddWithValue("@DOB", patient.dateOfBirth);
                    cmd.Parameters.AddWithValue("@Id", patient.patientId);

                    int result = cmd.ExecuteNonQuery();
                    if (result == 1)
                    {
                        return true;
                    }
                    return false;
                }
            }
        }
        public bool Create(Patient patient)
        {
            // Sanitize patient
            using (var conn = new SqlConnection(GetConnectionString()))
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    string query = "CREATE Patient " +
                        "(patientName, dateOfBirth ) " +
                        "VALUES " +
                        "(@Name, @DOB ";

                    cmd.CommandText = query;
                    cmd.Parameters.AddWithValue("@Name", patient.patientName);
                    cmd.Parameters.AddWithValue("@DOB", patient.dateOfBirth);

                    int result = cmd.ExecuteNonQuery();
                    if (result == 1)
                    {
                        return true;
                    }
                    return false;
                }
            }
        }
    }
}
