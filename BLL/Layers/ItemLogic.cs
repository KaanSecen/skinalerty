using BLL.Interfaces;
using BLL.Models;

namespace BLL.Layers;

public class ItemLogic(IItemService itemService)
{
    public ValidationResult<Item> SaveItem(Item item)
    {
        ValidationResult<Item> itemValidationResult = new ValidationResult<Item>
        {
            Result = [item],
        };

        itemValidationResult.Result = [itemService.SaveItem(item)];
        itemValidationResult.IsSuccess = true;
        itemValidationResult.Message = $"{item.Name} saved successfully!";

        return itemValidationResult;
    }

    public ValidationResult<List<Item>> SearchItems(string searchTerm)
    {
        ValidationResult<List<Item>> itemValidationResult = new ValidationResult<List<Item>>
        {
            Result = [itemService.SearchItems(searchTerm)],
        };

        if (!itemValidationResult.Result.Any())
        {
            itemValidationResult.IsSuccess = false;
            itemValidationResult.Message = "No items found!";
            return itemValidationResult;
        }

        itemValidationResult.IsSuccess = true;
        itemValidationResult.Message = "Items found successfully!";

        return itemValidationResult;
    }

    // public void UpdateItemPrice(Item item)
    // {
    //     ValidationResult<Item> itemValidationResult = new ValidationResult<Item>
    //     {
    //         Result = [item],
    //     };
    //
    //     itemValidationResult.Result = [itemService.UpdateItem(item)];
    //     itemValidationResult.IsSuccess = true;
    //     itemValidationResult.Message = "Item updated successfully!";
    // }
}