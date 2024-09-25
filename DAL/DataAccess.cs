using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using DAL.Models;

namespace DAL
{
    public class DataAccess : IDataAccess<Patient>
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

        public IEnumerable<Patient> GetAll()
        {
            List<Patient> patients = new List<Patient>();
            using (var conn = new SqlConnection(GetConnectionString()))
            {
                conn.Open();

                using (SqlCommand cmd = conn.CreateCommand())
                {
                    string query = "SELECT " +
                        "patientId," +
                        "patientName," +
                        "dateOfBirth," +
                        "animaltype" +
                        " FROM Patient";
                    cmd.CommandText = query;
                    SqlDataReader Results = cmd.ExecuteReader();

                    while (Results.Read())
                    {
                        patients.Add(new Patient
                        {
                            animalType = Results.GetInt32(3),
                            patientId = Results.GetInt32(0),
                            patientName = Results.GetString(1),
                            dateOfBirth = Results.GetDateTime(2)
                        });
                    }
                }
            }
            return patients;
        }
        public Patient GetById(int id)
        {
            Patient patient = new Patient();
            using (var conn = new SqlConnection(GetConnectionString()))
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    string query = "SELECT " +
                        "patientId," +
                        "patientName," +
                        "dateOfBirth," +
                        "animaltype" +
                        " FROM Patient";
                    cmd.CommandText = query;
                    cmd.Parameters.AddWithValue("@id", id);
                    SqlDataReader Results = cmd.ExecuteReader();

                    patient.patientId = Results.GetInt32(0);
                    patient.patientName = Results.GetString(1);
                    patient.animalType = Results.GetInt32(3);
                    patient.dateOfBirth = Results.GetDateTime(2);
                }
            }
            return patient;
        }

        public bool Update(Patient patient)
        {
            // Sanitize patient
            using (var conn = new SqlConnection(GetConnectionString()))
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    string query = "UPDATE patient SET patientName = @Name, dateOfBirth = @DOB " +
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
                    string query = "INSERT INTO patient " +
                        "(patientName, dateOfBirth ) " +
                        "VALUES " +
                        "(@Name, @DOB )";

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

        public bool Delete(int id)
        {
            using (var conn = new SqlConnection(GetConnectionString()))
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    string query = "DELETE FROM patient " +
                        " WHERE patientId = @Id;";

                    cmd.CommandText = query;
                    cmd.Parameters.AddWithValue("@Id", id);

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
