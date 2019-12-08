using avtogradshina.Models.Admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace avtogradshina.ViewModels
{
    public class ProductViewModel
    {
        public string Name { get; set; }
        public int Price { get; set; }
        public int InQuantity { get; set; }
        public string NameImage { get; set; }
        public string Path { get; set; }

        public long CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
