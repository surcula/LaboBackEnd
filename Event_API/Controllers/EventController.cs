using Event_API.Models;
using Event_API.Tools;
using Event_API_BLL.Interfaces;
using Event_API_BLL.Models;
using Event_API_Domain.Interfaces;
using Event_API_Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace Event_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventController : ControllerBase
    {
        private readonly ICrud<Event, EventComplete, EventComplete> _event;
        private readonly IEventTheme _eventTheme;

        public EventController(ICrud<Event, EventComplete,EventComplete> @event)
        {
            _event = @event;
        }


        /// <summary>
        /// Create new Event
        /// </summary>
        /// <remarks>This method creates a new event</remarks>
        /// <response code="200">Create Event</response>
        /// <response code="500">Error Server</response>
        /// <response code="400">Error BadRequest</response>
        //[Authorize("adminPolicy")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult create([FromBody] EventCreateForm form)
        {
            int eventId;
            if (ModelState.IsValid)
            {
                try
                {
                    eventId = _event.Create(form.ToBLLEvent());                    
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
            else
            {
                return BadRequest();
            }
        }


        /// <summary>
        /// return a EventList
        /// </summary>
        /// <returns><list type="Event"></list></returns>
        /// <remarks>This method returns a list of event</remarks>
        /// <response code="200">delete comment</response>
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
                return Ok(_event.GetAll());
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
        /// return event by id
        /// </summary>
        /// <remarks>This method returns info of one Event by id</remarks>
        /// <response code="200">info Event by id</response>
        /// <response code="500">Error Server</response>
        /// <response code="400">Error BadRequest</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult getById([FromRoute] int id)
        {

            try
            {
                return Ok(_event.GetById(id));
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
        /// Update one event by id
        /// </summary>
        /// <remarks>This method updates a comment</remarks>
        /// <response code="200">update event</response>
        /// <response code="500">Error Server</response>
        /// <response code="400">Error BadRequest</response>
        [Authorize("adminPolicy")]
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Update([FromBody] EventUpdateForm form, [FromRoute] int id)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Event e = form.ToBLLEvent();
                    e.EventId = id;
                    _event.Update(e);

                    foreach (var eventTheme in form.Themes)
                    {
                        _eventTheme.Create(new EventTheme
                        {
                            EventId = id,
                            ThemeId = eventTheme.ThemeId
                        });
                    }
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
            else
            {
                return BadRequest();
            }
        }


        /// <summary>
        /// Delete Event
        /// </summary>
        /// <remarks>This method deletes event by id</remarks>
        /// <response code="200">delete event</response>
        /// <response code="500">Error Server</response>
        /// <response code="400">Error BadRequest</response>
        [Authorize("adminPolicy")]
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Delete([FromRoute] int id)
        {
            try
            {
                _event.Delete(id);
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
