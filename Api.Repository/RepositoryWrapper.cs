using Contracts;
using Entities;

namespace Repository
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private RepositoryContext _repoContext;
        private IPersonRepository _person;
        private IAddressRepository _address;

        public IPersonRepository Person
        {
            get
            {
                if (_person == null)
                    _person = new PersonRepository(_repoContext);

                return _person;
            }
        }

        public IAddressRepository Address
        {
            get
            {
                if (_address == null)
                    _address = new AddressRepository(_repoContext);

                return _address;
            }
        }

        public RepositoryWrapper(RepositoryContext repositoryContext)
        {
            _repoContext = repositoryContext;
        }

        public void Save()
        {
            _repoContext.SaveChanges();
        }
    }
}
