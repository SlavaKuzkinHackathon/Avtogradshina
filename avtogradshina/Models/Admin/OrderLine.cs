using System.Collections.Generic;

namespace avtogradshina.Models.Admin
{
  public class OrderLine 
  {
    public long Id { get; set; }
    public long ProductId { get; set; }
    public Product Product { get; set; }
    public int Quant { get; set; }
    public long OrderId { get; set; }
    public Order Order { get; set; }
  }
}