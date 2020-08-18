namespace Contracts
{
    public interface IRepositoryWrapper
    {
        IPersonRepository Person { get; }
        IAddressRepository Address { get; }
        void Save();
    }
}
