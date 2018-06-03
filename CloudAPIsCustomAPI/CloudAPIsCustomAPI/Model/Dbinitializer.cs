using System.Linq;

namespace Model
{
    public class DBInitializer
    {
        public static void Initialize(LibraryContext context)
        {
            //Create the db if not yet exists
            context.Database.EnsureCreated();

            //Are there already Printers present ?
            if (!context.Printers.Any())
            {
                var makerbot = new Brand()
                {
                    BrandName = "makerbot"
                };
                context.Brands.Add(makerbot);
                var ultimaker = new Brand()
                {
                    BrandName = "Ultimaker"
                };
                context.Brands.Add(ultimaker);

                //Create new Printer
                var printr = new Printer()
                {
                    Product = "REPLICATOR+",
                    PrintVolume = "XYZ: 295x195x165mm",
                    PrintMethod = "FDM", 
                    Price = 2499,
                    Brand = makerbot
                };
                //Add the Printer to the collection of Printers
                context.Printers.Add(printr);
                printr = new Printer()
                {
                    Product = "S5",
                    PrintVolume = "XYZ: 330x240x300mm",
                    PrintMethod = "FDM",
                    Price = 5495,
                    Brand = ultimaker
                };
                context.Printers.Add(printr);
                //Save all the changes to the DB
                context.SaveChanges();
            }
        }
    }
}