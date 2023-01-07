using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repository
{
    public interface IRegionRepository
    {
        Task<IEnumerable<Region>> GetAllAsync();

        Task<Region> GetAsync(Guid id);

        Task<Region> PostAsync(Region region);

        Task<Region> PutAsync(Guid id,Region region);

        Task<Region> DeleteAsync(Guid id);
        Task<Region> PatchAsync(Region region);

    }
}
