using ECommerceWeb.DataAccess.Repository;
using ECommerceWeb.Models;
using ECommerceWeb.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ECommerceWeb.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = SD.Role_Admin)]
public class ProductController : Controller
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IWebHostEnvironment _webHostEnvironment;

    public ProductController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
    {
        _unitOfWork = unitOfWork;
        _webHostEnvironment = webHostEnvironment;
    }

    public IActionResult Index()
    {
        var products = _unitOfWork.Product.GetAll(includeProperties: "Category");
        return View(products);
    }

    public IActionResult Upsert(int? id)
    {
        ViewBag.Categories = _unitOfWork.Category.GetAll()
            .Select(c => new SelectListItem { Text = c.Name, Value = c.Id.ToString() });

        if (id == null || id == 0)
            return View(new Product());

        var product = _unitOfWork.Product.Get(p => p.Id == id);
        if (product == null) return NotFound();
        return View(product);
    }

    [HttpPost]
    public IActionResult Upsert(Product product, IFormFile? file)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.Categories = _unitOfWork.Category.GetAll()
                .Select(c => new SelectListItem { Text = c.Name, Value = c.Id.ToString() });
            return View(product);
        }

        if (file != null)
        {
            string wwwRootPath = _webHostEnvironment.WebRootPath;
            string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            string productPath = Path.Combine(wwwRootPath, "images", "products");

            Directory.CreateDirectory(productPath);

            if (!string.IsNullOrEmpty(product.ImageUrl))
            {
                var oldImagePath = Path.Combine(wwwRootPath, product.ImageUrl.TrimStart('/'));
                if (System.IO.File.Exists(oldImagePath))
                    System.IO.File.Delete(oldImagePath);
            }

            using var fileStream = new FileStream(
                Path.Combine(productPath, fileName), FileMode.Create);
            file.CopyTo(fileStream);
            product.ImageUrl = "/images/products/" + fileName;
        }

        if (product.Id == 0)
        {
            _unitOfWork.Product.Add(product);
            TempData["success"] = "Producto creado correctamente";
        }
        else
        {
            _unitOfWork.Product.Update(product);
            TempData["success"] = "Producto actualizado correctamente";
        }

        _unitOfWork.Save();
        return RedirectToAction(nameof(Index));
    }

    [HttpDelete]
    public IActionResult Delete(int id)
    {
        var product = _unitOfWork.Product.Get(p => p.Id == id);
        if (product == null)
            return Json(new { success = false, message = "Error al eliminar" });

        if (!string.IsNullOrEmpty(product.ImageUrl))
        {
            var oldImagePath = Path.Combine(_webHostEnvironment.WebRootPath,
                product.ImageUrl.TrimStart('/'));
            if (System.IO.File.Exists(oldImagePath))
                System.IO.File.Delete(oldImagePath);
        }

        _unitOfWork.Product.Remove(product);
        _unitOfWork.Save();
        return Json(new { success = true, message = "Producto eliminado" });
    }
    public IActionResult Delete(int? id)
    {
        var productToBeDeleted = _unitOfWork.Product.Get(u => u.Id == id);
        if (productToBeDeleted == null)
        {
            return Json(new { success = false, message = "Error al borrar el producto." });
        }
         if (!string.IsNullOrEmpty(productToBeDeleted.ImageUrl))
         {
             var imagePath = Path.Combine(_webHostEnvironment.WebRootPath, productToBeDeleted.ImageUrl.TrimStart('\\', '/'));
             if (System.IO.File.Exists(imagePath))
             {
                 System.IO.File.Delete(imagePath);
             }
         }

        _unitOfWork.Product.Remove(productToBeDeleted);
        _unitOfWork.Save();

        return Json(new { success = true, message = "Producto eliminado correctamente." });
    }
}