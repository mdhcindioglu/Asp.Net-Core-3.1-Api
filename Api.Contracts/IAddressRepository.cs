using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Contracts
{
    public interface IAddressRepository //: IRepositoryBase<Address>
    {
        IEnumerable<Address> GetAllAddresses();
        IEnumerable<Address> GetAllAddressesByConditions(Expression<Func<Address, bool>> expression);
        Address GetAddressById(Guid addressId);
        Address GetAddressWithPerson(Guid addressId);
        void CreateAddress(Address address);
        void UpdateAddress(Address address);
        void DeleteAddress(Address address);
        IEnumerable<Address> AddressesByPerson(Guid personId);
    }
}
