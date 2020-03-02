using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace GroceryMarketLibrary
{
    public class PointOfSaleTerminal : IPointOfSaleTerminal
    {
        /// <summary>
        /// _productList is a private product list in this PointOfSaleTerminal.
        /// This is set by the SetProductList(List<IProduct> productList) function.
        /// </summary>
        private List<IProduct> _productList;

        /// <summary>
        /// _productCodeList is a protected product code list in this PointOfSaleTerminal.
        /// This is set by the SetProductList(List<IProduct> productList) function.
        /// This list collects all the product codes of the products in the productList given to 
        /// the SetProductList(List<IProduct> productList) function.
        /// </summary>
        protected List<string> _productCodeList;

        /// <summary>
        /// _scannedProductCodes is a protected dictionary for storing and counting the scanned product code.
        /// The key represents product code. The value represents the number of products scanned.
        /// (i.e. IDictionary<productCode, quantity>)
        /// The scanned product code will be accumulagted in this dictionary.
        /// This list will be cleared after calculation is done in the CalculateTotal() function.
        /// </summary>
        protected IDictionary<string, int> _scannedProductCodes = new Dictionary<string, int>();

/* for considering to change _productCodeList and _scannedProductCodes from protected back to private
        /// <summary>
        /// GetProductCodeList() gives private List<string> _productCodeList
        /// </summary>
        /// <returns>List<string> _productCodeList</returns>
        public List<string> GetProductCodeList()
        {
            return this._productCodeList;
        }

        /// <summary>
        /// GetScannedProductCodes() gives private IDictionary<string, int> _scannedProductCodes.
        /// </summary>
        /// <returns>IDictionary<string, int> _scannedProductCodes</returns>
        public IDictionary<string, int> GetScannedProductCodes()
        {
            return this._scannedProductCodes;
        }
*/
        /// <summary>
        /// TerminalCode is a getter-setter method 
        /// for a unique string code respresenting a terminal in the grocery market
        /// For now here, this terminal is not a compulsory property.
        /// </summary>
        public string TerminalCode { get; set; }

        /// <summary>
        /// SetProductList(argument) deeply copies a product list into the terminal
        /// and also creates this._productCodeList for the use of ScanProduct(string productCode) function.
        /// (note: the references of the lists in the argument and this terminal are different)
        /// </summary>
        /// <param name="productList">is a list of product which is deeply copied into this terminal</param>
        /// <returns>If return 1, this function correctly copies the given list. Otherwise, return 0</returns>
        public int SetProductList(List<IProduct> productList)
        {
            // this function return done = 0 if this function cannot set the product list properly
            // otherwise, return done = 1 
            int done = 0;

            // create a new list assigned to the private _productList
            _productList = new List<IProduct>();

            // create a new list assigned to the private _productCodeList
            _productCodeList = new List<string>();

            // ---- deep copy productList (for preventing the change of this._productList using the productList reference outside this function and this class)
            // ---- and also set the private List<string> _productCodeList
            foreach (IProduct product in productList)
            {
                // make deep copy of productList
                this._productList.Add(product);
                // set _productCodeList
                this._productCodeList.Add(product.ProductCode);
            }
            // ^^^^ deep copy productList            
            // ^^^^ and also set the private List<string> _productCodeList

            //The line below is for testing only. If uncomment the line below, failure of test 7 in GroceryMarketLibraryTests is correct.
            //this._productList.Add(new Product("C"));

            // ---- compare the lists before and after deep copy above
            var firstNotSecond = productList.Except(this._productList).ToList();
            var secondNotFirst = this._productList.Except(productList).ToList();
            if (!firstNotSecond.Any() && !secondNotFirst.Any())
            {
                // assign the contents of productList to the private this._productList
                //this._productList = myProductList;
                done = 1;
            }
            else
            {
                this._productList = null;
            }
            // ^^^^ compare the lists before and after deep copy above

            return done;
        }

        /// <summary>
        /// GetProductList() return the product list in the terminal.
        /// (note: A deep copy of the list is return from this function.
        /// The references of the returned list from this function and list in this terminal are different.
        /// This prevents the change of this._productList using the reference of the returned productList outside this function and this class)
        /// </summary>
        /// <returns>A deep copy of List<IProduct> of this terminal</returns>
        public List<IProduct> GetProductList()
        {
            // create a new list which will be assigned to be a deep copy of the private this._productList
            List<IProduct> returnProductList = new List<IProduct>();

            // ---- deep copy productList (for preventing the change of this._productList using the returnProductList reference outside this function and this class)
            foreach (IProduct product in this._productList)
            {
                returnProductList.Add(product);
            }
            // ^^^^ deep copy productList  

            //The line below is for testing only. If uncomment the line below, failure of test 8 in GroceryMarketLibraryTests is correct.
            //returnProductList.Add(new Product("C"));

            // ---- compare the lists before and after deep copy above
            var firstNotSecond = returnProductList.Except(this._productList).ToList();
            var secondNotFirst = this._productList.Except(returnProductList).ToList();
            if (!(!firstNotSecond.Any() && !secondNotFirst.Any()))
            {
                // if the copy of list is incorrect, this function returns null
                returnProductList = null;
            }
            // ^^^^ compare the lists before and after deep copy above

            return returnProductList;
        }

        /// <summary>
        /// SetPricing(arguments) sets the price parameters of the given product 
        /// which product code is specified at the first argument.
        /// The products have to be in the product list which is set by the SetProductList(argument) function.
        /// The price parameters include product unit price, bulk quantity and bulk price.
        /// The product bulk quantity and bulk price are optional.
        /// This function would set the bulk quantity and bulk price to be 1 and unit price respectively
        /// if they are not given.
        /// </summary>
        /// <param name="productCode">product code of the product which is in the product list of the terminal and has the price parameters to be set in this function</param>
        /// <param name="productUnitPrice">unit price of the product</param>
        /// <param name="productBulkQty">bulk quantity of the product to be optional with default value of 1</param>
        /// <param name="productBulkPrice">bulk price of the product to be optional with default value of the unit price</param>
        /// <returns>
        /// This function returns 1 to indicate the parameters given in the augrment are properly set in the terminal. 
        /// Otherwise, the function does not work properly.
        /// </returns>
        public int SetPricing(string productCode, float productUnitPrice, int productBulkQty = 1, float productBulkPrice = 0.0f)
        {
            int done = 0;
            if (productBulkPrice == 0.0f & productBulkQty == 1)
            {
                productBulkPrice = productUnitPrice;
            }
            List<IProduct> myProductList = this._productList;
            IProduct myProduct = myProductList.Find(x => x.ProductCode == productCode);
            myProduct.ProductUnitPrice = productUnitPrice;
            myProduct.ProductBulkQty = productBulkQty;
            myProduct.ProductBulkPrice = productBulkPrice;
            done = 1;
            return done;
        }

        /// <summary>
        /// ScanProduct(string productCode) checks if the productCode exists in the product list.
        /// The product code would be counted by _scannedProducts: Dictionary<string, int>
        /// if the corresponding product exists in the product list.
        /// After the check and count are done, this function returns 1. Otherwise, it returns 0.
        /// </summary>
        /// <param name="productCode">
        /// The product code of the product which is scanned. 
        /// The product and its code have to exist in the product list.
        /// </param>
        /// <returns>
        /// This function returns integer 1, after the check and count are done.
        /// Otherwise, it returns 0.
        /// </returns>
        public int ScanProduct(string productCode)
        {
            int done = 0;

            if (!this._productCodeList.Contains(productCode))
            {
                return done;
            }

            if (this._scannedProductCodes.ContainsKey(productCode))
            {
                this._scannedProductCodes[productCode] += 1;
                done = 1;
            }
            else
            {
                this._scannedProductCodes.Add(productCode, 1);
                done = 1;
            }

            return done;
        }

        /// <summary>
        /// CalculateTotal() calculates the total price of all the scanned products according to 
        /// the prices given in the this._productList.
        /// After the calculation, this._scannedProductCodes will be cleared. 
        /// Therefore, CalculateTotal() cannot be used twice without productScan between them. 
        /// The second CalculateTotal() would return zero if there is no productScan between that two CalculateTotal().
        /// </summary>
        /// <returns>Total price of the scanned products</returns>
        public float CalculateTotal()
        {
            List<IProduct> myProductList = _productList;
            IDictionary<string, int> myScannedProductCodes = this._scannedProductCodes;
            IProduct myProduct;
            float totalPrice = 0;
            int scannedQty;
            foreach (KeyValuePair<string, int> productCodePair in myScannedProductCodes)
            {
                myProduct = myProductList.Find(productX => productX.ProductCode == productCodePair.Key);
                scannedQty = productCodePair.Value;
                totalPrice += (scannedQty / myProduct.ProductBulkQty) * myProduct.ProductBulkPrice + (scannedQty % myProduct.ProductBulkQty) * myProduct.ProductUnitPrice;
            }
            //clear _scannedProductCodes dictionary for next scan and calculation
            this._scannedProductCodes.Clear();
            return totalPrice;
        }
    }
}
