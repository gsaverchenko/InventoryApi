namespace InventoryApi.Models;

public class InventoryDatabaseSettings
{
    public string ConnectionString { get; set; } = null!;

    public string DatabaseName { get; set; } = null!;

    public string InventoriesCollectionName { get; set; } = null!;
}