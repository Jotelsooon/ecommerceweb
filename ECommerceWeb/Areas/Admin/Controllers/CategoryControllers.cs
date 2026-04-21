using ECommerceWeb.DataAccess.Repository;
using ECommerceWeb.Models;
using ECommerceWeb.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceWeb.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = SD.Role_Admin)]
public class CategoryController : Controller
{
    private readonly IUnitOfWork _unitOfWork;

    public CategoryController(IUnitOfWork unitOfWork)
        => _unitOfWork = unitOfWork;

    public IActionResult Index()
    {
        var categories = _unitOfWork.Category.GetAll();
        return View(categories);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Create(Category category)
    {
        if (!ModelState.IsValid) return View(category);
        _unitOfWork.Category.Add(category);
        _unitOfWork.Save();
        TempData["success"] = "Categoría creada correctamente";
        return RedirectToAction(nameof(Index));
    }

    public IActionResult Edit(int id)
    {
        var category = _unitOfWork.Category.Get(c => c.Id == id);
        if (category == null) return NotFound();
        return View(category);
    }

    [HttpPost]
    public IActionResult Edit(Category category)
    {
        if (!ModelState.IsValid) return View(category);
        _unitOfWork.Category.Update(category);
        _unitOfWork.Save();
        TempData["success"] = "Categoría actualizada correctamente";
        return RedirectToAction(nameof(Index));
    }

    public IActionResult Delete(int id)
    {
        var category = _unitOfWork.Category.Get(c => c.Id == id);
        if (category == null) return NotFound();
        return View(category);
    }

    [HttpPost, ActionName("Delete")]
    public IActionResult DeletePOST(int id)
    {
        var category = _unitOfWork.Category.Get(c => c.Id == id);
        if (category == null) return NotFound();
        _unitOfWork.Category.Remove(category);
        _unitOfWork.Save();
        TempData["success"] = "Categoría eliminada correctamente";
        return RedirectToAction(nameof(Index));
    }
}