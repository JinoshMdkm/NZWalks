using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repository;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionController : ControllerBase
    {
        private readonly IRegionRepository regionRepository;
        private readonly IMapper mapper;

        public RegionController(IRegionRepository regionRepository,IMapper mapper)
        {
            this.regionRepository = regionRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllRegion()
        {
            var regions= await regionRepository.GetAllAsync();
            var regionsDTO= mapper.Map<List<Models.DTO.RegionDTO>>(regions);
            return Ok(regionsDTO);
        }

        [HttpGet]
        [Route("{id:guid}")]
        [ActionName("GetRegionAsync")]
        public async Task<IActionResult> GetRegionAsync(Guid id)
        {
            var region = await regionRepository.GetAsync(id);
            if (region == null)
                return NotFound();
            var regionDTO = mapper.Map<Models.DTO.RegionDTO > (region);
            return Ok(regionDTO);
        }

        [HttpPost]
        public async Task<IActionResult> PostRegion(Models.DTO.AddRegionRequest regionReqDTO)
        {
            var region = mapper.Map<Models.Domain.Region>(regionReqDTO);
            region = await regionRepository.PostAsync(region);
            var regionDTO = mapper.Map<Models.DTO.RegionDTO>(region);
            return CreatedAtAction(nameof(GetRegionAsync), new { id = regionDTO.Id }, regionDTO);
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteRegionAsync(Guid id)
        {
            var region = await  regionRepository.DeleteAsync(id);
            if (region == null)
                return NotFound();
            var regionDTO = mapper.Map<Models.DTO.RegionDTO>(region);
            return Ok(regionDTO);
        }

        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> UpdateRegionAsync([FromRoute]Guid id,[FromBody] UpdateRegionRequest updateRegionRequest)
        {
            var region = mapper.Map<Models.Domain.Region>(updateRegionRequest);
           region= await regionRepository.PutAsync(id, region);
            var regionDTO = mapper.Map<RegionDTO>(region);
            return Ok( regionDTO) ;

        }
    }
}
