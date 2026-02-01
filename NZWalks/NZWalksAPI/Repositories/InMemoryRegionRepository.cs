using System.Reflection.Metadata.Ecma335;
using NZWalksAPI.Models.Domain;

namespace NZWalksAPI.Repositories
{
    public class InMemoryRegionRepository : IRegionRepository
    {
        public async Task<List<Region>> GetAllAsync()
        {
            return new List<Region>
            {
                new Region{
                    Id = Guid.NewGuid(),
                    Name = "Khoi Le's region",
                    Code = "KLR"
                }
            };
        }
    }
}
