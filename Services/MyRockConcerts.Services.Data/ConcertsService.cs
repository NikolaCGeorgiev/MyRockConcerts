namespace MyRockConcerts.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;

    using MyRockConcerts.Data.Common.Repositories;
    using MyRockConcerts.Data.Models;
    using MyRockConcerts.Services.Mapping;

    public class ConcertsService : IConcertsService
    {
        private readonly IDeletableEntityRepository<Concert> concertsReposotory;

        public ConcertsService(IDeletableEntityRepository<Concert> concertsReposotory)
        {
            this.concertsReposotory = concertsReposotory;
        }

        public IQueryable<T> GetAll<T>(int? count = null)
        {
            IQueryable<Concert> query =
                this.concertsReposotory.All().OrderBy(x => x.Date);

            if (count.HasValue)
            {
                query = query.Take(count.Value);
            }

            return query.To<T>();
        }
    }
}
