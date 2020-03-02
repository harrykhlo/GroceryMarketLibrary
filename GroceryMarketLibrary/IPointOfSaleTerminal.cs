using System.Collections.Generic;

namespace GroceryMarketLibrary
{
    public interface IPointOfSaleTerminal
    {
        string TerminalCode { get; set; }

        float CalculateTotal();
        List<IProduct> GetProductList();
        int ScanProduct(string productCode);
        int SetPricing(string productCode, float productUnitPrice, int productBulkQty = 1, float productBulkPrice = 0);
        int SetProductList(List<IProduct> productList);
    }
}