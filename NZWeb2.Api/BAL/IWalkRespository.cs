using Microsoft.EntityFrameworkCore;
using NZWeb2.Api.Data;
using NZWeb2.Api.Models.Domain;

namespace NZWeb2.Api.BAL
{
    public interface IWalkRespository
    {
        Task<IEnumerable<Walk>> getAllAsync();
        Task<Walk> getByIdAsync(Guid Id);
        Task<Walk> AddAsync(Walk paramWalk);
        Task<Walk> DeleteByIdAsync(Guid Id);
        Task<Walk> UpdateByIdAsync(Guid Id, Walk paramWalk);
    }
    public class WalkRespository : IWalkRespository
    {
        private readonly NZWalksDBContext _DBContext;
        public WalkRespository(NZWalksDBContext DBContext) {
            _DBContext = DBContext;
        }
        public async Task<Walk> AddAsync(Walk paramWalk)
        {
            paramWalk.Id=Guid.NewGuid();
            await _DBContext.Walks.AddAsync(paramWalk);
            await _DBContext.SaveChangesAsync();
            return paramWalk;

        }

        public async Task<Walk> DeleteByIdAsync(Guid Id)
        {
            var _ObjWalk = await _DBContext.Walks.FirstOrDefaultAsync(x => x.Id == Id);
            if (_ObjWalk == null) 
            {
                return null;
            }
            _DBContext.Walks.Remove(_ObjWalk);
            await _DBContext.SaveChangesAsync();
            return _ObjWalk;
        }

        public async Task<IEnumerable<Walk>> getAllAsync()
        {
            return await _DBContext.Walks.Include(x=>x.Regions).Include(x=>x.WalkDifficultys).ToListAsync();
        }

        public async Task<Walk> getByIdAsync(Guid Id)
        {
            return await _DBContext.Walks.FirstOrDefaultAsync(x => x.Id == Id);
        }

        public async Task<Walk> UpdateByIdAsync(Guid Id, Walk paramWalk)
        {
            var _ObjWalks =await _DBContext.Walks.FirstOrDefaultAsync(x => x.Id == Id);
            if (_ObjWalks == null) {
                return null;
            }
            _ObjWalks.Name=paramWalk.Name;
            _ObjWalks.Length=paramWalk.Length;
            _ObjWalks.WalkDifficultyId = paramWalk.WalkDifficultyId;
            _ObjWalks.RegionId=paramWalk.RegionId;
            await _DBContext.SaveChangesAsync();
            return _ObjWalks;
        }
    }


}
