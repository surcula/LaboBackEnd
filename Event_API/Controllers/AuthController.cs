using Event_API.Models;
using Event_API.Tools;
using Event_API_BLL.Interfaces;
using Event_API_DAL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace Event_API.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IPersonsServices _personsServices;
        private readonly TokenGenerator _tokenGenerator;

        public AuthController(IPersonsServices personsService, TokenGenerator tokenGenerator)
        {
            _personsServices = personsService;
            _tokenGenerator = tokenGenerator;

        }

        /// <summary>
        /// Login
        /// </summary>
        /// /// <param name="loginInfo"> Formulaire de login</param>
        /// <response code="200">Login</response>
        /// <response code="500">Error Server</response>
        /// <response code="400">Error BadRequest</response>
        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Login(PersonsLoginForm loginInfo)
        {
            if (!ModelState.IsValid) return BadRequest();

            try
            {
                Persons connectedPersons = _personsServices.Login(loginInfo.Email, loginInfo.Password);
                string token = _tokenGenerator.GenerateToken(connectedPersons);
                return Ok(token);
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
        /// Register
        /// </summary>
        /// <remarks>This method registers a new person</remarks>
        /// <param name="form"> Formulaire d'inscription</param>
        /// <response code="200">register</response>
        /// <response code="500">Error Server</response>
        /// <response code="400">Error BadRequest</response>
        [HttpPost("register")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Register(PersonsRegisterForm form)
        {

            if(_tokenGenerator.GetIdFromToken(HttpContext) != 0)
            {
                return BadRequest("Désolé vous êtes déja connecté.");
            }


            if (!ModelState.IsValid) return BadRequest();
            try
            {
                _personsServices.Register(form.Email, form.FirstName, form.LastName, form.Password);
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
