﻿using Data;
using Data.DTOs.SchoolDTO;
using Data.Repositories.SchoolRepo;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace PruebaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SchoolController : ControllerBase
    {
        private readonly ISchoolRepository _schoolRepository;
        protected APIResponse _response;

        public SchoolController(ISchoolRepository schoolRepository)
        {
            _schoolRepository = schoolRepository; 
            _response = new ();
        }

        [HttpGet("GetAllSchools")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> GetAllSchools()
        {
            _response.Success = true;
            _response.Result = await _schoolRepository.GetAllSchools();
            _response.StatusCode = HttpStatusCode.OK;
            return Ok(_response);
        }

        [HttpGet("GetSchoolById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType (StatusCodes.Status404NotFound)]
        [ProducesResponseType (StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> GetSchoolById(int id)
        {
            if (id <= 0) {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.Success = false;
                return BadRequest(_response);
            }

            var school = await _schoolRepository.GetSchoolById(id);
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

        [HttpGet("GetSchoolByName")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> GetSchoolByName(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.Success = false;
                //TODO agregar Errors = "Field "Name" is required"
                return BadRequest(_response);
            }

            var schools = await _schoolRepository.GetSchoolByName(name);
            if (schools == null || schools.Count() == 0)
            {
                _response.StatusCode= HttpStatusCode.NotFound;
                _response.Success=false;
                return NotFound(_response);
            }

            _response.StatusCode = HttpStatusCode.OK;
            _response.Success = true; 
            _response.Result = schools;
            return Ok(_response);
        }

        [HttpPost("CreateSchool")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<APIResponse>> CreateSchool([FromBody] CreateSchoolDTO createDTO)
        {
            if (createDTO == null)
            {
                _response.StatusCode=HttpStatusCode.BadRequest;
                _response.Success=false;
                return BadRequest(_response);
            }

            _response.StatusCode = HttpStatusCode.Created; 
            _response.Success = true;
            _response.Result = await _schoolRepository.CreateSchool(createDTO);
            return Created("created",_response);
        }

        [HttpPut("UpdateSchool")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<APIResponse>> UpdateSchool([FromBody] UpdateSchoolDTO updateDTO)
        {
            if (updateDTO == null)
            {
                _response.StatusCode=HttpStatusCode.BadRequest;
                _response.Success=false;
                return BadRequest(_response);
            }

            if (!ModelState.IsValid)
            {
                _response.StatusCode = HttpStatusCode.BadRequest; 
                _response.Success = false;
                _response.Errors.Add(ModelState.ToString());
                return BadRequest(_response);
            }

            _response.Result = await _schoolRepository.UpdateSchool(updateDTO);
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

        [HttpPatch("SoftDeleteSchool")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<APIResponse>> SoftDeleteSchool(int id)
        {
            if (id <= 0) {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.Success = false;
                return BadRequest(_response);
            }

            _response.Result = await _schoolRepository.SoftDeleteSchool(id);
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

        [HttpDelete("HardDeleteSchool")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<APIResponse>> HardDeleteSchool(int id)
        {
            if (id <= 0)
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.Success = false;
                return BadRequest(_response);
            }

            _response.Result = await _schoolRepository.HardDeleteSchool(id);
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
