namespace MyRockConcerts.Services.Data
{
    using System.Threading.Tasks;

    public interface IMembersService
    {
        Task<T> GetByIdAsync<T>(int memberId);

        Task<int> CreateAsync(string fullName, string imgUrl, string description, int groupId);
    }
}
