namespace MyRockConcerts.Services.Data
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Http;
    using Microsoft.EntityFrameworkCore;
    using MyRockConcerts.Common;
    using MyRockConcerts.Data.Common.Repositories;
    using MyRockConcerts.Data.Models;
    using MyRockConcerts.Services;
    using MyRockConcerts.Services.Mapping;
    using MyRockConcerts.Web.ViewModels.InputModels.Concerts;

    public class ConcertsService : IConcertsService
    {
        private const string ErrorMessageConcertExist = "Concert with this name already exist!";
        private const string ErrorMessageDate = "Мust be an upcoming date!";

        private readonly IDeletableEntityRepository<Concert> concertsRepository;
        private readonly IRepository<UserConcert> userConcertRepository;
        private readonly IRepository<ConcertGroup> concertsGroupsRepository;
        private readonly ICloudinaryService cloudinaryService;

        public ConcertsService(
            IDeletableEntityRepository<Concert> concertsRepository,
            IRepository<UserConcert> userConcertRepository,
            IRepository<ConcertGroup> concertsGroupsRepository,
            ICloudinaryService cloudinaryService)
        {
            this.concertsRepository = concertsRepository;
            this.userConcertRepository = userConcertRepository;
            this.concertsGroupsRepository = concertsGroupsRepository;
            this.cloudinaryService = cloudinaryService;
        }

        public async Task<bool> IsInMyConcertsAsync(int concertId, string userId)
        {
            var userConcerts = await this.userConcertRepository
                .All()
                .FirstOrDefaultAsync(uc => uc.UserId == userId && uc.ConcertId == concertId);

            if (userConcerts != null)
            {
                return true;
            }

            return false;
        }

        public IQueryable<T> GetAllUpcoming<T>(int? count = null)
        {
            var filterDate = DateTime.UtcNow;

            IQueryable<Concert> query = this.concertsRepository
                .All()
                .Where(c => c.Date > filterDate)
                .OrderBy(c => c.Date);

            if (count.HasValue)
            {
                query = query.Take(count.Value);
            }

            return query.To<T>();
        }

        public async Task<T> GetByIdAsync<T>(int id)
        {
            var concert = this.concertsRepository
                .All()
                .Where(c => c.Id == id);

            return await concert.To<T>().FirstOrDefaultAsync();
        }

        public async Task AddToMyConcertsAsync(int concertId, string userId)
        {
            if (await this.IsInMyConcertsAsync(concertId, userId))
            {
                return;
            }

            var userConcert = new UserConcert
            {
                UserId = userId,
                ConcertId = concertId,
            };

            await this.userConcertRepository.AddAsync(userConcert);
            await this.userConcertRepository.SaveChangesAsync();
        }

        public IQueryable<T> GetMyConcerts<T>(string userId)
        {
            var filterDate = DateTime.UtcNow;

            var concerts = this.userConcertRepository
                .All()
                .Where(uc => uc.UserId == userId)
                .Select(uc => uc.Concert)
                .Where(c => c.Date > filterDate)
                .OrderBy(c => c.Date);

            return concerts.To<T>();
        }

        public async Task RemoveFromMyConcertsAsync(int concertId, string userId)
        {
            var userConcert = new UserConcert
            {
                UserId = userId,
                ConcertId = concertId,
            };

            this.userConcertRepository.Delete(userConcert);
            await this.userConcertRepository.SaveChangesAsync();
        }

        public IQueryable<T> GetByGroupsId<T>(int groupId)
        {
            var filterDate = DateTime.UtcNow;

            var concerts = this.concertsGroupsRepository
                .All()
                .Where(cg => cg.GroupId == groupId)
                .Select(cg => cg.Concert)
                .Where(c => c.Date > filterDate)
                .OrderBy(c => c.Date);

            return concerts.To<T>();
        }

        public async Task<int> CreateAsync(string name, IFormFile imgUrl, DateTime date, string ticketUrl, int venueId)
        {
            var concert = await this.concertsRepository
                .All()
                .FirstOrDefaultAsync(c => c.Name.ToUpper() == name.ToUpper());

            if (concert != null)
            {
                throw new ArgumentException(ErrorMessageConcertExist);
            }

            var filterDate = DateTime.UtcNow;

            if (date < filterDate)
            {
                throw new ArgumentException(ErrorMessageDate);
            }

            var url = await this.cloudinaryService.UploadPhotoAsync(
                imgUrl,
                name,
                GlobalConstants.CloudFolderForConcertsPhotos);

            concert = new Concert
            {
                Name = name,
                ImgUrl = url,
                Date = date,
                TicketUrl = ticketUrl,
                VenueId = venueId,
            };

            await this.concertsRepository.AddAsync(concert);
            await this.concertsRepository.SaveChangesAsync();

            return concert.Id;
        }

        public async Task<bool> EditAsync(int id, ConcertEditInputModel model)
        {
            var concert = await this.concertsRepository
               .All()
               .FirstOrDefaultAsync(c => c.Id == id);

            if (concert.Name.ToUpper() != model.Name.ToUpper())
            {
                var concertWithSameName = await this.concertsRepository
                .All()
                .FirstOrDefaultAsync(c => c.Name.ToUpper() == model.Name.ToUpper());

                if (concertWithSameName != null)
                {
                    throw new ArgumentException(ErrorMessageConcertExist);
                }
            }

            var filterDate = DateTime.UtcNow;

            if (model.Date < filterDate)
            {
                throw new ArgumentException(ErrorMessageDate);
            }

            var url = model.ImgUrl;

            if (model.Photo != null)
            {
                url = await this.cloudinaryService.UploadPhotoAsync(
                model.Photo,
                model.Name,
                GlobalConstants.CloudFolderForConcertsPhotos);
            }

            concert.Name = model.Name;
            concert.ImgUrl = url;
            concert.Date = model.Date;
            concert.TicketUrl = model.TicketUrl;
            concert.VenueId = model.VenueId;

            this.concertsRepository.Update(concert);
            await this.concertsRepository.SaveChangesAsync();

            return true;
        }
    }
}
