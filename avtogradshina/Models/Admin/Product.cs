using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using avtogradshina.Models.Admin;

namespace avtogradshina.Models.Admin
{
    public class Product
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public int InQuantity { get; set; }
        public string NameImage { get; set; }
        public string Path { get; set; }

        public long CategoryId { get; set; }
        public Category Category { get; set; }

        internal static Product FirstOrDefault(Func<object, bool> p)
        {
            throw new NotImplementedException();
        }
    }
}