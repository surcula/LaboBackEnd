using Event_API.Models;
using Event_API.Tools;
using Event_API_BLL.Interfaces;
using Event_API_DAL.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace Event_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly ICommentsServices _commentsServices;

        public CommentsController(ICommentsServices commentsServices)
        {
            _commentsServices = commentsServices;
        }


        /// <summary>
        /// Delete one comment by Id
        /// </summary>
        /// <param name="id">id comment</param>
        /// <remarks>This method deletes one comment by Id</remarks>
        /// <response code="200">delete comment</response>
        /// <response code="500">Error Server</response>
        /// <response code="400">Error BadRequest</response>
        [Authorize("adminPolicy")]
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Delete(int id)
        {
            try
            {
                _commentsServices.Delete(id);
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
        /// Create new comment
        /// </summary>
        /// /// <param name="form"> Form create comment</param>
        /// <remarks>This method creates a new comment</remarks>
        /// <response code="200">Create comment</response>
        /// <response code="500">Error Server</response>
        /// <response code="400">Error BadRequest</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Create(CommentsCreateForm form)
        {
            if (!ModelState.IsValid) return BadRequest();
            try
            {
                _commentsServices.Create(form.ToDomain());
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
