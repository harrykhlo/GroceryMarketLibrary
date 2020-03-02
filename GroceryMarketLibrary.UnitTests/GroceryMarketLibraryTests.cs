using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace GroceryMarketLibrary.UnitTests
{
    /// <summary>
    /// IGetProductCodeList interface specifies the function signature used to get 
    /// the protected List<string> _productCodeList in the PointOfSaleTerminal class 
    /// for testing purpose.
    /// </summary>
    public interface IGetProductCodeList
    {
        List<string> GetProductCodeList();
    }

    /// <summary>
    /// IGetScannedProductCodes interface specifies the function signature used to get 
    /// the protected IDictionary<string, int> _scannedProductCodes in the PointOfSaleTerminal class 
    /// for testing purpose.
    /// </summary>
    public interface IGetScannedProductCodes
    {
        IDictionary<string, int> GetScannedProductCodes();
    }


    /// <summary>
    /// TestHelperTermainal class inherits from PointOfSaleTerminal class and 
    /// implements IPointOfSaleTerminal, IGetProductCodeList and IGetScannedProductCodes interfaces
    /// for having additional functions to test the protected List<string> _productCodeList and
    /// the protected IDictionary<string, int> _scannedProductCodes in the PointOfSaleTerminal class.
    /// </summary>
    public class TestHelperTermainal : PointOfSaleTerminal, IPointOfSaleTerminal, IGetProductCodeList, IGetScannedProductCodes
    {


        /// <summary>
        /// GetProductCodeList() gives protected List<string> _productCodeList
        /// </summary>
        /// <returns>List<string> _productCodeList</returns>
        public List<string> GetProductCodeList()
        {
            return this._productCodeList;
        }

        /// <summary>
        /// GetScannedProductCodes() gives protected IDictionary<string, int> _scannedProductCodes.
        /// </summary>
        /// <returns>IDictionary<string, int> _scannedProductCodes</returns>
        public IDictionary<string, int> GetScannedProductCodes()
        {
            return this._scannedProductCodes;
        }
    }

    /// <summary>
    /// GroceryMarketLibraryTests contains all the functions to test the development codes 
    /// including PointOfSaleTerminal and Product classes.
    /// </summary>
    [TestClass]
    public class GroceryMarketLibraryTests
    {
        //Test1: product.ProductCode
        [TestMethod]
        public void Test1_ProductCode_ProductCodeIsA_ProductCodeAreEqualA()
        {
            // Arrange
            var product = new Product("A");

            // Act
            string actual = product.ProductCode;

            // Assert
            string expected = "A"; //expected string of product.ProductCode
            Assert.AreEqual(expected, actual, "The product code is wrong and should be 'A'");
        }

        //Test2: product.ProductUnitPrice
        [TestMethod]
        public void Test2_ProductUnitPrice_ProductUnitPriceIs1_25_ProductUnitPriceAreEqual1_25()
        {
            // Arrange
            var product = new Product("A");

            // Act
            product.ProductUnitPrice = 1.25f;
            float actual = product.ProductUnitPrice;

            // Assert
            float expected = 1.25f; //expected value of product.ProductUnitPrice
            Assert.AreEqual(expected, actual, "The product unit price is wrong and should be 1.25f");
        }

        //Test3: product.ProductBulkQty
        [TestMethod]
        public void Test3_ProductBulkQty_ProductBulkQtyIs3_ProductBulkQtyAreEqual3()
        {
            // Arrange
            var product = new Product("A");

            // Act
            product.ProductBulkQty = 3;
            int actual = product.ProductBulkQty;

            // Assert
            int expected = 3; //expected value of product.ProductBulkQty
            Assert.AreEqual(expected, actual, "The Product Bulk Quantity is wrong and should be 3");
        }

        //Test4: product.ProductBulkPrice
        [TestMethod]
        public void Test4_ProductBulkPrice_ProductBulkPriceIs3_0_ProductBulkQtyAreEqual3_0()
        {
            // Arrange
            var product = new Product("A");

            // Act
            product.ProductBulkPrice = 3.0f;
            float actual = product.ProductBulkPrice;

            // Assert
            float expected = 3.0f; //expected value of product.ProductBulkPrice
            Assert.AreEqual(expected, actual, "The Product Bulk Price is wrong and should be 3.0");
        }

        //Test5: product.ProductName
        [TestMethod]
        public void Test5_ProductName_ProductNameIsNameOfProduct_ProductNameAreEqualNameOfProduct()
        {
            // Arrange
            var product = new Product("A");

            // Act
            product.ProductName = "NameOfProduct";
            string actual = product.ProductName;

            // Assert
            string expected = "NameOfProduct"; //expected string of product.ProductName
            Assert.AreEqual(expected, actual, "The Product Name is wrong and should be 'NameOfProduct'");
        }

        //Test6: terminal.TerminalCode
        [TestMethod]
        public void Test6_TerminalCode_TerminalCodeIsCodeOfTerminal_TerminalCodeAreEqualCodeOfTerminal()
        {
            // Arrange
            var terminal = new PointOfSaleTerminal();

            // Act
            terminal.TerminalCode = "CodeOfTerminal";
            string actual = terminal.TerminalCode;

            // Assert
            string expected = "CodeOfTerminal"; //expected string of terminal.TerminalCode
            Assert.AreEqual(expected, actual, "The Terminal Code is wrong and should be 'CodeOfTerminal'");
        }

        //Test7: terminal.SetProductList 
        [TestMethod]
        public void Test7_SetProductList_SetProductListSetList_SetProductListReturn1()
        {
            // Arrange
            var terminal = new PointOfSaleTerminal();
            List<IProduct> productListActual = new List<IProduct>
            {
                new Product("A"),
                new Product("B")
            };

            // Act
            int actual = terminal.SetProductList(productListActual);

            // Assert
            int expected = 1; //expected return value of terminal.SetProductList(productList)
            Assert.AreEqual(expected, actual, "Return value of terminal.SetProductList(productList) is wrong and should be 1");
        }

        //Test8: terminal.GetProductList 
        [TestMethod]
        public void Test8_GetProductList_SetProductListSetList_GetProductListReturnTheList()
        {
            // Arrange
            var terminal = new PointOfSaleTerminal();
            List<IProduct> productListActual = new List<IProduct>
            {
                new Product("A"),
                new Product("B")
            };
            terminal.SetProductList(productListActual);
            bool correct = false;

            // Act
            List<IProduct> actual = productListActual;

            // Assert
            List<IProduct> expected = terminal.GetProductList(); //expected return list of terminal.GetProductList()
            
            // ---- compare the lists before and after deep copy above
            var firstNotSecond = actual.Except(expected).ToList();
            var secondNotFirst = expected.Except(actual).ToList();
            if (!firstNotSecond.Any() && !secondNotFirst.Any())
            {
                correct = true;
            }
            // ^^^^ compare the lists before and after deep copy above

            Assert.IsTrue(correct, "GetProductList() returns a wrong list");
        }

        //Test9: terminal.GetProductList - check product codes
        [TestMethod]
        public void Test9_GetProductList_SetProductListSetList_GetProductListWithProductAandB()
        {
            // Arrange
            var terminal = new PointOfSaleTerminal();
            List<IProduct> productListActual = new List<IProduct>
            {
                new Product("A"),
                new Product("B")
            };
            terminal.SetProductList(productListActual);
            List<IProduct> myProductList = terminal.GetProductList();
            string[] myProductCodeArray = new string[2];

            // Act
            int i = 0;
            foreach (IProduct product in myProductList)
            {
                myProductCodeArray[i++] = product.ProductCode;
            }
            string actual0 = myProductCodeArray[0];
            string actual1 = myProductCodeArray[1];

            IProduct myProductWithCodeA = myProductList.Find(x => x.ProductCode == "A");
            string actual2 = myProductWithCodeA.ProductCode;
            IProduct myProductWithCodeB = myProductList.Find(x => x.ProductCode == "B");
            string actual3 = myProductWithCodeB.ProductCode;

            // Assert
            string expected0 = "A";
            string expected1 = "B";
            string expected2 = "A";
            string expected3 = "B";

            Assert.AreEqual(expected0, actual0, "GetProductList() returns a wrong list");
            Assert.AreEqual(expected1, actual1, "GetProductList() returns a wrong list");
            Assert.AreEqual(expected2, actual2, "GetProductList() returns a wrong list");
            Assert.AreEqual(expected3, actual3, "GetProductList() returns a wrong list");
        }


        //Test10: terminal.SetPricing - check return value
        [TestMethod]
        public void Test10_SetPricing_SetPricingOfGivenProducts_SetPricingReturn1()
        {
            // Arrange
            var terminal = new PointOfSaleTerminal();
            List<IProduct> productListActual = new List<IProduct>
            {
                new Product("A"),
                new Product("B")
            };
            terminal.SetProductList(productListActual);

            // Act - for product with code A
            int actualReturnValueForCodeA = terminal.SetPricing("A",1.25f,3,3.0f);

            // Act - for product with code B
            int actualReturnValueForCodeB = terminal.SetPricing("B", 4.25f);

            // Assert - for product with code A
            int expectedReturnValueForCodeA = 1;
            Assert.AreEqual(expectedReturnValueForCodeA, actualReturnValueForCodeA, "terminal.SetPricing('A',1.25f,3,3.0f) should return 1");

            // Assert - for product with code B
            int expectedReturnValueForCodeB = 1;
            Assert.AreEqual(expectedReturnValueForCodeB, actualReturnValueForCodeB, "terminal.SetPricing('B', 4.25f) should return 1");
        }

        //Test11: terminal.SetPricing - check prices
        [TestMethod]
        public void Test11_SetPricing_SetPricingOfGivenProducts_GetProductListWithRightPrices()
        {
            // Arrange
            var terminal = new PointOfSaleTerminal();
            List<IProduct> productListActual = new List<IProduct>
            {
                new Product("A"),
                new Product("B")
            };
            terminal.SetProductList(productListActual);

            // Act - for product with code A
            int testingValueForCodeA = terminal.SetPricing("A", 1.25f, 3, 3.0f);
            List<IProduct> myProductListForCodeA = terminal.GetProductList();
            IProduct myProductWithCodeA = myProductListForCodeA.Find(x => x.ProductCode == "A");
            float actualUnitPriceOfCodeA = myProductWithCodeA.ProductUnitPrice;
            int actualBulkQtyOfCodeA = myProductWithCodeA.ProductBulkQty;
            float actualBulkPriceOfCodeA = myProductWithCodeA.ProductBulkPrice;

            // Act - for product with code B
            int testingValueForCodeB = terminal.SetPricing("B", 4.25f);
            List<IProduct> myProductListForCodeB = terminal.GetProductList();
            IProduct myProductWithCodeB = myProductListForCodeB.Find(x => x.ProductCode == "B");
            float actualUnitPriceOfCodeB = myProductWithCodeB.ProductUnitPrice;
            int actualBulkQtyOfCodeB = myProductWithCodeB.ProductBulkQty;
            float actualBulkPriceOfCodeB = myProductWithCodeB.ProductBulkPrice;

            // Assert - for product with code A
            float expectedUnitPriceOfCodeA = 1.25f;
            int expectedBulkQtyOfCodeA = 3;
            float expectedBulkPriceOfCodeA = 3.0f;

            Assert.AreEqual(expectedUnitPriceOfCodeA, actualUnitPriceOfCodeA, "terminal.SetPricing('A',1.25f,3,3.0f) did not correctly set unit price for product with code A");
            Assert.AreEqual(expectedBulkQtyOfCodeA, actualBulkQtyOfCodeA, "terminal.SetPricing('A',1.25f,3,3.0f) did not correctly set bulk quantity for product with code A");
            Assert.AreEqual(expectedBulkPriceOfCodeA, actualBulkPriceOfCodeA, "terminal.SetPricing('A',1.25f,3,3.0f) did not correctly set bulk price for product with code A");

            // Assert - for product with code B
            float expectedUnitPriceOfCodeB = 4.25f;
            int expectedBulkQtyOfCodeB = 1;
            float expectedBulkPriceOfCodeB = 4.25f;

            Assert.AreEqual(expectedUnitPriceOfCodeB, actualUnitPriceOfCodeB, "terminal.SetPricing('B', 4.25f) did not correctly set unit price for product with code B");
            Assert.AreEqual(expectedBulkQtyOfCodeB, actualBulkQtyOfCodeB, "terminal.SetPricing('B', 4.25f) did not correctly set bulk quantity for product with code B");
            Assert.AreEqual(expectedBulkPriceOfCodeB, actualBulkPriceOfCodeB, "terminal.SetPricing('B', 4.25f) did not correctly set bulk price for product with code B");
        }

        //Test12: terminal.ScanProduct(argument)
        [TestMethod]
        public void Test12_ScanProduct_ScanProductProductWithCodeA_B_C_ScanProductReturn1_1_0()
        {       
            // Arrange
            var terminal = new PointOfSaleTerminal();
            List<IProduct> productListActual = new List<IProduct>
            {
                new Product("A"),
                new Product("B")
                
            };
            terminal.SetProductList(productListActual);
            int testingValueForCodeA = terminal.SetPricing("A", 1.25f, 3, 3.0f);
            int testingValueForCodeB = terminal.SetPricing("B", 4.25f);

            // Act
            int actualProductAReturn = terminal.ScanProduct("A");
            int actualProductBReturn = terminal.ScanProduct("B");
            int actualProductCReturn = terminal.ScanProduct("C");// there is no C scanned

            // Assert
            int expectedProductAReturn = 1;
            int expectedProductBReturn = 1;
            int expectedProductCReturn = 0;// This is zero because there was no C scanned.

            Assert.AreEqual(expectedProductAReturn, actualProductAReturn, "Return value from ScanProduct('A') is wrong");
            Assert.AreEqual(expectedProductBReturn, actualProductBReturn, "Return value from ScanProduct('B') is wrong");
            Assert.AreEqual(expectedProductCReturn, actualProductCReturn, "Return value from ScanProduct('C') is wrong");
        }

        //Test13: terminal.ScanProduct(argument) and see if the private and protected parameters are correct or not.
        [TestMethod]
        public void Test13_ScanProduct_ScanProductProduct_ABCDABA_CheckParamatersAboutScannedProduct()
        {
            // Arrange
            var terminal = new TestHelperTermainal();   // using TestHelperTermainal class inherited from PointOfSaleTerminal class
                                                        // for testing the protected properties
                                                        // such as _productCodeList and _productCodeList

            List<IProduct> productListActual = new List<IProduct>
            {
                new Product("A"),
                new Product("B"),
                new Product("C"),
                new Product("D")

            };
            terminal.SetProductList(productListActual);
            int ReturnValueForCodeA_priceSet = terminal.SetPricing("A", 1.25f, 3, 3.0f);
            int ReturnValueForCodeB_priceSet = terminal.SetPricing("B", 4.25f);
            int ReturnValueForCodeC_priceSet = terminal.SetPricing("C", 1.0f, 6, 5.0f);
            int ReturnValueForCodeD_priceSet = terminal.SetPricing("D", 0.75f);

            // Act
            terminal.ScanProduct("A");
            terminal.ScanProduct("B");
            terminal.ScanProduct("C");
            terminal.ScanProduct("D");
            terminal.ScanProduct("A");
            terminal.ScanProduct("B");
            terminal.ScanProduct("A");
            IDictionary<string, int> actualScannedProductCodeDict = terminal.GetScannedProductCodes();
            int actualCount = actualScannedProductCodeDict.Count();
            string actualKey1 = actualScannedProductCodeDict.Keys.ElementAt(actualCount - actualCount);
            string actualKey2 = actualScannedProductCodeDict.Keys.ElementAt(actualCount - 3);
            string actualKey3 = actualScannedProductCodeDict.Keys.ElementAt(actualCount - 2);
            string actualKey4 = actualScannedProductCodeDict.Keys.ElementAt(actualCount - 1);
            int actualValue1 = actualScannedProductCodeDict[actualKey1];
            int actualValue2 = actualScannedProductCodeDict[actualKey2];
            int actualValue3 = actualScannedProductCodeDict[actualKey3];
            int actualValue4 = actualScannedProductCodeDict[actualKey4];
            List<string> actualProductCodeList = terminal.GetProductCodeList();
            List<IProduct> actualProductList = terminal.GetProductList();
            IProduct actualProductA = actualProductList.Find(productX => productX.ProductCode == actualKey1); //actualKey1 is "A"
            IProduct actualProductB = actualProductList.Find(productX => productX.ProductCode == actualKey2); //actualKey1 is "B"
            IProduct actualProductC = actualProductList.Find(productX => productX.ProductCode == actualKey3); //actualKey1 is "C"
            IProduct actualProductD = actualProductList.Find(productX => productX.ProductCode == actualKey4); //actualKey1 is "D"
            float actualUnitPriceOfA = actualProductA.ProductUnitPrice;
            float actualUnitPriceOfB = actualProductB.ProductUnitPrice;
            float actualUnitPriceOfC = actualProductC.ProductUnitPrice;
            float actualUnitPriceOfD = actualProductD.ProductUnitPrice;
            int actualBulkQtyOfA = actualProductA.ProductBulkQty;
            int actualBulkQtyOfB = actualProductB.ProductBulkQty;
            int actualBulkQtyOfC = actualProductC.ProductBulkQty;
            int actualBulkQtyOfD = actualProductD.ProductBulkQty;
            float actualBulkPriceOfA = actualProductA.ProductBulkPrice;
            float actualBulkPriceOfB = actualProductB.ProductBulkPrice;
            float actualBulkPriceOfC = actualProductC.ProductBulkPrice;
            float actualBulkPriceOfD = actualProductD.ProductBulkPrice;

            // Assert
            IDictionary<string, int> expectedScannedProductCodeDict = new Dictionary<string, int>()
                                            {
                                                {"A",3},
                                                {"B",2},
                                                {"C",1},
                                                {"D",1}
                                            };
            int expectedCount = expectedScannedProductCodeDict.Count();
            string expectedKey1 = expectedScannedProductCodeDict.Keys.ElementAt(expectedCount - expectedCount);
            string expectedKey2 = expectedScannedProductCodeDict.Keys.ElementAt(expectedCount - 3);
            string expectedKey3 = expectedScannedProductCodeDict.Keys.ElementAt(expectedCount - 2);
            string expectedKey4 = expectedScannedProductCodeDict.Keys.ElementAt(expectedCount - 1);
            int expectedValue1 = expectedScannedProductCodeDict[expectedKey1];
            int expectedValue2 = expectedScannedProductCodeDict[expectedKey2];
            int expectedValue3 = expectedScannedProductCodeDict[expectedKey3];
            int expectedValue4 = expectedScannedProductCodeDict[expectedKey4];
            List<string> expectedProductCodeList = new List<string>() { "A", "B", "C", "D" };
            float expectedUnitPriceOfA = 1.25f;
            float expectedUnitPriceOfB = 4.25f;
            float expectedUnitPriceOfC = 1.0f;
            float expectedUnitPriceOfD = 0.75f;
            int expectedBulkQtyOfA = 3;
            int expectedBulkQtyOfB = 1; //default value
            int expectedBulkQtyOfC = 6;
            int expectedBulkQtyOfD = 1; //default value
            float expectedBulkPriceOfA = 3.0f;
            float expectedBulkPriceOfB = 4.25f; //default as unit price
            float expectedBulkPriceOfC = 5.0f;
            float expectedBulkPriceOfD = 0.75f; //default as unit price

            Assert.AreEqual(expectedCount, actualCount, "Wrong dictionary count");
            Assert.AreEqual(expectedKey1, actualKey1, "Wrong Key1");
            Assert.AreEqual(expectedKey2, actualKey2, "Wrong Key2");
            Assert.AreEqual(expectedKey3, actualKey3, "Wrong Key3");
            Assert.AreEqual(expectedKey4, actualKey4, "Wrong Key4");
            Assert.AreEqual(expectedValue1, actualValue1, "Wrong Value1");
            Assert.AreEqual(expectedValue2, actualValue2, "Wrong Value2");
            Assert.AreEqual(expectedValue3, actualValue3, "Wrong Value3");
            Assert.AreEqual(expectedValue4, actualValue4, "Wrong Value4");
            // ---- compare the actual and expected product code lists
            var firstNotSecond = actualProductCodeList.Except(expectedProductCodeList).ToList();
            var secondNotFirst = expectedProductCodeList.Except(actualProductCodeList).ToList();
            bool correct = false;
            if (!firstNotSecond.Any() && !secondNotFirst.Any())
            {
                correct = true;
            }
            // ^^^^ compare the lists before and after deep copy above
            Assert.IsTrue(correct, "Wrong ProductCodeList");
            Assert.AreEqual(expectedUnitPriceOfA, actualUnitPriceOfA, "Wrong UnitPriceOfA");
            Assert.AreEqual(expectedUnitPriceOfB, actualUnitPriceOfB, "Wrong UnitPriceOfB");
            Assert.AreEqual(expectedUnitPriceOfC, actualUnitPriceOfC, "Wrong UnitPriceOfC");
            Assert.AreEqual(expectedUnitPriceOfD, actualUnitPriceOfD, "Wrong UnitPriceOfD");

            Assert.AreEqual(expectedBulkQtyOfA, actualBulkQtyOfA, "Wrong BulkQtyOfA");
            Assert.AreEqual(expectedBulkQtyOfB, actualBulkQtyOfB, "Wrong BulkQtyOfB");
            Assert.AreEqual(expectedBulkQtyOfC, actualBulkQtyOfC, "Wrong BulkQtyOfC");
            Assert.AreEqual(expectedBulkQtyOfD, actualBulkQtyOfD, "Wrong BulkQtyOfD");

            Assert.AreEqual(expectedBulkPriceOfA, actualBulkPriceOfA, "Wrong BulkPriceOfA");
            Assert.AreEqual(expectedBulkPriceOfB, actualBulkPriceOfB, "Wrong BulkPriceOfB");
            Assert.AreEqual(expectedBulkPriceOfC, actualBulkPriceOfC, "Wrong BulkPriceOfC");
            Assert.AreEqual(expectedBulkPriceOfD, actualBulkPriceOfD, "Wrong BulkPriceOfD");
        }

        //Test14: terminal.CalculateTotal() - ScannedProductABCDABA
        [TestMethod]
        public void Test14_CalculateTotal_ScannedProductABCDABA_CalculateTotalEqual13_25()
        {
            // Arrange
            var terminal = new PointOfSaleTerminal();
            List<IProduct> productListActual = new List<IProduct>
            {
                new Product("A"),
                new Product("B"),
                new Product("C"),
                new Product("D")

            };
            terminal.SetProductList(productListActual);
            int ReturnValueForCodeA_priceSet = terminal.SetPricing("A", 1.25f, 3, 3.0f);
            int ReturnValueForCodeB_priceSet = terminal.SetPricing("B", 4.25f);
            int ReturnValueForCodeC_priceSet = terminal.SetPricing("C", 1.0f, 6, 5.0f);
            int ReturnValueForCodeD_priceSet = terminal.SetPricing("D", 0.75f);

            terminal.ScanProduct("A");
            terminal.ScanProduct("B");
            terminal.ScanProduct("C");
            terminal.ScanProduct("D");
            terminal.ScanProduct("A");
            terminal.ScanProduct("B");
            terminal.ScanProduct("A");

            // Act
            float actualTotal = terminal.CalculateTotal();

            // Assert
            float expectedTotal = 13.25f; 
            Assert.AreEqual(expectedTotal, actualTotal, "The calculated total is wrong and should be 13.25");
        }

        //Test15: terminal.CalculateTotal() - ScannedProductCCCCCCC
        [TestMethod]
        public void Test15_CalculateTotal_ScannedProductCCCCCCC_CalculateTotalEqual6()
        {
            // Arrange
            var terminal = new PointOfSaleTerminal();
            List<IProduct> productListActual = new List<IProduct>
            {
                new Product("A"),
                new Product("B"),
                new Product("C"),
                new Product("D")

            };
            terminal.SetProductList(productListActual);
            int ReturnValueForCodeA_priceSet = terminal.SetPricing("A", 1.25f, 3, 3.0f);
            int ReturnValueForCodeB_priceSet = terminal.SetPricing("B", 4.25f);
            int ReturnValueForCodeC_priceSet = terminal.SetPricing("C", 1.0f, 6, 5.0f);
            int ReturnValueForCodeD_priceSet = terminal.SetPricing("D", 0.75f);

            terminal.ScanProduct("C");
            terminal.ScanProduct("C");
            terminal.ScanProduct("C");
            terminal.ScanProduct("C");
            terminal.ScanProduct("C");
            terminal.ScanProduct("C");
            terminal.ScanProduct("C");

            // Act
            float actualTotal = terminal.CalculateTotal();

            // Assert
            float expectedTotal = 6.0f;
            Assert.AreEqual(expectedTotal, actualTotal, "The calculated total is wrong and should be 6");
        }

        //Test16: terminal.CalculateTotal() - ScannedProductABCD
        [TestMethod]
        public void Test16_CalculateTotal_ScannedProductABCD_CalculateTotalEqual7_25()
        {
            // Arrange
            var terminal = new PointOfSaleTerminal();
            List<IProduct> productListActual = new List<IProduct>
            {
                new Product("A"),
                new Product("B"),
                new Product("C"),
                new Product("D")

            };
            terminal.SetProductList(productListActual);
            int ReturnValueForCodeA_priceSet = terminal.SetPricing("A", 1.25f, 3, 3.0f);
            int ReturnValueForCodeB_priceSet = terminal.SetPricing("B", 4.25f);
            int ReturnValueForCodeC_priceSet = terminal.SetPricing("C", 1.0f, 6, 5.0f);
            int ReturnValueForCodeD_priceSet = terminal.SetPricing("D", 0.75f);

            terminal.ScanProduct("A");
            terminal.ScanProduct("B");
            terminal.ScanProduct("C");
            terminal.ScanProduct("D");

            // Act
            float actualTotal = terminal.CalculateTotal();
            
            // Assert
            float expectedTotal = 7.25f;
            Assert.AreEqual(expectedTotal, actualTotal, "The calculated total is wrong and should be 7.25");
        }
    }
}
