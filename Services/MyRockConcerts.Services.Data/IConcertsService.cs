namespace MyRockConcerts.Services.Data
{
    using System.Linq;
    using System.Threading.Tasks;

    using MyRockConcerts.Data.Models;

    public interface IConcertsService
    {
        IQueryable<T> GetAllUpcoming<T>(int? count = null);

        Task<T> GetByIdAsync<T>(int id);

        Task<bool> IsInMyConcertsAsync(int concertId, string userId);

        Task AddToMyConcertsAsync(int concertId, string userId);
    }
}
