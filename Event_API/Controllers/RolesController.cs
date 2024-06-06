using Event_API.Models;
using Event_API.Tools;
using Event_API_BLL.Interfaces;
using Event_API_DAL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace Event_API.Controllers
{
    [Authorize("adminPolicy")]
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly IRolesServices _rolesService;
        
        public RolesController(IRolesServices rolesService)
        {
            _rolesService = rolesService;
            
        }

        /// <summary>
        /// Create a Role
        /// </summary>
        /// <remarks>This method creates a new role</remarks>
        /// <response code="200">Create Role</response>
        /// <response code="500">Error Server</response>
        /// <response code="400">Error BadRequest</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Create(RolesCreateForm form)
        {
            if (!ModelState.IsValid) return BadRequest();

            try
            {
                _rolesService.Create(form.ToDomain());
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
        /// Edit role by id
        /// </summary>
        /// <remarks>This method creates a new comment</remarks>
        /// <response code="200">update Role</response>
        /// <response code="500">Error Server</response>
        /// <response code="400">Error BadRequest</response>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Edit([FromBody] RolesCreateForm r, [FromRoute] int id)
        {
            if (!ModelState.IsValid) return BadRequest();
            Roles roles = r.ToDomain();
            roles.RoleId = id;
            try
            {
                _rolesService.Edit(roles);
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
        /// Return a list of Roles
        /// </summary>
        /// <remarks>This method returnes a <list type="Role"></list></remarks>
        /// <response code="200">return <list type="Role"></list></response>
        /// <response code="500">Error Server</response>
        /// <response code="400">Error BadRequest</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetAll()
        {
            try
            {
                return Ok(_rolesService.GetAll());
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
        /// return Role by id
        /// </summary>
        /// <remarks>This method returns a role by id</remarks>
        /// <response code="200">return info Role by id</response>
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
                return Ok(_rolesService.GetById(id));
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
