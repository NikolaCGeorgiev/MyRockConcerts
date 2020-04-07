namespace MyRockConcerts.Services.Data
{
    using System.Linq;
    using System.Threading.Tasks;

    using MyRockConcerts.Data.Models;

    public interface IConcertsService
    {
        IQueryable<T> GetAll<T>(int? count = null);

        Task<T> GetByIdAsync<T>(int id);
    }
}
