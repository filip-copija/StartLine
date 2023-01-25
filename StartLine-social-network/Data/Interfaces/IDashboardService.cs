using StartLine_social_network.Models;

namespace StartLine_social_network.Data.Interfaces
{
    public interface IDashboardService
    {
        Task<List<Party>> GetAllUserParties();
        Task<List<Club>> GetAllUserClubs();
    }
}
