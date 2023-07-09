using CodigoShopping.Domain.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Mobile.API.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Mobile.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController, Authorize]
    public class CatelogTypeController : ControllerBase
    {
        private readonly ICatelogTypeServices _catelogTypeServices;

        public CatelogTypeController(ICatelogTypeServices catelogTypeServices)
        {
            _catelogTypeServices = catelogTypeServices;
        }

        // GET: api/<CatelogTypeControllercs>
        [HttpGet]
        public async Task<List<CatalogType>> GetAll()
        {
            return await _catelogTypeServices.GetAllCatelogTypes();
        }

        // GET api/<CatelogTypeControllercs>/5
        [HttpGet("{id}")]
        public async Task<CatalogType> Get(int id)
        {
            return await _catelogTypeServices.GetCatelogTypeById(id);
        }
        
        // POST api/<CatelogTypeControllercs>
        [HttpPost("Add")]
        public async Task<CatalogType> Insert(CatalogType catalogType)
        {
            return await _catelogTypeServices.InsertCatelogType(catalogType);
        }

        [HttpPost("Update")]
        public async Task<CatalogType> Update(CatalogType catalogType)
        {
            return await _catelogTypeServices.UpdateCatelogType(catalogType);
        }

        // DELETE api/<CatelogTypeControllercs>/5
        [HttpDelete("{id}")]
        public async Task<CatalogType> Delete(int id)
        {
            return await _catelogTypeServices.DeletedCatelogType(id);
        }
    }
}