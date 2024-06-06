using Event_API.Models;
using Event_API.Tools;
using Event_API_BLL.Interfaces;
using Event_API_BLL.Services;
using Event_API_DAL.Models;
using Event_API_DAL.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.IdentityModel.Tokens.Jwt;

namespace Event_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonsController : ControllerBase
    {
        private readonly IPersonsServices _personsServices;
        private readonly TokenGenerator _token;
        public PersonsController(IPersonsServices personsServices, TokenGenerator token)
        {
            _personsServices = personsServices;
            _token = token;
        }


        /// <summary>
        /// Edit Person
        /// </summary>
        /// <remarks>This method edits a person by id</remarks>
        /// <response code="200">update person</response>
        /// <response code="500">Error Server</response>
        /// <response code="400">Error BadRequest</response>
        [Authorize("isConnectedPolicy")]
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Edit([FromBody]PersonsEditForm p, [FromRoute]int id)
        {

            try
            {
                if (!ModelState.IsValid) return BadRequest();
                Persons persons = p.ToDomain();
                persons.PersonId = _token.GetIdFromToken(HttpContext);
                _personsServices.Edit(persons);
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
        /// Return Persons List
        /// </summary>
        /// <remarks>This method returns a <list type="Persons"></list></remarks>
        /// <response code="200">return List of Person </response>
        /// <response code="500">Error Server</response>
        /// <response code="400">Error BadRequest</response>
        [Authorize("adminPolicy")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetAll()
        {
            try
            {
                return Ok(_personsServices.GetAll());
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
        /// Return a person by id
        /// </summary>
        /// <remarks>This method returns a Person by id</remarks>
        /// <response code="200">return info person by id</response>
        /// <response code="500">Error Server</response>
        /// <response code="400">Error BadRequest</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetById(int id)
        {
            try
            {
                return Ok(_personsServices.GetById(id));
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
        /// Ban a person
        /// </summary>
        /// <remarks>This method bans a person</remarks>
        /// <response code="200">ban Person</response>
        /// <response code="500">Error Server</response>
        /// <response code="400">Error BadRequest</response>
        [Authorize("adminPolicy")]
        [HttpPut("{isbanned},{personId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult IsBanned(bool isbanned, int personId)
        {
            try
            {
                _personsServices.IsBanned(isbanned, personId);
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
        /// Update status
        /// </summary>
        /// <remarks>This method update a person status</remarks>
        /// <response code="200">update Person status</response>
        /// <response code="500">Error Server</response>
        /// <response code="400">Error BadRequest</response>
        [Authorize("adminPolicy")]
        [HttpPut("UpdateStatus/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult EditStatus([FromRoute]int id, [FromBody]int status)
        {
            try
            {
                _personsServices.EditStatus(id, status);
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
