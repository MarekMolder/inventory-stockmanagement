using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.BLL.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Asp.Versioning;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using ActionTypeEntity = App.DTO.v1.ActionTypeEntity;

namespace WebApp.ApiControllers
{
    [ApiVersion( "1.0" )]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    
    public class ActionTypesController : ControllerBase
    {
        private readonly ILogger<ActionTypesController> _logger;
        private readonly IAppBLL _bll;
        
        private readonly App.DTO.v1.Mappers.ActionTypeEntityAPIMapper _mapper =
            new App.DTO.v1.Mappers.ActionTypeEntityAPIMapper();

        public ActionTypesController(IAppBLL bll, ILogger<ActionTypesController> logger)
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
        [ProducesResponseType( typeof( IEnumerable<App.DTO.v1.ActionTypeEntity> ), 200 )]
        [ProducesResponseType( 404 )]
        public async Task<ActionResult<IEnumerable<App.DTO.v1.ActionTypeEntity>>> GetActions()
        {
            return (await _bll.ActionTypeEntityService.AllAsync()).Select(x => _mapper.Map(x)!).ToList();
        }

        /// <summary>
        /// Get person by id - owned by current user
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<App.DTO.v1.ActionTypeEntity>> GetActionEntity(Guid id)
        {
            var actionTypeEntity = await _bll.ActionTypeEntityService.FindAsync(id);

            if (actionTypeEntity == null)
            {
                return NotFound();
            }

            return _mapper.Map(actionTypeEntity)!;
        }

        /// <summary>
        /// Update person
        /// </summary>
        /// <param name="id"></param>
        /// <param name="person"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutActionEntity(Guid id, App.DTO.v1.ActionTypeEntity actionTypeEntity)
        {
            if (id != actionTypeEntity.Id)
            {
                return BadRequest();
            }

            await _bll.ActionTypeEntityService.UpdateAsync(_mapper.Map(actionTypeEntity)!);
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        /// <summary>
        /// Create new person
        /// </summary>
        /// <param name="person"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<App.DTO.v1.ActionTypeEntity>> PostActionEntity(App.DTO.v1.ActionTypeEntity actionTypeEntity)
        {
            var bllEntity = _mapper.Map(actionTypeEntity);
            _bll.ActionTypeEntityService.Add(bllEntity);
            await _bll.SaveChangesAsync();

            return CreatedAtAction("GetActionType", new
            {
                id = bllEntity.Id,
                version = HttpContext.GetRequestedApiVersion()!.ToString()
            }, actionTypeEntity);
        }

        /// <summary>
        /// Delete person by id - owned by current user
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteActionEntity(Guid id)
        {
            await _bll.ActionTypeEntityService.RemoveAsync(id);
            await _bll.SaveChangesAsync();
            return NoContent();
        }
    }
}
