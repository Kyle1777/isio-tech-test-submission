using GildedRoseKata.Domain.Inventory;
using GildedRoseKata.Domain.Inventory.Handlers.Interfaces;
using GildedRoseKata.Domain.Inventory.Models;
using GildedRoseKata.Utilities;
using System;
using System.Collections.Generic;

namespace GildedRoseKata;

public class GildedRose
{
    private readonly IList<Item> _items;
    private readonly ItemHandlerResolver _handlerResolver;

    public GildedRose(IList<Item> items, ItemHandlerResolver handlerResolver)
    {
        _items = items;
        _handlerResolver = handlerResolver;
    }

    public void Run(int days)
    {
        try
        {
            PrintWelcomeHeader();

            for (int dayIndex = 0; dayIndex < days; dayIndex++)
            {
                Console.WriteLine($"-------- day {dayIndex} --------");
                Console.WriteLine("name, sellIn, quality");

                // KRB: Refactored into a foreach to improve readability as it's referencing property values.
                // KRB: Bringing the update method call into here also prevents creating an additional item loop in the original update method.
                foreach (var item in _items)
                {
                    IItemHandler handler = _handlerResolver.Resolve(item);
                    handler.UpdateItemProperties(item);

                    PrintItemDetails(item);
                }
            }

            Console.WriteLine("Press any key to exit...");
            Console.ReadLine();
        }
        catch (Exception ex)
        {
            HandleError(ex);
        }
    }

    public void PrintWelcomeHeader() 
    {
        Console.WriteLine("OMGHAI!");
        Console.WriteLine("This program is designed to estimate the quality of goods day by day over a given period of days!");
    }

    // KRB: Created a dedicated method for this because I can see it being used often and also may want to change the way it's displayed.
    public void PrintItemDetails(Item item)
    {
        // KRB: Despite the memory efficiency trade off, I think it's worth it in this instance because of the readability and if someone wants to format other fields.
        string sellInDue = NormalisationUtility.NegativeToNever(item.SellIn);

        Console.WriteLine($"{item.Name}, {sellInDue}, {item.Quality}");
    }

    // KRB: Later it might be worth pulling this out into a dedicated service class but for now I think it's fine as we're only outputting to console.
    private static void HandleError(Exception ex)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine(ex.Message);
        Console.ResetColor();
    }

    // TODO: Remove this once it's broken up into the individual methods inside the handlers
    //public void UpdateQuality()
    //{
    //    for (var i = 0; i < Items.Count; i++)
    //    {
    //        if (Items[i].Name != "Aged Brie" && Items[i].Name != "Backstage passes to a TAFKAL80ETC concert")
    //        {
    //            if (Items[i].Quality > 0)
    //            {
    //                if (Items[i].Name != "Sulfuras, Hand of Ragnaros")
    //                {
    //                    Items[i].Quality = Items[i].Quality - 1;
    //                }
    //            }
    //        }
    //        else
    //        {
    //            if (Items[i].Quality < 50)
    //            {
    //                Items[i].Quality = Items[i].Quality + 1;

    //                if (Items[i].Name == "Backstage passes to a TAFKAL80ETC concert")
    //                {
    //                    if (Items[i].SellIn < 11)
    //                    {
    //                        if (Items[i].Quality < 50)
    //                        {
    //                            Items[i].Quality = Items[i].Quality + 1;
    //                        }
    //                    }

    //                    if (Items[i].SellIn < 6)
    //                    {
    //                        if (Items[i].Quality < 50)
    //                        {
    //                            Items[i].Quality = Items[i].Quality + 1;
    //                        }
    //                    }
    //                }
    //            }
    //        }

    //        if (Items[i].Name != "Sulfuras, Hand of Ragnaros")
    //        {
    //            Items[i].SellIn = Items[i].SellIn - 1;
    //        }

    //        if (Items[i].SellIn < 0)
    //        {
    //            if (Items[i].Name != "Aged Brie")
    //            {
    //                if (Items[i].Name != "Backstage passes to a TAFKAL80ETC concert")
    //                {
    //                    if (Items[i].Quality > 0)
    //                    {
    //                        if (Items[i].Name != "Sulfuras, Hand of Ragnaros")
    //                        {
    //                            Items[i].Quality = Items[i].Quality - 1;
    //                        }
    //                    }
    //                }
    //                else
    //                {
    //                    Items[i].Quality = Items[i].Quality - Items[i].Quality;
    //                }
    //            }
    //            else
    //            {
    //                if (Items[i].Quality < 50)
    //                {
    //                    Items[i].Quality = Items[i].Quality + 1;
    //                }
    //            }
    //        }
    //    }
    //}
}