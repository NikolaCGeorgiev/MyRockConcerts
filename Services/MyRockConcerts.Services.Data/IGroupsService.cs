namespace MyRockConcerts.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using MyRockConcerts.Data.Models;

    public interface IGroupsService
    {
        Task<IEnumerable<T>> GetGroupsByConcertId<T>(int id);
    }
}
