namespace MyRockConcerts.Services.Data
{
    using System.Threading.Tasks;

    public interface IVenuesService
    {
        Task<T> GetByIdAsync<T>(int venueId);

        Task<bool> CreateAsync(string name, string imgUrl, string country, string city, string address, int capacity);
    }
}
