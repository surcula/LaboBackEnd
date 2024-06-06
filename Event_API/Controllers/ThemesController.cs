using Event_API.Models;
using Event_API.Tools;
using Event_API_BLL.Interfaces;
using Event_API_Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace Event_API.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class ThemesController : ControllerBase
    {
        private readonly IThemes _themes;

        public ThemesController(IThemes themes)
        {
            _themes = themes;
        }

        /// <summary>
        /// Create a theme
        /// </summary>
        /// <remarks>This method creates a new theme</remarks>
        /// <response code="200">create theme</response>
        /// <response code="500">Error Server</response>
        /// <response code="400">Error BadRequest</response>
        [Authorize("adminPolicy")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Create([FromBody] ThemeCreateForm theme)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _themes.Create(theme.ToBLL());
                }
                catch (SqlException ex)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
                return Ok();
            }
            else
            {
                return BadRequest();
            }

        }

        /// <summary>
        /// update theme
        /// </summary>
        /// <remarks>This method updates a theme by id</remarks>
        /// <response code="200">update theme by id</response>
        /// <response code="500">Error Server</response>
        /// <response code="400">Error BadRequest</response>
        [HttpPut("{id}")]
        [Authorize("adminPolicy")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Update([FromBody] ThemeUpdateForm form, [FromRoute] int id)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Themes theme = form.ToBLL();
                    theme.ThemeId = id;
                    _themes.update(theme);
                    return Ok();
                }
                catch (SqlException ex)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
                }
                catch (Exception ex )
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
        /// Delete theme
        /// </summary>
        /// <remarks>This method deletes a theme</remarks>
        /// <response code="200">delete theme by id</response>
        /// <response code="500">Error Server</response>
        /// <response code="400">Error BadRequest</response>
        [HttpDelete("{id}")]
        [Authorize("adminPolicy")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Delete([FromRoute] int id)
        {
            try
            {
                _themes.Delete(id);
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
        /// return List themes
        /// </summary>
        /// <remarks>This method returns a <list type="Theme"></list></remarks>
        /// <response code="200">return List Theme </response>
        /// <response code="500">Error Server</response>
        /// <response code="400">Error BadRequest</response>
        [HttpGet]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetAll()
        {
            try
            {
                return Ok(_themes.getAll());
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
        /// return a theme by id
        /// </summary>
        /// <remarks>This method returns a theme by id</remarks>
        /// <response code="200">return theme by id</response>
        /// <response code="500">Error Server</response>
        /// <response code="400">Error BadRequest</response>
        [HttpGet("{id}")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetBydId([FromRoute] int id)
        {
            try
            {
                return Ok(_themes.getById(id));
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
