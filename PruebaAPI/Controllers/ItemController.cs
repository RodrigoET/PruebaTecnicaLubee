using Data;
using Data.DTOs.ItemDTO;
using Data.Repositories.ItemRepo;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace PruebaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemController : ControllerBase
    {
        private readonly IItemRepository _itemRepository;
        private APIResponse _response;

        public ItemController(IItemRepository itemRepository)
        {
            _itemRepository = itemRepository;
            _response = new();
        }

        [HttpGet("GetAllItems")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> GetAllItems()
        {
            _response.Success = true;
            _response.Result = await _itemRepository.GetAllItems();
            _response.StatusCode = HttpStatusCode.OK;
            return Ok(_response);
        }

        [HttpGet("GetItemById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> GetItemById(int id)
        {
            if (id <= 0)
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.Success = false;
                return BadRequest(_response);
            }

            var school = await _itemRepository.GetItemById(id);
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

        [HttpGet("GetItemByName")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> GetItemByName(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.Success = false;
                //TODO agregar Errors = "Field "Name" is required"
                return BadRequest(_response);
            }

            var items = await _itemRepository.GetItemByName(name);
            if (items == null || items.Count() == 0)
            {
                _response.StatusCode = HttpStatusCode.NotFound;
                _response.Success = false;
                return NotFound(_response);
            }

            _response.StatusCode = HttpStatusCode.OK;
            _response.Success = true;
            _response.Result = items;
            return Ok(_response);
        }

        [HttpPost("CreateItem")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<APIResponse>> CreateItem([FromBody] CreateItemDTO createDTO)
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
            _response.Result = await _itemRepository.CreateItem(createDTO);
            return Created("created", _response);
        }

        [HttpPut("UpdateItem")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<APIResponse>> UpdateItem([FromBody] UpdateItemDTO updateDTO)
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

            _response.Result = await _itemRepository.UpdateItem(updateDTO);
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

        [HttpPatch("SoftDeleteItem")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<APIResponse>> SoftDeleteItem(int id)
        {
            if (id <= 0)
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.Success = false;
                return BadRequest(_response);
            }

            _response.Result = await _itemRepository.SoftDeleteItem(id);
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

        [HttpDelete("HardDeleteItem")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<APIResponse>> HardDeleteItem(int id)
        {
            if (id <= 0)
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.Success = false;
                return BadRequest(_response);
            }

            _response.Result = await _itemRepository.HardDeleteItem(id);
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
