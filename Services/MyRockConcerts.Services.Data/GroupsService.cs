using Microsoft.EntityFrameworkCore;
using MyRockConcerts.Data.Common.Repositories;
using MyRockConcerts.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MyRockConcerts.Services.Mapping;
using System.Threading.Tasks;

namespace MyRockConcerts.Services.Data
{
    public class GroupsService : IGroupsService
    {
        private readonly IRepository<ConcertGroup> concertGroupsRepository;

        public GroupsService(IRepository<ConcertGroup> concertGroupsRepository)
        {
            this.concertGroupsRepository = concertGroupsRepository;
        }

        public async Task<IEnumerable<T>> GetGroupsByConcertId<T>(int id)
        {
            var concerts = this.concertGroupsRepository.All()
                .Where(x => x.ConcertId == id)
                .Select(y => y.Group);

            return await concerts.To<T>().ToListAsync();
        }
    }
}
