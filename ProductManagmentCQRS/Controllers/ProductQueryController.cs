using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductManagmentCQRS.CommandModel;
using ProductManagmentCQRS.Interface;
using ProductManagmentCQRS.QueryModel;

namespace ProductManagmentCQRS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductQueryController : ControllerBase
    {
        private readonly IQueryServices iqueryServices;
        public ProductQueryController(IQueryServices iqueryServices)
        {
            this.iqueryServices = iqueryServices;
        }

        [HttpGet("getProductById")]
        public IActionResult GetProductByID(int id)
        {
            ProductReadModel products = iqueryServices.GetProductById(id);
            if (products != null)
            {
                return Ok(new ResponseModel<ProductReadModel> { status = true, message = "successfully get product", data = products });
            }
            return BadRequest(new ResponseModel<string> { status = false, message = "unable to get product" });
        }

        [HttpGet("getAllProduct")]
        public IActionResult GetAllProducts()
        {
            IEnumerable<ProductReadModel> products = iqueryServices.GetAllProducts();
            if(products != null)
            {
                return Ok(new ResponseModel<IEnumerable<ProductReadModel>> { status = true, message = "successfully get all products", data = products });
            }
            return BadRequest(new ResponseModel<string> { status = false, message = "unable to get all products" });
        }
    }
}
