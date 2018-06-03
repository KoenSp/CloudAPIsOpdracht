using System.Collections.Generic;
using Newtonsoft.Json;

namespace Model
{
    public class Brand
    {
        public int Id { get; set; }
        public string BrandName { get; set; }
        [JsonIgnore]
        public ICollection<Printer> Printers { get; set; }
    }
}
