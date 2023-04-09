using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NZWeb2.Api.BAL;
using NZWeb2.Api.Models.Domain;
using NZWeb2.Api.Models.DTO;


namespace NZWeb2.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //Step - 5
    //[Authorize]
    //
    public class RegionsController : ControllerBase
    {
        private readonly IMapper _IMapper;
        private readonly IRegionRespository _IRegionRespository;
        public RegionsController(IMapper mapper, IRegionRespository RegionRespository)
        {
            _IMapper = mapper;
            _IRegionRespository = RegionRespository;
        }

        [HttpGet]
        [Authorize(Roles = "reader")]
        public async Task<IActionResult> getAllRegionsAsync()
        {
            var Res = _IMapper.Map<List<RegionDTO>>(await _IRegionRespository.getAllAsync());
            return Ok(Res);
        }

        [HttpGet]
        [Route("{Id:Guid}")]
        [ActionName("getRegionsByIdAsync")]
        [Authorize(Roles = "reader")]
        public async Task<IActionResult> getRegionsByIdAsync(Guid Id)
        {
            var _Region = await _IRegionRespository.getByIdAsync(Id);
            if (_Region == null) { 
            return NotFound();
            }
            var Res = _IMapper.Map<RegionDTO>(_Region);
            return Ok(Res);
        }

        [HttpPost]
        [Authorize(Roles = "writer")]
        public async Task<IActionResult> AddRegionsAsync([FromBody] AddRegionRequestDTO paramAddRegionRequestDTO)
        {
            //if (!ValidateAddUpdateRegionsAsync(paramAddRegionRequestDTO))
            //{
            //    return BadRequest(ModelState);
            //}



            var _ObjRegion = new Region
            {
                Area = paramAddRegionRequestDTO.Area,
                Code = paramAddRegionRequestDTO.Code,
                Lat = paramAddRegionRequestDTO.Lat,
                Long = paramAddRegionRequestDTO.Long,
                Name = paramAddRegionRequestDTO.Name,
                Population = paramAddRegionRequestDTO.Population
            };

            var _Region = await _IRegionRespository.AddAsync(_ObjRegion);
            var Res = _IMapper.Map<RegionDTO>(_Region);

            return CreatedAtAction(nameof(getRegionsByIdAsync), new { Id = Res.Id }, Res);
        }

        [HttpDelete]
        [Route("{Id:Guid}")]
        [ActionName("DeleteRegionsByIdAsync")]
        [Authorize(Roles = "writer")]
        public async Task<IActionResult> DeleteRegionsByIdAsync(Guid Id)
        {
            var Regions = await _IRegionRespository.DeleteByIdAsync(Id);
            
            if (Regions == null)
            {
                return NotFound();
            }
            return Ok(_IMapper.Map<RegionDTO>(Regions));
        }


        [HttpPut]
        [Route("{Id:Guid}")]
        [Authorize(Roles = "writer")]
        public async Task<IActionResult> UpdateRegionsByIdAsync([FromRoute] Guid Id,[FromBody] AddRegionRequestDTO paramAddRegionRequestDTO)
        {
            var _Regions = new Region
            {
                Area = paramAddRegionRequestDTO.Area,
                Population = paramAddRegionRequestDTO.Population,
                Name = paramAddRegionRequestDTO.Name,
                Code = paramAddRegionRequestDTO.Code,
                Lat = paramAddRegionRequestDTO.Lat,
                Long = paramAddRegionRequestDTO.Long
            };

            var Regions = await _IRegionRespository.UpdateByIdAsync(Id, _Regions);

            if (Regions == null)
            {
                return NotFound();
            }
            return Ok(_IMapper.Map<RegionDTO>(Regions));
        }

        private bool ValidateAddUpdateRegionsAsync(AddRegionRequestDTO paramAddRegionRequestDTO) 
        {
            if (paramAddRegionRequestDTO == null) 
            {
                ModelState.AddModelError(nameof(paramAddRegionRequestDTO), $"Region Fields Required.");
                return false;
            }

            if (string.IsNullOrWhiteSpace(paramAddRegionRequestDTO.Code)) {
                ModelState.AddModelError(nameof(paramAddRegionRequestDTO.Code), $"{nameof(paramAddRegionRequestDTO.Code)} cannot be null or empty or White Space .");
                
            }
            if (string.IsNullOrWhiteSpace(paramAddRegionRequestDTO.Name))
            {
                ModelState.AddModelError(nameof(paramAddRegionRequestDTO.Name), $"{nameof(paramAddRegionRequestDTO.Name)} cannot be null or empty or White Space .");
                
            }
            if (paramAddRegionRequestDTO.Area <= 0)
            {
                ModelState.AddModelError(nameof(paramAddRegionRequestDTO.Area), $"{nameof(paramAddRegionRequestDTO.Area)} cannot be lessthen or equal to zero.");
                
            }
            if (paramAddRegionRequestDTO.Lat <= 0)
            {
                ModelState.AddModelError(nameof(paramAddRegionRequestDTO.Lat), $"{nameof(paramAddRegionRequestDTO.Lat)} cannot be lessthen or equal to zero.");
                
            }
            if (paramAddRegionRequestDTO.Long <= 0)
            {
                ModelState.AddModelError(nameof(paramAddRegionRequestDTO.Long), $"{nameof(paramAddRegionRequestDTO.Long)} cannot be lessthen or equal to zero.");
                
            }
            if (paramAddRegionRequestDTO.Population < 0)
            {
                ModelState.AddModelError(nameof(paramAddRegionRequestDTO.Population), $"{nameof(paramAddRegionRequestDTO.Population)} cannot be less then zero.");
                
            }
            if (ModelState.ErrorCount > 0)
            {
                return false;
            }

            return true;
        }

    }
}
