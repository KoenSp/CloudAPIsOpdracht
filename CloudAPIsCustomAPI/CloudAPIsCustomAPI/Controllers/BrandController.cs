using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Model;

[Route("api/v1/brands")]
public class BrandsController : Controller
{
    private readonly LibraryContext context; 

    public BrandsController(LibraryContext context)
    {
        this.context = context; 
    }

    [HttpGet]
    public List<Brand> GetAllBrands()
    {
        return context.Brands.ToList(); 
    }

    [Route("{id}")]
    [HttpGet]
    public IActionResult GetBrand(int id)
    {
        var brand = context.Brands.Find(id);
        if (brand == null)
            return NotFound();

        return Ok(brand); 
    }

    [Route("{id}/printers")]
    [HttpGet]
    public IActionResult GetPrintersForBrand(int id)
    {
        var brand = context.Brands
                .Include(d => d.Printers)
                .SingleOrDefault(d => d.Id == id);

        if (brand == null)
            return NotFound();

        return Ok(brand.Printers);
    }

    [HttpPost]
    public IActionResult CreateBrand([FromBody] Brand newBrand)
    {
        context.Brands.Add(newBrand);
        context.SaveChanges();
        return Created("", newBrand);
    }

    [HttpPut]
    public IActionResult UpdateBrand([FromBody] Brand updateBrand)
    {
        var orgBrand = context.Brands.Find(updateBrand.Id);
        if (orgBrand == null)
            return NotFound();

        orgBrand.BrandName = updateBrand.BrandName;

        context.SaveChanges();
        return Ok(orgBrand);
    }

    [Route("{id}")]
    [HttpDelete]
    public IActionResult DeleteBrand(int id)
    {
        var brand = context.Brands.Find(id);
        if (brand == null)
            return NotFound(); //404
        
        context.Brands.Remove(brand);
        context.SaveChanges();
        return NoContent(); //204
    }
}
