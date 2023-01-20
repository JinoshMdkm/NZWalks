using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repository;
using System.Runtime.InteropServices;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalksController : ControllerBase
    {
        private readonly IWalkRepository walkRepository;
        private readonly IMapper mapper;

        public WalksController(IWalkRepository walkRepository,IMapper mapper)
        {
            this.walkRepository = walkRepository;
            this.mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var walk = await walkRepository.GetAllAsync();
            var walksDTO = mapper.Map<List< WalkDTO>>(walk);
            return Ok(walksDTO);
        }

        [HttpGet]
        [Route("{id:guid}")]
        [ActionName("GetWalkAsync")]
        public async Task<ActionResult> GetAsync(Guid id)
        {
            var walk=await walkRepository.GetAsync(id);

            return Ok( mapper.Map<WalkDTO>(walk));

        }

        [HttpPost]     
        public async Task<ActionResult> PostAsync([FromBody]AddWalkRequest walkDto)
        {
            var walk=mapper.Map<Walk>(walkDto);
            var newWalk=await walkRepository.AddAsync(walk);
            var walkRespDto = mapper.Map<WalkDTO>(newWalk);
            return CreatedAtAction(nameof(GetAsync),new {id=newWalk.Id},walkDto);
        }

        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<ActionResult> UpdateAsync([FromRoute]Guid id,[FromBody] UpdateWalkRequest walkDto)
        {
            var walk = mapper.Map<Walk>(walkDto);
            var updatedWalk = await walkRepository.UpdateAsync(id,walk);
            if(updatedWalk == null)
                return NotFound();

            return Ok(mapper.Map<WalkDTO>(updatedWalk));
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<ActionResult> DeleteAsync(Guid id)
        {
          return Ok(  await walkRepository.DeleteAsync(id));
        }

        [HttpGet("GetWalkByName/{name}")]
        [ActionName("GetWalkByName")]
        public async Task<ActionResult> GetWalkByName(string name)
        {
            var walk=await walkRepository.GetAsync(name);
            if(walk==null)
                return NotFound();
            var walkDto=mapper.Map<WalkDTO>(walk);
            return Ok(walkDto);
        }
    }
}
