using StartLine_social_network.Models;

namespace StartLine_social_network.Data.Interfaces
{
    public interface IDashboardService
    {
        Task<List<Party>> GetAllUserParties();
        Task<List<Club>> GetAllUserClubs();
        Task<AppUser> GetUsedById(string id);
        Task<AppUser> GetByIdNoTracking(string id);
        bool Update(AppUser user);
        bool Save();
    }
}
