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
    public class PersonRepository : RepositoryBase<Person>, IPersonRepository
    {
        public PersonRepository(RepositoryContext repositoryContext) : base(repositoryContext) { }

        public IEnumerable<Person> GetAllPersons()
        {
            return FindAll().OrderBy(x => x.FullName).ToList();
        }

        public IEnumerable<Person> GetAllPersonsByConditions(Expression<Func<Person, bool>> expression)
        {
            return FindByConditions(expression).OrderBy(x => x.FullName).ToList();
        }

        public Person GetPersonById(Guid personId)
        {
            return FindByConditions(x => x.Id.Equals(personId)).FirstOrDefault();
        }

        public Person GetPersonWithAddresses(Guid personId)
        {
            return FindByConditions(x => x.Id.Equals(personId)).Include(X => X.Addresses).FirstOrDefault();
        }

        public void CreatePerson(Person person)
        {
            Create(person);
        }

        public void UpdatePerson(Person person)
        {
            Update(person);
        }

        public void DeletePerson(Person person)
        {
            Delete(person);
        }
    }
}
