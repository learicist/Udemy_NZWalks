using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;
using System.Text.Json;

namespace NZWalks.API.Controllers
{
    // https://localhost:portNum/api/regions  <-- Route is pointing here. 
    // Would be the same as if we did [Route("api/regions")] below

    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly NZWalksDbContext dbContext;
        private readonly IRegionRepository regionRepository;
        private readonly IMapper mapper;
        private readonly ILogger<RegionsController> logger;

        public RegionsController(NZWalksDbContext dbContext, 
            IRegionRepository regionRepository, 
            IMapper mapper,
            ILogger<RegionsController> logger)
        {
            this.dbContext = dbContext;
            this.regionRepository = regionRepository;
            this.mapper = mapper;
            this.logger = logger;
        }


        // Get all regions        
        [HttpGet]
        // [Authorize(Roles = "Reader, Writer")]
        public async Task<IActionResult> GetAll()
        {

            //try
            //{
                //throw new Exception("This is a custom exception");

                // Get Data from DB - Domain Models
                var regions = await regionRepository.GetAllAsync();

                // Manually Map Domain Models to DTOs
                /* var regionsDTO = new List<RegionDTO>();
                 foreach (var region in regions)
                 {
                     regionsDTO.Add(new RegionDTO()
                     {
                         Id = region.Id,
                         Code = region.Code,
                         Name = region.Name,
                         RegionImageUrl = region.RegionImageUrl
                     });
                 } */

                // AutoMap Domain Models to DTOs
                var regionsDTO = mapper.Map<List<RegionDTO>>(regions);

                logger.LogInformation($"Finished GetAllRegions with data: {JsonSerializer.Serialize(regions)}");

                // Return DTOs instead of Domain Models to client
                return Ok(regionsDTO);
            //}

            //catch (Exception ex) 
            //{
                //logger.LogError(ex, ex.Message);
                //throw;
            //}


            logger.LogInformation("GetAll Action Method was invoked");

            logger.LogWarning("This is a warning log");

            logger.LogError("This is an error log");

            // Commented out below hardcode of regions because we will use Constructors instead
            /*var regions = new List<Region>
            {
                
                new Region
                {
                    Id = Guid.NewGuid(),
                    Name = "Auckland Region",
                    Code = "AKL",
                    RegionImageUrl = "https://wallpaperaccess.com/full/1606811.jpg"
                },
                new Region
                {
                    Id = Guid.NewGuid(),
                    Name = "Wellington Region",
                    Code = "WLG",
                    RegionImageUrl = "https://wallpaperaccess.com/full/1903819.jpg"
                }
            };*/

            
        }

        // Get single region by ID
        [HttpGet]
        [Route("{id:Guid}")]
        //[Authorize(Roles = "Reader, Writer")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            // Find() only works with ID property
            // var region = dbContext.Regions.Find(id);

            // To query by other means than ID, we can use the method below.
            // Note: Route must be updated to include whatever parameter we query by way of
            // ...orDefault(x => x.Code == Code); ...etc.
            var region = await regionRepository.GetByIdAsync(id);

            if (region == null)
            {
                return NotFound();
            }

            // Map/Convert Region Domain Model to Region DTO
            /* var regionDTO = new RegionDTO
            {
                Id = region.Id,
                Code = region.Code,
                Name = region.Name,
                RegionImageUrl = region.RegionImageUrl
            }; */

            // Return DTO back to client
            return Ok(mapper.Map<RegionDTO>(region));
        }

        // POST To create new Region
        [HttpPost]
        //[Authorize(Roles = "Writer")]
        public async Task<IActionResult> Create([FromBody] AddRegionRequestDTO addRegion)
        {
            
            if (ModelState.IsValid)
            {
                var regionDomainModel = mapper.Map<Region>(addRegion);

                // Use Domain Model to create Region
                regionDomainModel = await regionRepository.CreateAsync(regionDomainModel);

                var regionDTO = mapper.Map<RegionDTO>(regionDomainModel);

                return CreatedAtAction(nameof(GetById), new { id = regionDTO.Id }, regionDTO);
            }
            else
            {
                return BadRequest(ModelState);
            }

            // Map/Convert DTO to Domain Model, since this is a POST
            /* var regionDomainModel = new Region
            {
                Code = addRegion.Code,
                Name = addRegion.Name,
                RegionImageUrl = addRegion.RegionImageUrl
            }; */

            // Map/Convert BACK to DTO to return to the client
            /* var regionDTO = new RegionDTO
            {
                Id = regionDomainModel.Id,
                Code = regionDomainModel.Code,
                Name = regionDomainModel.Name,
                RegionImageUrl = regionDomainModel.RegionImageUrl
            }; */

            // Changes not reflected in server until this next line
            // await dbContext.SaveChangesAsync();

        }


        // Update pre-existing Region
        [HttpPut]
        [Route("{id:Guid}")]
        //[Authorize(Roles = "Writer")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateRegionRequestDTO updateRegion)
        {
            // Map DTO to Domain Model
            /* var regionDomainModel = new Region
            {
                Code = updateRegion.Code,
                Name = updateRegion.Name,
                RegionImageUrl = updateRegion.RegionImageUrl
            }; */

            if (ModelState.IsValid)
            {
                var regionDomainModel = mapper.Map<Region>(updateRegion);

                // Check if Region exists
                regionDomainModel = await regionRepository.UpdateAsync(id, regionDomainModel);

                if (regionDomainModel == null)
                {
                    return NotFound();
                }

                await dbContext.SaveChangesAsync();

                // Convert Domain Model back to DTO for clientside
                /* var regionDTO = new RegionDTO
                {
                    Id = regionDomainModel.Id,
                    Code = regionDomainModel.Code,
                    Name = regionDomainModel.Name,
                    RegionImageUrl = regionDomainModel.RegionImageUrl
                }; */

                var regionDTO = mapper.Map<Region>(regionDomainModel);

                return Ok(regionDTO);
            }
            else
            {
                return BadRequest(ModelState);
            }
            
        }

        // Delete a region
        [HttpDelete]
        [Route("{id:guid}")]
        //[Authorize(Roles = "Writer")]
        public async Task<IActionResult> Delete([FromRoute] Guid id) 
        {
            var regionDomainModel = await regionRepository.DeleteAsync(id);

            if (regionDomainModel == null) 
            {
                return NotFound();
            }

            return Ok(regionDomainModel);
        }


    }
}
