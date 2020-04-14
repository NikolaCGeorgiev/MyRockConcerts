namespace MyRockConcerts.Services.Data
{
    using System.Threading.Tasks;

    public interface IVenuesService
    {
        Task<T> GetByIdAsync<T>(int venueId);
    }
}
