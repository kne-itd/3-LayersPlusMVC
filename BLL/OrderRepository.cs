using DAL;
using DAL.Models;
using BLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BLL
{
    public class OrderRepository : IGenericRepository<BLL.Models.Order>
    {
        private OrderAccess orderAccess;
        public OrderRepository()
        {
            orderAccess = new OrderAccess();
        }
        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<BLL.Models.Order> GetAll()
        {
            List<DAL.Models.Order>? dalOrders = orderAccess.GetAll() as List<DAL.Models.Order>;
            List<BLL.Models.Order> orders = new List<BLL.Models.Order>();
            foreach (var item in dalOrders)
            {
                orders.Add(new BLL.Models.Order
                {
                    OrderId = item.OrderId,
                    OrderDate = item.OrderDate,
                    Owner = new BLL.Models.Owner
                    {
                        firstname = item.Owner.firstname,
                        lastname = item.Owner.lastname,
                        address = item.Owner.address,
                        zip = item.Owner.zip,
                        city = item.Owner.city
                    }

                });
            }
            return orders;
        }

        public BLL.Models.Order GetById(int id)
        {
            DAL.Models.Order dalOrder = orderAccess.GetById(id);
            BLL.Models.Order order = new BLL.Models.Order
            {
                OrderId = dalOrder.OrderId,
                OrderDate = dalOrder.OrderDate,
                Owner = new BLL.Models.Owner
                {
                    firstname = dalOrder.Owner.firstname,
                    lastname = dalOrder.Owner.lastname,
                    address = dalOrder.Owner.address,
                    zip = dalOrder.Owner.zip,
                    city = dalOrder.Owner.city
                }
            };
            foreach (var item in dalOrder.Consultations)
            {
                order.Consultations.Add(
                    new BLL.Models.Consultation
                    {
                        Id = item.Id,
                        ConsultationPrice = item.ConsultationPrice,
                        Patient = new BLL.Models.Patient
                        {
                            patientId = item.Patient.patientId,
                            patientName = item.Patient.patientName,
                            dateOfBirth = item.Patient.dateOfBirth,
                        },
                        Treatment = new BLL.Models.Treatment
                        {
                            treatmentId = item.Treatment.treatmentId,
                            treatment = item.Treatment.treatment,
                            Price = item.Treatment.Price
                        }
                    });
            }
            return order;
        }

        public bool Insert(BLL.Models.Order t)
        {
            throw new NotImplementedException();
        }

        public bool Update(BLL.Models.Order t)
        {
            throw new NotImplementedException();
        }
    }
}
