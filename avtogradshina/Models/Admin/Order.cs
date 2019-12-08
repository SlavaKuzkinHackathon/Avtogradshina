using System.Collections.Generic;

namespace avtogradshina.Models.Admin
{
    public class Order {
     public long Id { get; set; }
     public string CustomerName { get; set; }
     public string Address { get; set; }
     public string NamberTel { get; set; }
     public bool Shipped { get; set; }

    public IEnumerable<OrderLine> Lines { get; set; }
    }
}