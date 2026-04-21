using GildedRoseKata.Domain.Inventory;
using GildedRoseKata.Domain.Inventory.Handlers;
using GildedRoseKata.Domain.Inventory.Handlers.Interfaces;
using GildedRoseKata.Domain.Inventory.Models;
using GildedRoseKata.Domain.Inventory.Services;
using GildedRoseKata.Utilities;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;

namespace GildedRoseKata;

public class Program
{
    public static void Main(string[] args)
    {
        HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);

        builder.Services.Configure<ItemSettings>(builder.Configuration.GetSection("ItemSettings"));

        IList<Item> items = new List<Item>
        {
            new StandardItem {Name = "+5 Dexterity Vest", SellIn = 10, Quality = 20},
            new MaturableItem {Name = "Aged Brie", SellIn = 2, Quality = 0},
            new StandardItem {Name = "Elixir of the Mongoose", SellIn = 5, Quality = 7},
            new LegendaryItem {Name = "Sulfuras, Hand of Ragnaros", SellIn = -1, Quality = 80},
            new LegendaryItem {Name = "Sulfuras, Hand of Ragnaros", SellIn = -1, Quality = 80},
            new BackstagePassItem
            {
                Name = "Backstage passes to a TAFKAL80ETC concert",
                SellIn = 15,
                Quality = 20
            },
            new BackstagePassItem
            {
                Name = "Backstage passes to a TAFKAL80ETC concert",
                SellIn = 10,
                Quality = 49
            },
            new BackstagePassItem
            {
                Name = "Backstage passes to a TAFKAL80ETC concert",
                SellIn = 5,
                Quality = 49
            },
            new ConjuredItem {Name = "Conjured Mana Cake", SellIn = 3, Quality = 6}
        };

        builder.Services.AddSingleton(items);

        builder.Services.AddScoped<ItemHandlerResolver>();

        builder.Services.AddScoped<ItemStateService>();

        // KRB: Registering all of the handler implementations here so the resolver can be extended easily in future by adding new implementation classes.
        builder.Services.AddScoped<IItemHandler, LegendaryItemHandler>();
        builder.Services.AddScoped<IItemHandler, MaturableItemHandler>();
        builder.Services.AddScoped<IItemHandler, BackstagePassItemHandler>();
        builder.Services.AddScoped<IItemHandler, ConjuredItemHandler>();
        builder.Services.AddScoped<IItemHandler, StandardItemHandler>();

        builder.Services.AddTransient<GildedRose>();

        ServiceProvider serviceProvider = builder.Services.BuildServiceProvider();
        GildedRose app = serviceProvider.GetRequiredService<GildedRose>();

        int days = 2;
        if (args.Length > 0)
        {
            // KRB: Doing this because the original code didn't handle the potential that the arg wasn't a number. The utility class allows for other methods to be added in future.
            days = NormalisationUtility.NumberOrDefault(args[0], 1) + 1; // TODO: Potentially should display a message in case they did it accidentally(?)
        }

        app.Run(days);
    }
}