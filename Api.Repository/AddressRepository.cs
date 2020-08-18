using Entities;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Contracts
{
    public class AddressRepository : RepositoryBase<Address>, IAddressRepository
    {
        public AddressRepository(RepositoryContext repositoryContext) : base(repositoryContext) { }

        public IEnumerable<Address> GetAllAddresses()
        {
            return FindAll().OrderBy(x => x.Title).ToList();
        }

        public IEnumerable<Address> GetAllAddressesByConditions(Expression<Func<Address, bool>> expression)
        {
            return FindByConditions(expression).OrderBy(x => x.Title).ToList();
        }

        public Address GetAddressById(Guid addressId)
        {
            return FindByConditions(x => x.Id.Equals(addressId)).FirstOrDefault();
        }

        public Address GetAddressWithPerson(Guid addressId)
        {
            return FindByConditions(x => x.Id.Equals(addressId)).Include(x => x.Person).FirstOrDefault();
        }

        public void CreateAddress(Address address)
        {
            Create(address);
        }

        public void UpdateAddress(Address address)
        {
            Update(address);
        }

        public void DeleteAddress(Address address)
        {
            Delete(address);
        }

        public IEnumerable<Address> AddressesByPerson(Guid personId)
        {
            return FindByConditions(x => x.PersonId.Equals(personId)).ToList();
        }
    }
}
