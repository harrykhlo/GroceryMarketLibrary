namespace GroceryMarketLibrary
{
    public interface IProduct
    {
        float ProductBulkPrice { get; set; }
        int ProductBulkQty { get; set; }
        string ProductCode { get; set; }
        string ProductName { get; set; }
        float ProductUnitPrice { get; set; }
    }
}