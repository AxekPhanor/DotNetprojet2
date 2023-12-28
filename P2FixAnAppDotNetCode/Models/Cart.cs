using System.Collections.Generic;
using System.Linq;

namespace P2FixAnAppDotNetCode.Models
{
    /// <summary>
    /// The Cart class
    /// </summary>
    public class Cart : ICart
    {
        private List<CartLine> cartList = new List<CartLine>();

        /// <summary>
        /// Read-only property for display only
        /// </summary>
        public IEnumerable<CartLine> Lines => GetCartLineList();

        /// <summary>
        /// Return the actual cartline list
        /// </summary>
        /// <returns></returns>
        private List<CartLine> GetCartLineList()
        {
            return cartList;
        }

        private void SetCartLineList(List<CartLine> cartList)
        {
            this.cartList = cartList;
        }

        /// <summary>
        /// Adds a product in the cart or increment its quantity in the cart if already added
        /// </summary>//
        public void AddItem(Product product, int quantity)
        {
            // TODO implement the method
            var line = cartList.Exists(l => l.Product.Id == product.Id);
            if (line)
            {
                var cart = cartList.First(l => l.Product.Id == product.Id);
                cart.Quantity += quantity;
            }
            else
            {
                cartList.Add(new CartLine()
                {
                    OrderLineId = cartList.Count(),
                    Quantity = quantity,
                    Product = product
                });
            }
        }

        /// <summary>
        /// Removes a product form the cart
        /// </summary>
        public void RemoveLine(Product product) =>
            GetCartLineList().RemoveAll(l => l.Product.Id == product.Id);

        /// <summary>
        /// Get total value of a cart
        /// </summary>
        public double GetTotalValue()
        {
            // TODO implement the method
            double total = 0;
            foreach(var line in GetCartLineList())
            {
                total += + line.Quantity * line.Product.Price;
            }
            return total;
        }

        /// <summary>
        /// Get average value of a cart
        /// </summary>
        public double GetAverageValue()
        {
            // TODO implement the method
            double total = 0;
            int nbProductsInCart = 0;
            foreach (var line in GetCartLineList())
            {
                total += +line.Quantity * line.Product.Price;
                nbProductsInCart += line.Quantity;
            }
            return total / nbProductsInCart;
        }

        /// <summary>
        /// Looks after a given product in the cart and returns if it finds it
        /// </summary>
        public Product FindProductInCartLines(int productId)
        {
            // TODO implement the method
            var line = cartList.First(l => l.Product.Id == productId);
            return line.Product;
        }

        /// <summary>
        /// Get a specifid cartline by its index
        /// </summary>
        public CartLine GetCartLineByIndex(int index)
        {
            return Lines.ToArray()[index];
        }

        /// <summary>
        /// Clears a the cart of all added products
        /// </summary>
        public void Clear()
        {
            List<CartLine> cartLines = GetCartLineList();
            cartLines.Clear();
        }
    }

    public class CartLine
    {
        public int OrderLineId { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }
    }
}
