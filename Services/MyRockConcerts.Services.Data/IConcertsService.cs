namespace MyRockConcerts.Services.Data
{
    using System.Linq;

    public interface IConcertsService
    {
        IQueryable<T> GetAll<T>(int? count = null);
    }
}
