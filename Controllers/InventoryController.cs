using InventoryApi.Models;
using InventoryApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace InventoryApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class InventoriesController : ControllerBase
{
    private readonly InventoriesService _inventoriesService;

    public InventoriesController(InventoriesService inventoriesService) =>
        _inventoriesService = inventoriesService;

    [HttpGet]
    public async Task<List<Inventory>> Get() =>
        await _inventoriesService.GetAsync();

    [HttpGet("{id:length(24)}")]
    public async Task<ActionResult<Inventory>> Get(string id)
    {
        var inventory = await _inventoriesService.GetAsync(id);

        if (inventory is null)
        {
            return NotFound();
        }

        return inventory;
    }

    [HttpPost]
    public async Task<IActionResult> Post(Inventory newInventory)
    {
        await _inventoriesService.CreateAsync(newInventory);

        return CreatedAtAction(nameof(Get), new { id = newInventory.Id }, newInventory);
    }

    [HttpPut("{id:length(24)}")]
    public async Task<IActionResult> Update(string id, Inventory updatedInventory)
    {
        var inventory = await _inventoriesService.GetAsync(id);

        if (inventory is null)
        {
            return NotFound();
        }

        updatedInventory.Id = inventory.Id;

        await _inventoriesService.UpdateAsync(id, updatedInventory);

        return NoContent();
    }

    [HttpDelete("{id:length(24)}")]
    public async Task<IActionResult> Delete(string id)
    {
        var inventory = await _inventoriesService.GetAsync(id);

        if (inventory is null)
        {
            return NotFound();
        }

        await _inventoriesService.RemoveAsync(id);

        return NoContent();
    }
}