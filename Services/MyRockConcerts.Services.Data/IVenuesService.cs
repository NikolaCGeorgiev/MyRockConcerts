namespace MyRockConcerts.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IVenuesService
    {
        Task<T> GetByIdAsync<T>(int venueId);

        Task<IEnumerable<T>> GetAllAsync<T>();

        Task<int> CreateAsync(string name, string imgUrl, string country, string city, string address, int capacity);
    }
}
