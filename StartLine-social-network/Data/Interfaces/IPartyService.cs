using StartLine_social_network.Models;

namespace StartLine_social_network.Data.Interfaces
{
    public interface IPartyService
    {
        Task<IEnumerable<Party>> GetAllElements();
        Task<Party> GetByIdAsync(int id);
        Task<Party> GetByIdAsyncNoTracking(int id);
        // TODO: Geolocation
        Task<IEnumerable<Party>> GetPartyCity(string city);
        bool Add(Party party);
        bool Update(Party party);
        bool Delete(Party party);
        bool Save();
    }
}
