using BLL.Models;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class OwnerRepository : IGenericRepository<Owner>
    {
        private OwnerAccess access = new OwnerAccess();
        public bool Delete(int id)
        {
            if (access.Delete(id)){
                return true;
            }
            return false;
        }

        public IEnumerable<Owner> GetAll()
        {
            List<DAL.Models.Owner> list = access.GetAll() as List<DAL.Models.Owner>;
            List<Owner> owners = new List<Owner>();
            foreach (var item in list)
            {
                owners.Add(new Owner
                {
                    ownerId = item.ownerId,
                    firstname = item.firstname,
                    lastname = item.lastname,
                    address = item.address,
                    zip = item.zip,
                    city = item.city
                });
            }
            return owners;
        }

        public Owner GetById(int id)
        {
            DAL.Models.Owner item = access.GetById(id);
            Owner owner = new Owner
            {
                ownerId = item.ownerId,
                firstname = item.firstname,
                lastname = item.lastname,
                address = item.address,
                zip = item.zip,
                city = item.city
            };
            return owner;
        }

        public bool Insert(Owner t)
        {
            DAL.Models.Owner dalOwner = new DAL.Models.Owner
            {
                ownerId = t.ownerId,
                firstname = t.firstname,
                lastname = t.lastname,
                address = t.address,
                zip = t.zip
            };
            if (access.Create(dalOwner)) {
                return true;
            }
            return false;
        }

        public bool Update(Owner t)
        {
            DAL.Models.Owner dalOwner = new DAL.Models.Owner
            {
                ownerId = t.ownerId,
                firstname = t.firstname,
                lastname = t.lastname,
                address = t.address,
                zip = t.zip
            };
            if (access.Update(dalOwner) )
            {
                return true;
            }
            return false ;
        }
    }
}
