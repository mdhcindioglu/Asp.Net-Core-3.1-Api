using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Contracts
{
    public interface IPersonRepository //: IRepositoryBase<Person>
    {
        IEnumerable<Person> GetAllPersons();
        IEnumerable<Person> GetAllPersonsByConditions(Expression<Func<Person, bool>> expression);
        Person GetPersonById(Guid personId);
        Person GetPersonWithAddresses(Guid personId);
        void CreatePerson(Person person);
        void UpdatePerson(Person person);
        void DeletePerson(Person person);
    }
}
