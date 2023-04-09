using Microsoft.EntityFrameworkCore;
using NZWeb2.Api.Data;
using NZWeb2.Api.Models.Domain;

namespace NZWeb2.Api.BAL
{
    public interface IWalkDifficultyRespository
    {
        Task<IEnumerable<WalkDifficulty>> getAllAsync();
        Task<WalkDifficulty> getByIdAsync(Guid Id);
        Task<WalkDifficulty> AddAsync(WalkDifficulty paramWalkDifficulty);
        Task<WalkDifficulty> DeleteByIdAsync(Guid Id);
        Task<WalkDifficulty> UpdateByIdAsync(Guid Id, WalkDifficulty paramWalkDifficulty);
    }
    public class WalkDifficultyRespository : IWalkDifficultyRespository
    {
        private readonly NZWalksDBContext _DBContext;
        public WalkDifficultyRespository(NZWalksDBContext DBContext) {
            _DBContext = DBContext;
        }
        public async Task<WalkDifficulty> AddAsync(WalkDifficulty paramWalkDifficulty)
        {
            paramWalkDifficulty.Id = Guid.NewGuid();
            await _DBContext.WalkDifficultys.AddAsync(paramWalkDifficulty);
            await _DBContext.SaveChangesAsync();
            return paramWalkDifficulty;
        }

        public async Task<WalkDifficulty> DeleteByIdAsync(Guid Id)
        {
            var _ObjWalkDifficultys= await _DBContext.WalkDifficultys.FirstOrDefaultAsync(x => x.Id == Id);
            if (_ObjWalkDifficultys == null) {
                return null;
            }
            _DBContext.WalkDifficultys.Remove(_ObjWalkDifficultys);
            await _DBContext.SaveChangesAsync();
            return _ObjWalkDifficultys;
        }

        public async Task<IEnumerable<WalkDifficulty>> getAllAsync()
        {
            return await _DBContext.WalkDifficultys.ToListAsync();
        }

        public async Task<WalkDifficulty> getByIdAsync(Guid Id)
        {
            return await _DBContext.WalkDifficultys.FirstOrDefaultAsync(x => x.Id == Id);
        }

        public async Task<WalkDifficulty> UpdateByIdAsync(Guid Id, WalkDifficulty paramWalkDifficulty)
        {
            var _ObjWalkDifficultys =await _DBContext.WalkDifficultys.FirstOrDefaultAsync(x => x.Id == Id);
            if (_ObjWalkDifficultys == null) {
                return null;
            }
            _ObjWalkDifficultys.Code= paramWalkDifficulty.Code;

            return _ObjWalkDifficultys;
        }
    }
}
