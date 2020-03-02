using System;
using System.Collections.Generic;
using System.Text;

namespace GroceryMarketLibrary
{
    public class Product : IProduct
    {
        public Product(string code)
        {
            this.ProductCode = code;
        }
        /// <summary>
        /// ProductCode is a getter-setter method 
        /// for a unique string code respresenting a product in the grocery market
        /// </summary>
        public string ProductCode { get; set; }

        /// <summary>
        /// ProductUnitPrice is a getter-setter method 
        /// for a unit price of a product in the grocery market
        /// </summary>
        public float ProductUnitPrice { get; set; }

        /// <summary>
        /// ProductBulkQty is a getter-setter method 
        /// for a bulk quantity of a product in the grocery market.
        /// Customers purchasing in the bulk quantity may get discount on the purchasing
        /// according to particular terms and conditions at the time of purchasing.
        /// This ProductBulkQty may be equal to one when there is no bulk purchase discount.
        /// </summary>
        public int ProductBulkQty { get; set; }

        /// <summary>
        /// ProductBulkPrice is a getter-setter method
        /// for a discounted bulk price applying for a purchase with the precribed bulk quantity.
        /// This ProductBulkPrice would be equal to ProductUnitPrice 
        /// if the ProductBulkQty is one and no discount is given.
        /// </summary>
        public float ProductBulkPrice { get; set; }

        /// <summary>
        /// ProductName is a getter-setter method
        /// for the product name.
        /// Here assumed that ProductName is not a compulsory property
        /// </summary>
        public string ProductName { get; set; }
    }
}
