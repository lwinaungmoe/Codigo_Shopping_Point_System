using CodigoShopping.Domain.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Mobile.API.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Mobile.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController,Authorize]
    public class CatelogController : ControllerBase
    {
        private readonly ICatelogItemsService _itemsService;
        public CatelogController(ICatelogItemsService itemsService)
        {
            _itemsService = itemsService;
        }

        // GET: api/<CatelogTypeControllercs>
        [HttpGet]
        public async Task<List<CatalogItem>> GetAll()
        {
            return await _itemsService.GetAllGetCatelogItems();
        }

        // GET api/<CatelogTypeControllercs>/5
        [HttpGet("{id}")]
        public async Task<CatalogItem> Get(int id)
        {
            return await _itemsService.GetCatelogItemsById(id);
        }

        // POST api/<CatelogTypeControllercs>
        [HttpPost("Add")]
        public async Task<CatalogItem> Insert(CatalogItem catalogItem)
        {
            return await _itemsService.InsertGetCatelogItems(catalogItem);
        }

        [HttpPost("Update")]
        public async Task<CatalogItem> Update(CatalogItem catalogItem)
        {
            return await _itemsService.UpdateGetCatelogItems(catalogItem);
        }

        // DELETE api/<CatelogTypeControllercs>/5
        [HttpDelete("{id}")]
        public async Task<CatalogItem> Delete(int id)
        {
            return await _itemsService.DeletedGetCatelogItems(id);
        }
    }
}
