using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DataAccess
    {
        string ConnectionString = "Server=192.168.1.7;" +
            "Database=Vet;" +
            "Uid=Mac;" +
            "Pwd = 1234;";
        public List<Patient> GetData()
        {
            List<Patient> patients = new List<Patient>();
            using (var conn = new SqlConnection(ConnectionString))
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
            using (var conn = new SqlConnection(ConnectionString))
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
    }
}
