using Data.DTOs.SchoolDTO;
using Data.Repositories.SchoolRepo;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PruebaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SchoolController : ControllerBase
    {
        private readonly ISchoolRepository _schoolRepository;

        public SchoolController(ISchoolRepository schoolRepository)
        {
            _schoolRepository = schoolRepository; 
        }

        [HttpGet]
        public async Task<IActionResult> GetAllSchools()
        {
            return Ok(await _schoolRepository.GetAllSchools());
        }

        [HttpPost]
        public async Task<IActionResult> CreateSchool([FromBody] CreateSchoolDTO createDTO)
        {
            if (createDTO == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var created = await _schoolRepository.CreateSchool(createDTO);
            return Created("created",created);
        }

        [HttpPut]
        public async Task<IActionResult> SoftDeleteSchool(int id)
        {
            if (id <= 0) {
                return BadRequest();
            }
            await _schoolRepository.SoftDeleteSchool(id);

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> HardDeleteSchool(int id)
        {
            await _schoolRepository.HardDeleteSchool(id);

            return NoContent();
        }
    }
}
