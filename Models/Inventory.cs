namespace InventoryApi.Models;

public class Inventory : Group
{
    public ICollection<Group> Groups { get; set; } = null!;
}