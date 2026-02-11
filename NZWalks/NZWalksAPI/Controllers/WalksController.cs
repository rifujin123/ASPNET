using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using NZWalksAPI.Models.Domain;
using NZWalksAPI.Models.DTO;
using NZWalksAPI.Repositories;

namespace NZWalksAPI.Controllers
{
    // /api/walks
    [Route("api/[controller]")]
    [ApiController]
    public class WalksController : ControllerBase
    {
        private readonly IMapper autoMapper;
        private readonly IWalkRepository walkRepository;

        public WalksController(IMapper autoMapper, IWalkRepository walkRepository)
        {
            this.autoMapper = autoMapper;
            this.walkRepository = walkRepository;
        }

        //CREATE Walk
        // POST:
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddWalkRequestDto addWalkRequest)
        {
            //Map dto to domain model
            var walkDomainModel = autoMapper.Map<Walk>(addWalkRequest);

            //Add domain model to the database
            await walkRepository.CreateAsync(walkDomainModel);

            return Ok(autoMapper.Map<WalkDto>(walkDomainModel));

        }

        //GET ALL WALk
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var walks = await walkRepository.GetAllAsync();
            //map domain model to dto
            return Ok(autoMapper.Map<List<WalkDto>>(walks));
        }

        //GET SINGLE WALK
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var walkDomainModel = await walkRepository.GetByIdAsync(id);

            if (walkDomainModel == null)
                return NotFound();

            return Ok(autoMapper.Map<WalkDto>(walkDomainModel));
        }

        //UPDATE WALK
        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateWalkRequest updateWalkRequest)
        {
            //map dto to domain model
            var walkDomainModel = autoMapper.Map<Walk>(updateWalkRequest);

            walkDomainModel = await walkRepository.UpdateAsync(id, walkDomainModel);

            if(walkDomainModel == null) return NotFound();

            return Ok(autoMapper.Map<WalkDto>(walkDomainModel));

        }

        //DELETE
        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var walk = await walkRepository.DeleteAsync(id);

            if (walk == null) return NotFound();

            return Ok(autoMapper.Map<WalkDto>(walk));
        }
    }
}
