using System;
using System.Threading.Tasks;
using Eos.Abstracts.Bl;
using Eos.Abstracts.Entities;
using Eos.Abstracts.Models;
using Eos.Abstracts.Models.Dto;
using Eos.Abstracts.Models.Pages;
using Mapster;
using Microsoft.AspNetCore.Mvc;

namespace EosTestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemController: ControllerBase
    {
        private readonly IItemService _itemService;

        public ItemController(IItemService itemService)
        {
            _itemService = itemService;
        }

        [HttpGet("childs")]
        public async Task<IActionResult> GetChildItems([FromQuery] Pager pager)
        {
            if (!Guid.TryParse(pager.Query, out var parentId))
            {
                return BadRequest("Could not recognize ParentId string format");
            }
            
            var page = await _itemService.GetChildItems(pager, parentId);
            var pageDto = page.Adapt<ItemDtoPage>();
            return Ok(pageDto);
        }

        [HttpGet("ctechilds")]
        public async Task<IActionResult> GetCteChildItems([FromQuery] Pager pager)
        {
            if (!Guid.TryParse(pager.Query, out var parentId))
            {
                return BadRequest("Could not recognize ParentId string format");
            }
            
            var page = await _itemService.GetCteChildItems(pager, parentId);
            var pageDto = page.Adapt<ItemDtoPage>();
            return Ok(pageDto);
        }

        [HttpGet("parents")]
        public async Task<ItemDtoPage> GetParents([FromQuery]Pager pager)
        {
            var page = await _itemService.GetParents(pager);
            return page.Adapt<ItemDtoPage>();
        } 

        [HttpPost("initial-fill")]
        public Task InitialFill()
        {
            return _itemService.InitialFilling();
        }
    }
}