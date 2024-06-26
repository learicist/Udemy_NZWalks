using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalksController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IWalkRepository walkRepo;

        public WalksController(IMapper mapper, IWalkRepository walkRepo)
        {
            this.mapper = mapper;
            this.walkRepo = walkRepo;
        }

        //Create Walk -- POST 
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddWalkRequestDTO addWalk)
        {
            if (ModelState.IsValid)
            {
                //Map DTO to Domain Model
                var walkDomainModel = mapper.Map<Walk>(addWalk);

                await walkRepo.CreateAsync(walkDomainModel);

                //Map Domain Model to DTO
                var clientReturn = mapper.Map<WalkDTO>(walkDomainModel);

                return Ok(clientReturn);
            }
            else
            {
                return BadRequest(ModelState);
            }

        }


        //Get Walks
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] string? filterOn, 
            [FromQuery] string? filterQuery,
            [FromQuery] string? sortBy, 
            [FromQuery] bool? isAscending, 
            [FromQuery] int pageNumber = 1, 
            [FromQuery] int pageSize = 1000)
        {
            var walksDomanModel = await walkRepo.GetAllAsync(filterOn, 
                filterQuery, 
                sortBy, 
                isAscending ?? true, 
                pageNumber, 
                pageSize);

            //Create an exception instance from the Global one
            throw new Exception("This is a new exception");

            //Map Domain to DTO
            return Ok(mapper.Map<List<WalkDTO>>(walksDomanModel));
        }


        // Get Walk by Id
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetByIdAsync([FromRoute] Guid id)
        {
            var walkDomainModel = await walkRepo.GetByIdAsync(id);
            
            if (walkDomainModel == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<WalkDTO>(walkDomainModel));
        }


        // Update Walk By Id
        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> UpdateAsync([FromRoute] Guid id, UpdateWalkRequestDTO updateWalk)
        {
            
            if (ModelState.IsValid)
            {
                // Map DTO to Domain Model
                var walkDomainModel = mapper.Map<Walk>(updateWalk);

                walkDomainModel = await walkRepo.UpdateAsync(id, walkDomainModel);

                if (walkDomainModel == null)
                {
                    return NotFound();
                }

                // Map back to DTO for client
                return Ok(mapper.Map<WalkDTO>(walkDomainModel));
            }
            else
            {
                return BadRequest(ModelState);
            }
            
        }

        // Delete Walk by Id
        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var deletedWalkDomainModel = await walkRepo.DeleteAsync(id);

            if (deletedWalkDomainModel == null)
            {
                return NotFound();
            }

            // Map back to DTO for client
            return Ok(mapper.Map<WalkDTO>(deletedWalkDomainModel));
        }
    }
}
