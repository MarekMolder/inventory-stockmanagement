using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.BLL.Contracts;
using App.DTO.v1.ApiMappers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Asp.Versioning;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace WebApp.ApiControllers
{
    [ApiVersion( "1.0" )]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Authorize(Roles = "admin")]
    public class InventoriesController : ControllerBase
    {
        private readonly ILogger<InventoriesController> _logger;
        private readonly IAppBLL _bll;
        
        private readonly App.DTO.v1.Mappers.InventoryAPIMapper _mapper =
            new App.DTO.v1.Mappers.InventoryAPIMapper();
        
        private readonly EnrichedInventoryApiMapper _enrichedInventoryApiMapper = new();

        public InventoriesController(IAppBLL bll, ILogger<InventoriesController> logger)
        {
            _bll = bll;
            _logger = logger;
        }

        /// <summary>
        /// Get all persons for current user
        /// </summary>
        /// <returns>List of persons</returns>
        [HttpGet]
        [Produces( "application/json" )]
        [ProducesResponseType( typeof( IEnumerable<App.DTO.v1.Inventory> ), 200 )]
        [ProducesResponseType( 404 )]
        public async Task<ActionResult<IEnumerable<App.DTO.v1.Inventory>>> GetActions()
        {
            return (await _bll.InventoryService.AllAsync()).Select(x => _mapper.Map(x)!).ToList();
        }

        /// <summary>
        /// Get person by id - owned by current user
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<App.DTO.v1.Inventory>> GetActionEntity(Guid id)
        {
            var inventory = await _bll.InventoryService.FindAsync(id);

            if (inventory == null)
            {
                return NotFound();
            }

            return _mapper.Map(inventory)!;
        }

        /// <summary>
        /// Update person
        /// </summary>
        /// <param name="id"></param>
        /// <param name="person"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutActionEntity(Guid id, App.DTO.v1.Inventory inventory)
        {
            if (id != inventory.Id)
            {
                return BadRequest();
            }

            await _bll.InventoryService.UpdateAsync(_mapper.Map(inventory)!);
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        /// <summary>
        /// Create new person
        /// </summary>
        /// <param name="person"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<App.DTO.v1.Inventory>> PostActionEntity(App.DTO.v1.Inventory inventory)
        {
            var bllEntity = _mapper.Map(inventory);
            _bll.InventoryService.Add(bllEntity);
            await _bll.SaveChangesAsync();

            return CreatedAtAction("GetInventory", new
            {
                id = bllEntity.Id,
                version = HttpContext.GetRequestedApiVersion()!.ToString()
            }, inventory);
        }

        /// <summary>
        /// Delete person by id - owned by current user
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteActionEntity(Guid id)
        {
            await _bll.InventoryService.RemoveAsync(id);
            await _bll.SaveChangesAsync();
            return NoContent();
        }
        
        [HttpGet("enrichedInventories/")]
        [ProducesResponseType(typeof(IEnumerable<App.DTO.v1.ApiEntities.EnrichedInventory>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<App.DTO.v1.ApiEntities.EnrichedInventory>>> GetEnrichedInventories()
        {
            var data = await _bll.InventoryService.GetEnrichedInventories();

            var res = data.Select(u => _enrichedInventoryApiMapper.Map(u)!).ToList();
            return Ok(res);
        }
    }
}
