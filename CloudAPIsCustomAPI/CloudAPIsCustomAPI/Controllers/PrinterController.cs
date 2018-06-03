using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Model;

[Route("api/v1/printers")]

public class PrinterController : Controller
{
    private readonly LibraryContext context;

    public PrinterController(LibraryContext context)
    {
        this.context = context;
    }

    [HttpGet]         // api/v1/printers
    public List<Printer> GetAllPrinters(string product, string printMethod, int? page, string sort, int length = 1, string dir = "asc")
    {
        IQueryable<Printer> query = context.Printers;

        if (!string.IsNullOrWhiteSpace(product))
            query = query.Where(d => d.Product == product);
        if (!string.IsNullOrWhiteSpace(printMethod))
            query = query.Where(d => d.PrintMethod == printMethod);

        if (!string.IsNullOrWhiteSpace(sort))
        {
            switch (sort)
            {
                case "brand":
                    if (dir == "asc")
                        query = query.OrderBy(d => d.Brand);
                    else if (dir == "desc")
                        query = query.OrderByDescending(d => d.Brand);
                    break;
                case "price":
                    if (dir == "asc")
                        query = query.OrderBy(d => d.Price);
                    else if (dir == "desc")
                        query = query.OrderByDescending(d => d.Price);
                    break;
            }
        }

        if (page.HasValue)
            query = query.Skip(page.Value * length);
        query = query.Take(length);

        return query.ToList();
    }
    [Route("{id}")]   // api/v1/printers/2
    [HttpGet]
    public IActionResult GetPrinter(int id)
    {
        var printer = context.Printers
                    .Include(d => d.Brand)
                    .SingleOrDefault(d => d.Id == id);

        if (printer == null)
            return NotFound();

        return Ok(printer);
    }
    [Route("{product}/product")]   // api/v1/printers/2
    [HttpGet]
    public IActionResult GetPrinter(string product)
    {
        var printer = context.Printers
                    .Include(d => d.Brand)
                    .SingleOrDefault(d => d.Product == product);

        if (printer == null)
            return NotFound();

        return Ok(printer);
    }
    [Route("{id}/brand")]   // api/v1/printers/2
    [HttpGet]
    public IActionResult GetBrandForPrinter(int id)
    {
        var printer = context.Printers
                    .Include(d => d.Brand)
                    .SingleOrDefault(d => d.Id == id);
        if (printer == null)
            return NotFound();

        return Ok(printer.Brand);
    }

    [HttpPost]
    public IActionResult CreatePrinter([FromBody] Printer newPrinter)
    {
        //Printer toevoegen in de databank, Id wordt dan ook toegekend
        context.Printers.Add(newPrinter);
        context.SaveChanges();
        // Stuur een result 201 met het boek als content
        return Created("", newPrinter);
    }
    
    [HttpPut]
    public IActionResult UpdatePrinter([FromBody] Printer updatePrinter)
    {
        var orgPrinter = context.Printers.Find(updatePrinter.Id);
        if (orgPrinter == null)
            return NotFound();

        orgPrinter.Product = updatePrinter.Product;
        orgPrinter.PrintVolume = updatePrinter.PrintVolume;
        orgPrinter.PrintMethod = updatePrinter.PrintMethod;
        orgPrinter.Price = updatePrinter.Price;

        context.SaveChanges();
        return Ok(orgPrinter);
    }

    [Route("{id}")]
    [HttpDelete]
    public IActionResult DeletePrinter(int id)
    {
        var printer = context.Printers.Find(id);
        if (printer == null)
            return NotFound();

        //printer verwijderen ..
        context.Printers.Remove(printer);
        context.SaveChanges();
        //Standaard response 204 bij een gelukte delete
        return NoContent();
    }
}