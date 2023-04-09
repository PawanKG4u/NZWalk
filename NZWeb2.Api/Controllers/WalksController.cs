using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWeb2.Api.BAL;
using NZWeb2.Api.Models.Domain;
using NZWeb2.Api.Models.DTO;

namespace NZWeb2.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalksController : ControllerBase
    {
        

        private readonly IMapper _IMapper;
        private readonly IWalkRespository _IWalkRespository;
        public WalksController(IMapper Mapper, IWalkRespository WalkRespository)
        {
            _IMapper = Mapper;
            _IWalkRespository = WalkRespository;
        }


        [HttpGet]
        public async Task<IActionResult> getAllWalksAsync()
        {
            var Res = _IMapper.Map<List<WalkDTO>>(await _IWalkRespository.getAllAsync());
            return Ok(Res);
        }

        [HttpGet]
        [Route("{Id:Guid}")]
        [ActionName("getWalksByIdAsync")]
        public async Task<IActionResult> getWalksByIdAsync(Guid Id)
        {
            var Res = _IMapper.Map<WalkDTO>(await _IWalkRespository.getByIdAsync(Id));
            return Ok(Res);
        }

        [HttpPost]
        public async Task<IActionResult> AddRegionsAsync([FromBody] AddWalkRequestDTO paramAddWalkRequestDTO)
        {
            var _ObjWalk = new Walk
            {
                Length = paramAddWalkRequestDTO.Length,
                RegionId = paramAddWalkRequestDTO.RegionId,
                WalkDifficultyId = paramAddWalkRequestDTO.WalkDifficultyId,
                Name = paramAddWalkRequestDTO.Name,
                
                
            };

            var _Walk = await _IWalkRespository.AddAsync(_ObjWalk);
            var Res = _IMapper.Map<WalkDTO>(_Walk);

            return CreatedAtAction(nameof(getWalksByIdAsync), new { Id = Res.Id }, Res);
        }

        [HttpDelete]
        [Route("{Id:Guid}")]
        [ActionName("DeleteWalksByIdAsync")]
        public async Task<IActionResult> DeleteWalksByIdAsync(Guid Id)
        {
            var _ObjWalks = await _IWalkRespository.DeleteByIdAsync(Id);

            if (_ObjWalks == null)
            {
                return NotFound();
            }
            return Ok(_IMapper.Map<WalkDTO>(_ObjWalks));
        }


        [HttpPut]
        [Route("{Id:Guid}")]
        public async Task<IActionResult> UpdateWalksByIdAsync([FromRoute] Guid Id, [FromBody] AddWalkRequestDTO paramAddWalkRequestDTO)
        {
            var _Walks = new Walk
            {
                Length = paramAddWalkRequestDTO.Length,
                RegionId = paramAddWalkRequestDTO.RegionId,
                WalkDifficultyId = paramAddWalkRequestDTO.WalkDifficultyId,
                Name = paramAddWalkRequestDTO.Name
            };

            var Regions = await _IWalkRespository.UpdateByIdAsync(Id, _Walks);

            if (Regions == null)
            {
                return NotFound();
            }
            return Ok(_IMapper.Map<WalkDTO>(Regions));
        }

    }
}
