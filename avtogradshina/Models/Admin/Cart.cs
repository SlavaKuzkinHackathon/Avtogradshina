using System.Collections.Generic;
using System.Linq;

namespace avtogradshina.Models.Admin
{
    public class Cart
    {
        private List<OrderLine> selections = new List<OrderLine>();

        public Cart AddItem(Product p, int quant)
        {
            OrderLine line = selections
                .Where(l => l.ProductId == p.Id).FirstOrDefault();
            if (line != null)
            {
                line.Quant += quant;
            }
            else
            {
                selections.Add(new OrderLine
                {
                    ProductId = p.Id,
                    Product = p,
                    Quant = quant
                });
            }
            return this;
        }

        public Cart RemoveItem(long productId)
        {
            selections.RemoveAll(l => l.ProductId == productId);
            return this;
        }

        public void Clear() => selections.Clear();

        public IEnumerable<OrderLine> Selections { get => selections; }
    }
}
