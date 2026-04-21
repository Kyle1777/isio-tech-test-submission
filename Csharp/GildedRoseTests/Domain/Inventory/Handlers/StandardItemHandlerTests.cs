using GildedRoseKata.Domain.Inventory.Handlers;
using GildedRoseKata.Domain.Inventory.Models;
using GildedRoseKata.Domain.Inventory.Services;
using Microsoft.Extensions.Options;
using Xunit;

namespace GildedRoseTests.Domain.Inventory.Handlers
{
    // KRB: I've broken this up into smaller units which test individual requirements for certain functions. This makes it easier to build the dependencies and also to identify what goes wrong if there's an issue.
    public class StandardItemHandlerTests
    {
        [Fact]
        public void UpdateItemProperties_DecreaseSellInBy1()
        {
            Item item = new StandardItem
            {
                Name = "Standard Item",
                SellIn = 10,
                Quality = 20
            };

            IOptions<ItemSettings> itemSettings = Options.Create(new ItemSettings
            {
                DefaultQualityIncrement = 1,
                MaxQuality = 40
            });

            ItemStateService itemStateService = new ItemStateService(itemSettings);

            StandardItemHandler handler = new StandardItemHandler(itemStateService, itemSettings);

            handler.UpdateItemProperties(item);

            Assert.Equal(9, item.SellIn);
        }

        [Fact]
        public void UpdateItemProperties_DecreaseQualityBy1BeforeSellByDate()
        {
            Item item = new StandardItem
            {
                Name = "Standard Item",
                SellIn = 10,
                Quality = 20
            };

            IOptions<ItemSettings> itemSettings = Options.Create(new ItemSettings
            {
                DefaultQualityIncrement = 1,
                MaxQuality = 40
            });

            ItemStateService itemStateService = new ItemStateService(itemSettings);

            StandardItemHandler handler = new StandardItemHandler(itemStateService, itemSettings);

            handler.UpdateItemProperties(item);

            Assert.Equal(19, item.Quality);
        }

        [Fact]
        public void UpdateItemProperties_DecreaseQualityBy2AfterSellByDate()
        {
            Item item = new StandardItem
            {
                Name = "Standard Item",
                SellIn = 0,
                Quality = 10
            };

            IOptions<ItemSettings> itemSettings = Options.Create(new ItemSettings
            {
                DefaultQualityIncrement = 1,
                MaxQuality = 40
            });

            ItemStateService itemStateService = new ItemStateService(itemSettings);

            StandardItemHandler handler = new StandardItemHandler(itemStateService, itemSettings);

            handler.UpdateItemProperties(item);

            Assert.Equal(8, item.Quality);
        }

        [Fact]
        public void UpdateItemProperties_QualityNeverGoesBelow0()
        {
            Item item = new StandardItem
            {
                Name = "Standard Item",
                SellIn = 0,
                Quality = 1
            };

            IOptions<ItemSettings> itemSettings = Options.Create(new ItemSettings
            {
                DefaultQualityIncrement = 1,
                MaxQuality = 40
            });

            ItemStateService itemStateService = new ItemStateService(itemSettings);

            StandardItemHandler handler = new StandardItemHandler(itemStateService, itemSettings);

            handler.UpdateItemProperties(item);
            handler.UpdateItemProperties(item);

            Assert.Equal(0, item.Quality);
        }
    }
}
