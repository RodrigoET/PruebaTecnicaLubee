using Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Data.DTOs.ContractDTO;
using Data.Repositories.ContractRepo;
using System.Net;

namespace PruebaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContractController : ControllerBase
    {
        private readonly IContractRepository _contractRepository;
        protected APIResponse _response;

        public ContractController(IContractRepository gradeRepository)
        {
            _contractRepository = gradeRepository;
            _response = new();
        }

        [HttpPost("CreateContract")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<APIResponse>> CreateGrade([FromBody] CreateContractDTO createDTO)
        {
            if (!ModelState.IsValid)
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.Success = false;
                _response.Errors.Add(ModelState.ToString());
                return BadRequest(_response);
            }

            _response.StatusCode = HttpStatusCode.Created;
            _response.Success = true;
            _response.Result = await _contractRepository.CreateContract(createDTO);
            return Created("created", _response);

        }

        [HttpGet("GetContractById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> GetContractById(int id)
        {
            if (id <= 0)
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.Success = false;
                return BadRequest(_response);
            }

            var contract = await _contractRepository.GetContractById(id);
            if (contract == null)
            {
                _response.StatusCode = HttpStatusCode.NotFound;
                _response.Success = false;
                return NotFound(_response);

            }

            _response.Result = contract;
            _response.StatusCode = HttpStatusCode.OK;
            _response.Success = true;
            return Ok(_response);
        }
    }
}
