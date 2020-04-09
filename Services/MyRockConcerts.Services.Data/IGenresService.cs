﻿namespace MyRockConcerts.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IGenresService
    {
        Task<IEnumerable<T>> GetGenresByGroupIdAsync<T>(int id);
    }
}