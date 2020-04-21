namespace MyRockConcerts.Services.Data
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Http;
    using MyRockConcerts.Web.ViewModels.InputModels.Concerts;

    public interface IConcertsService
    {
        IQueryable<T> GetAllUpcoming<T>(int? count = null);

        IQueryable<T> GetMyConcerts<T>(string userId);

        IQueryable<T> GetByGroupsId<T>(int groupId);

        Task<T> GetByIdAsync<T>(int id);

        Task<bool> IsInMyConcertsAsync(int concertId, string userId);

        Task AddToMyConcertsAsync(int concertId, string userId);

        Task RemoveFromMyConcertsAsync(int concertId, string userId);

        Task<int> CreateAsync(string name, IFormFile imgUrl, DateTime date, string ticketUrl, int venueId);

        Task<int> EditAsync(int id, ConcertEditInputModel model);
    }
}
