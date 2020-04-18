namespace MyRockConcerts.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IGenresService
    {
        Task<IEnumerable<T>> GetGenresByGroupIdAsync<T>(int id);

        Task<IEnumerable<T>> AllAsync<T>();

        Task<int> AddGenreAsync(int groupId, int genreId);

        Task<string> GetNameByIdAsync(int genreId);

        Task<int> CreateAsync(string name);
    }
}
