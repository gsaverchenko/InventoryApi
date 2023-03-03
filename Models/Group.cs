namespace InventoryApi.Models;

public class Group : Mongo
{
    public ICollection<Item> Items { get; set; } = null!;
}