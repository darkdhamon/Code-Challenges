using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace Codility.WarehouseApi
{
    public class WarehouseController : Controller
    {
        private readonly IWarehouseRepository _warehouseRepository;

        public WarehouseController(IWarehouseRepository warehouseRepository)
        {
            _warehouseRepository = warehouseRepository;
        }

        // Return OkObjectResult(IEnumerable<WarehouseEntry>)
        public IActionResult GetProducts()
        {
            var availableProducts = _warehouseRepository.GetProductRecords(product => product.Quantity > 0).Select(product => new WarehouseEntry
            {
                ProductId = product.ProductId,
                Quantity = product.Quantity
            });
            return Ok(availableProducts);
        }

        // Return OkResult, BadRequestObjectResult(NotPositiveQuantityMessage), or BadRequestObjectResult(QuantityTooLowMessage)
        public IActionResult SetProductCapacity(int productId, int capacity)
        {
            if (capacity <= 0)
            {
                return BadRequest("NotPositiveQuantityMessage");
            }
            var currentRecord = _warehouseRepository.GetProductRecords(product => product.ProductId == productId).FirstOrDefault();
            if (currentRecord.Quantity > capacity)
            {
                return BadRequest("QuantityTooLowMessage");
            }
            _warehouseRepository.SetCapacityRecord(productId, capacity);
            return Ok();
        }

        // Return OkResult, BadRequestObjectResult(NotPositiveQuantityMessage), or BadRequestObjectResult(QuantityTooHighMessage)
        public IActionResult ReceiveProduct(int productId, int qty)
        {
            if (qty <= 0)
            {
                return BadRequest("NotPositiveQuantityMessage");
            }
            var productRecord = _warehouseRepository.GetProductRecords(product => product.ProductId == productId).FirstOrDefault();
            var capacityRecord = _warehouseRepository.GetCapacityRecords(product => product.ProductId == productId)
                .FirstOrDefault();
            var currentQty = productRecord?.Quantity ?? 0;
            if (currentQty + qty > (capacityRecord?.Capacity??0))
            {
                return BadRequest("QuantityTooHighMessage");
            }
            _warehouseRepository.SetProductRecord(productId, currentQty + qty);
            return Ok();
        }

        // Return OkResult, BadRequestObjectResult(NotPositiveQuantityMessage), or BadRequestObjectResult(QuantityTooHighMessage)
        public IActionResult DispatchProduct(int productId, int qty)
        {
            if (qty <= 0)
                return BadRequest("NotPositiveQuantityMessage");
            var productRecord = _warehouseRepository.GetProductRecords(product => product.ProductId == productId).FirstOrDefault();
            var currentQty = productRecord?.Quantity ?? 0;
            if (currentQty < qty)
            {
                return BadRequest("QuantityTooHighMessage");
            }
            _warehouseRepository.SetProductRecord(productId, currentQty-qty);
            return Ok();
        }
    }
}