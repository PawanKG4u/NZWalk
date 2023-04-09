using Microsoft.EntityFrameworkCore;
using NZWeb2.Api.Data;
using NZWeb2.Api.Models.Domain;
using NZWeb2.Api.Models.DTO;

namespace NZWeb2.Api.BAL
{
    public interface IRegionRespository
    {
        Task<IEnumerable<Region>> getAllAsync();
        Task<Region> getByIdAsync(Guid Id);
        Task<Region> AddAsync(Region paramRegion);
        Task<Region> DeleteByIdAsync(Guid Id);
        Task<Region> UpdateByIdAsync(Guid Id, Region paramRegion);

    }

    public class RegionRespository : IRegionRespository
    {
        private readonly NZWalksDBContext _DBContext;
        public RegionRespository(NZWalksDBContext DBContext) {
            _DBContext = DBContext;
        }
        public async Task<IEnumerable<Region>> getAllAsync()
        {
            return await _DBContext.Regions.Include(x=>x.Walks).ToListAsync();
        }
        public async Task<Region> getByIdAsync(Guid Id)
        {
            return await _DBContext.Regions.Include(x=>x.Walks).FirstOrDefaultAsync(x => x.Id == Id);
        }
        public async Task<Region> AddAsync(Region paramRegion)
        {
            paramRegion.Id = Guid.NewGuid();

            await _DBContext.Regions.AddAsync(paramRegion);
            await _DBContext.SaveChangesAsync();
            return paramRegion;

        }
        public async Task<Region> DeleteByIdAsync(Guid Id)
        {
            var Regions = await _DBContext.Regions.FirstOrDefaultAsync(x => x.Id == Id);
            if (Regions == null) 
            {
                return null;
            }
            _DBContext.Regions.Remove(Regions);
            await _DBContext.SaveChangesAsync();
            return Regions;

        }

        public async Task<Region> UpdateByIdAsync(Guid Id,Region paramRegion)
        {
            var Regions = await _DBContext.Regions.FirstOrDefaultAsync(x => x.Id == Id);

            if (Regions == null)
            {
                return null;
            }

            Regions.Code = paramRegion.Code;
            Regions.Area = paramRegion.Area;
            Regions.Lat=paramRegion.Lat;
            Regions.Long=paramRegion.Long;
            Regions.Name=paramRegion.Name;
            Regions.Population = paramRegion.Population;

            await _DBContext.SaveChangesAsync();
            return Regions;

        }
    }
}
