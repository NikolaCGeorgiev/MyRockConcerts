﻿namespace MyRockConcerts.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Http;
    using MyRockConcerts.Web.ViewModels.InputModels.Venues;

    public interface IVenuesService
    {
        Task<T> GetByIdAsync<T>(int venueId);

        Task<IEnumerable<T>> GetAllAsync<T>();

        Task<int> CreateAsync(string name, IFormFile imgUrl, string country, string city, string address, int capacity);

        Task<bool> EditAsync(int id, VenueEditInputModel model);
    }
}
