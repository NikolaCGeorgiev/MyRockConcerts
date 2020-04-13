namespace MyRockConcerts.Services.Data
{
    using System.Linq;
    using System.Threading.Tasks;

    public interface IConcertsService
    {
        IQueryable<T> GetAllUpcoming<T>(int? count = null);

        IQueryable<T> GetMyConcerts<T>(string userId);

        Task<T> GetByIdAsync<T>(int id);

        Task<bool> IsInMyConcertsAsync(int concertId, string userId);

        Task AddToMyConcertsAsync(int concertId, string userId);
    }
}
