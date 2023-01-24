using StartLine_social_network.Models;

namespace StartLine_social_network.Data.Interfaces
{
    public interface IClubService
    {
        Task<IEnumerable<Club>> GetAllElements();
        Task<Club> GetByIdAsync(int id);
        // TODO: Geolocation
        Task<IEnumerable<Club>> GetClubByCity(string city);
        bool Add(Club club);
        bool Update(Club club);
        bool Delete(Club club);
        bool Save();

    }
}
