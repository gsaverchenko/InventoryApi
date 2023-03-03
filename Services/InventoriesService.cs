using InventoryApi.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace InventoryApi.Services;

public class InventoriesService
{
    private readonly IMongoCollection<Inventory> _inventoriesCollection;

    public InventoriesService(
        IOptions<InventoryDatabaseSettings> inventoryDatabaseSettings)
    {
        var mongoClient = new MongoClient(
            inventoryDatabaseSettings.Value.ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(
            inventoryDatabaseSettings.Value.DatabaseName);

        _inventoriesCollection = mongoDatabase.GetCollection<Inventory>(
            inventoryDatabaseSettings.Value.InventoriesCollectionName);
    }

    public async Task<List<Inventory>> GetAsync() =>
        await _inventoriesCollection.Find(_ => true).ToListAsync();

    public async Task<Inventory?> GetAsync(string id) =>
        await _inventoriesCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

    public async Task CreateAsync(Inventory newInventory) =>
        await _inventoriesCollection.InsertOneAsync(newInventory);

    public async Task UpdateAsync(string id, Inventory updatedInventory) =>
        await _inventoriesCollection.ReplaceOneAsync(x => x.Id == id, updatedInventory);

    public async Task RemoveAsync(string id) =>
        await _inventoriesCollection.DeleteOneAsync(x => x.Id == id);
}