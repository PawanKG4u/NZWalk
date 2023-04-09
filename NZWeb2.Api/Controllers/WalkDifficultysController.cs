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
    public class WalkDifficultysController : ControllerBase
    {
        private readonly IMapper _IMapper;
        private readonly IWalkDifficultyRespository _IWalkDifficultyRespository;
        public WalkDifficultysController(IMapper Mapper, IWalkDifficultyRespository WalkDifficultyRespository) { 
            _IMapper = Mapper;
            _IWalkDifficultyRespository = WalkDifficultyRespository;
        }

        [HttpGet]
        public async Task<IActionResult> getAllWalkDifficultysAsync()
        {
            var Res = _IMapper.Map<List<WalkDifficultyDTO>>(await _IWalkDifficultyRespository.getAllAsync());
            return Ok(Res);
        }

        [HttpGet]
        [Route("{Id:Guid}")]
        [ActionName("getWalkDifficultysByIdAsync")]
        public async Task<IActionResult> getWalkDifficultysByIdAsync(Guid Id)
        {
            var Res = _IMapper.Map<WalkDifficultyDTO>(await _IWalkDifficultyRespository.getByIdAsync(Id));
            return Ok(Res);
        }

        [HttpPost]
        public async Task<IActionResult> AddWalkDifficultysAsync([FromBody] AddWalkDifficultyRequestDTO paramAddWalkDifficultyRequestDTO)
        {
            var _ObjWalkDifficulty = new WalkDifficulty
            {
                Code = paramAddWalkDifficultyRequestDTO.Code
            };

            var _WalkDifficulty = await _IWalkDifficultyRespository.AddAsync(_ObjWalkDifficulty);
            var Res = _IMapper.Map<WalkDifficultyDTO>(_WalkDifficulty);

            return CreatedAtAction(nameof(getWalkDifficultysByIdAsync), new { Id = Res.Id }, Res);
        }

        [HttpDelete]
        [Route("{Id:Guid}")]
        [ActionName("DeleteWalkDifficultysByIdAsync")]
        public async Task<IActionResult> DeleteWalkDifficultysByIdAsync(Guid Id)
        {
            var _ObjWalkDifficulty = await _IWalkDifficultyRespository.DeleteByIdAsync(Id);

            if (_ObjWalkDifficulty == null)
            {
                return NotFound();
            }
            return Ok(_IMapper.Map<WalkDifficultyDTO>(_ObjWalkDifficulty));
        }


        [HttpPut]
        [Route("{Id:Guid}")]
        public async Task<IActionResult> UpdateWalkDifficultysByIdAsync([FromRoute] Guid Id, [FromBody] AddWalkDifficultyRequestDTO paramAddWalkDifficultyRequestDTO)
        {
            var _WalkDifficulty = new WalkDifficulty
            {
                Code= paramAddWalkDifficultyRequestDTO.Code
            };

            var WalkDifficulty = await _IWalkDifficultyRespository.UpdateByIdAsync(Id, _WalkDifficulty);

            if (WalkDifficulty == null)
            {
                return NotFound();
            }
            return Ok(_IMapper.Map<WalkDifficultyDTO>(WalkDifficulty));
        }

    }
}
