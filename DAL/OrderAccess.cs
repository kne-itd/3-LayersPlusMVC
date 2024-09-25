using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using DAL.Models;

namespace DAL
{
    public class OrderAccess : IDataAccess<Order>
    {
        private string query = "select " +
                        "OrderID," +
                        "OrderDate," +
                        "firstname," +
                        "lastname," +
                        "address," +
                        "zip," +
                        "bynavn " +
                        "from [order] " +
                        "inner join owner on([order].customerID = owner.ownerID) " +
                        "inner join postby on(owner.zip = postby.postnr) ";
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

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        bool IDataAccess<Order>.Create(Order t)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Order> GetAll()
        {
            List<Order> orders = new List<Order>();
            using (var conn = new SqlConnection(GetConnectionString()))
            {
                conn.Open();

                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = query;
                    SqlDataReader Results = cmd.ExecuteReader();

                    while (Results.Read())
                    {
                        orders.Add(new Order
                        {
                            OrderId = Results.GetInt32(0),
                            OrderDate = Results.GetDateTime(1),
                            Owner = new Owner
                            {
                                firstname = Results.GetString(2),
                                lastname = Results.GetString(3),
                                address = Results.GetString(4),
                                zip = Results.GetString(5),
                                city = Results.GetString(6)
                            }
                        });
                    }
                }
            }
            return orders;
        }

        public Order? GetById(int id)
        {
            using (var conn = new SqlConnection(GetConnectionString()))
            {
                conn.Open(); 
                using (SqlCommand cmd = conn.CreateCommand()) 
                {
                    string queryWithId = query + " WHERE orderId = @Id";
                    cmd.Parameters.AddWithValue("@Id", id);
                    cmd.CommandText = queryWithId;
                    SqlDataReader Result = cmd.ExecuteReader();
                    if (Result.Read())
                    {
                        Order order = new Order
                        {
                            OrderId = Result.GetInt32(0),
                            OrderDate = Result.GetDateTime(1),
                            Owner = new Owner
                            {
                                firstname = Result.GetString(2),
                                lastname = Result.GetString(3),
                                address = Result.GetString(4),
                                zip = Result.GetString(5),
                                city = Result.GetString(6)
                            }
                        };
                        Result.Close();
                        //               List<Consultation> consultations = new List<Consultation>();
                        string sql = "SELECT " +
                            "patientTreatmentId AS Id" +
                            "patientName," +
                            "animaltype.animaltype," +
                            "treatment," +
                            "price as listPrice," +
                            "treatmentPrice " +
                            "from " +
                            "patienttreatment " +
                            "inner join " +
                            "patient on (patienttreatment.patientID = patient.patientID) " +
                            "inner join " +
                            "animaltype on (patient.animaltype = animaltype.animaltypeID) " +
                            "inner join treatment on (patienttreatment.treatmentID = treatment.treatmentID) " +
                            "where orderID = @orderID";
                        cmd.Parameters.AddWithValue("@orderID", id);
                        cmd.CommandText = sql;
                        SqlDataReader Results = cmd.ExecuteReader();
                        while (Results.Read())
                        {

                            order.Consultations.Add(new Consultation
                            {
                                Id = Results.GetInt32(0),
                                Patient = new Patient
                                {
                                    patientName = Results.GetString(1)
                                },
                                Treatment = new Treatment
                                {
                                    treatment = Results.GetString(2),
                                    Price = Results.GetDecimal(3),
                                },
                                ConsultationPrice = Results.GetDecimal(4),
                            });
                            
                        }
                        order = order;
                        return order;
                    }
                }
                return null;

            }
        }

        bool IDataAccess<Order>.Update(Order t)
        {
            throw new NotImplementedException();
        }
    }
}
