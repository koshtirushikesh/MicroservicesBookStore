using Microsoft.AspNetCore.Mvc;
using ProductManagmentCQRS.CommandModel;
using ProductManagmentCQRS.Interface;

namespace ProductManagmentCQRS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductCommandController : ControllerBase
    {
        private readonly ICommandServices iCommandServices;
        public ProductCommandController(ICommandServices iCommandServices)
        {
            this.iCommandServices = iCommandServices;
        }

        [HttpPost("addProduct")]
        public IActionResult AddProduct(ProductAddUpdateModel addProduct)
        {
            ProductAddUpdateModel product = iCommandServices.AddProduct(addProduct);
            if (product != null)
            {
                return Ok(new ResponseModel<ProductAddUpdateModel> { status = true, message = "successfully added product", data = product });
            }
            return BadRequest(new ResponseModel<string> { status = false, message = "unable to add product" });
        }

        [HttpPut("updateProduct")]
        public IActionResult UpdateProduct(ProductAddUpdateModel updateProduct, int productID)
        {
            ProductAddUpdateModel product = iCommandServices.UpdateProduct(updateProduct,productID);
            if (product != null)
            {
                return Ok(new ResponseModel<ProductAddUpdateModel> { status = true, message = "successfully update product", data = product });
            }
            return BadRequest(new ResponseModel<string> { status = false, message = "unable to update product" });
        }

        [HttpDelete("deleteProduct")]
        public IActionResult DeleteProduct(int productID)
        {
            bool result = iCommandServices.DeleteProduct(productID);
            if (result != null)
            {
                return Ok(new ResponseModel<bool> { status = true, message = "successfully deleted product" });
            }
            return BadRequest(new ResponseModel<string> { status = false, message = "unable to delete product" });
        }

    }
}
