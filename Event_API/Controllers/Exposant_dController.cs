using Event_API.Models;
using Event_API.Tools;
using Event_API_BLL.Interfaces;
using Event_API_Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.ComponentModel.DataAnnotations;

namespace Event_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Exposant_dController : ControllerBase
    {

        private readonly IExposant_d _exposant;

        public Exposant_dController(IExposant_d exposant)
        {
            _exposant = exposant;
        }   
        
        /// <summary>
        /// Create new exposant
        /// </summary>
        /// <remarks>This method creates a new exposant</remarks>
        /// <response code="200">Create exposant</response>
        /// <response code="500">Error Server</response>
        /// <response code="400">Error BadRequest</response>
        [Authorize("participantPolicy")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Create([FromBody]Exposant_dCreateForm form) 
        {
            try
            {
                _exposant.Create(form.ToBll());
                return Ok();
            }
            catch (SqlException ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// return a exposant by id
        /// </summary>
        /// <remarks>This method returns info of one exposant</remarks>
        /// <response code="200">Create comment</response>
        /// <response code="500">Error Server</response>
        /// <response code="400">Error BadRequest</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetById([FromRoute] int id)
        {
            try
            {               
                return Ok(_exposant.GetById(id));
            }
            catch (SqlException ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        /// <summary>
        /// update exposant
        /// </summary>
        /// <remarks>This method updates one exposant by id</remarks>
        /// <response code="200">update exposant</response>
        /// <response code="500">Error Server</response>
        /// <response code="400">Error BadRequest</response>
        [Authorize("adminParticipantPolicy")]
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Update([FromBody] Exposant_dUpdateForm form,[FromRoute] int id)
        {
            try
            {
                Exposant_d e = form.ToBll();
                e.PersonEventId = id;
                _exposant.Update(e);
                return Ok();
            }
            catch (SqlException ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }


        /// <summary>
        /// Delete Exposant
        /// </summary>
        /// <remarks>This method deletes a exposant by id</remarks>
        /// <response code="200">delete exposant</response>
        /// <response code="500">Error Server</response>
        /// <response code="400">Error BadRequest</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Delete([FromRoute] int id)
        {
            try
            {
                _exposant.Delete(id);
                return Ok();
            }
            catch (SqlException ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
    }
}
