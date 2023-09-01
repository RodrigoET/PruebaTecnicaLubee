using Data;
using Data.DTOs.GradeDTO;
using Data.DTOs.SchoolDTO;
using Data.Repositories.GradesRepo;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace PruebaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GradeController : ControllerBase
    {
        private readonly IGradeRepository _gradeRepository;
        protected APIResponse _response;

        public GradeController(IGradeRepository gradeRepository)
        {
            _gradeRepository = gradeRepository;
            _response = new();
        }

        [HttpGet("GetAllGrades")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> GetAllGrades()
        {
            _response.Success = true;
            _response.Result = await _gradeRepository.GetAllGrades();
            _response.StatusCode = HttpStatusCode.OK;
            return Ok(_response);
        }

        [HttpGet("GetGradeById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> GetGradeById(int id)
        {
            if (id <= 0)
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.Success = false;
                return BadRequest(_response);
            }

            var school = await _gradeRepository.GetGradeById(id);
            if (school == null)
            {
                _response.StatusCode = HttpStatusCode.NotFound;
                _response.Success = false;
                return NotFound(_response);
            }

            _response.Result = school;
            _response.StatusCode = HttpStatusCode.OK;
            _response.Success = true;
            return Ok(_response);
        }

        [HttpGet("GetGradeByName")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> GetGradeByName(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.Success = false;
                //TODO agregar Errors = "Field "Name" is required"
                return BadRequest(_response);
            }

            var grades = await _gradeRepository.GetGradeByName(name);
            if (grades == null || grades.Count() == 0)
            {
                _response.StatusCode = HttpStatusCode.NotFound;
                _response.Success = false;
                return NotFound(_response);
            }

            _response.StatusCode = HttpStatusCode.OK;
            _response.Success = true;
            _response.Result = grades;
            return Ok(_response);
        }

        [HttpPost("CreateGrade")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<APIResponse>> CreateGrade([FromBody] CreateGradeDTO createDTO)
        {
            if (createDTO == null)
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.Success = false;
                return BadRequest(_response);
            }

            if (!ModelState.IsValid)
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.Success = false;
                _response.Errors.Add(ModelState.ToString());
                return BadRequest(_response);
            }

            _response.StatusCode = HttpStatusCode.Created;
            _response.Success = true;
            _response.Result = await _gradeRepository.CreateGrade(createDTO);
            return Created("created", _response);
        }

        [HttpPut("UpdateGrade")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<APIResponse>> UpdateGrade([FromBody] UpdateGradeDTO updateDTO)
        {
            if (updateDTO == null)
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.Success = false;
                return BadRequest(_response);
            }

            if (!ModelState.IsValid)
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.Success = false;
                _response.Errors.Add(ModelState.ToString());
                return BadRequest(_response);
            }

            _response.Result = await _gradeRepository.UpdateGrade(updateDTO);
            if ((bool)_response.Result)
            {
                _response.Success = true;
                _response.StatusCode = HttpStatusCode.NoContent;
                return Ok(_response);
            }
            else
            {
                _response.Success = false;
                _response.StatusCode = HttpStatusCode.NotFound;
                return NotFound(_response);
            }
        }

        [HttpPatch("SoftDeleteGrade")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<APIResponse>> SoftDeleteGrade(int id)
        {
            if (id <= 0)
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.Success = false;
                return BadRequest(_response);
            }

            _response.Result = await _gradeRepository.SoftDeleteGrade(id);
            if ((bool)_response.Result)
            {
                _response.Success = true;
                _response.StatusCode = HttpStatusCode.NoContent;
                return Ok(_response);
            }
            else
            {
                _response.Success = false;
                _response.StatusCode = HttpStatusCode.NotFound;
                return NotFound(_response);
            }
        }

        [HttpDelete("HardDeleteGrade")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<APIResponse>> HardDeleteGrade(int id)
        {
            if (id <= 0)
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.Success = false;
                return BadRequest(_response);
            }

            _response.Result = await _gradeRepository.HardDeleteGrade(id);
            if ((bool)_response.Result)
            {
                _response.Success = true;
                _response.StatusCode = HttpStatusCode.NoContent;
                return Ok(_response);
            }
            else
            {
                _response.Success = false;
                _response.StatusCode = HttpStatusCode.NotFound;
                return NotFound(_response);
            }

        }

    }
}
