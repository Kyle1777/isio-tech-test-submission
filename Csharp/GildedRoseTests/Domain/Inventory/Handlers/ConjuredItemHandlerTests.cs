using GildedRoseKata.Domain.Inventory.Handlers;
using GildedRoseKata.Domain.Inventory.Models;
using GildedRoseKata.Domain.Inventory.Services;
using Microsoft.Extensions.Options;
using Xunit;

namespace GildedRoseTests.Domain.Inventory.Handlers
{
    public class ConjuredItemHandlerTests
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

            ConjuredItemHandler handler = new ConjuredItemHandler(itemStateService, itemSettings);

            handler.UpdateItemProperties(item);

            Assert.Equal(9, item.SellIn);
        }

        [Fact]
        public void UpdateItemProperties_DecreaseQualityBy2BeforeSellByDate()
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

            ConjuredItemHandler handler = new ConjuredItemHandler(itemStateService, itemSettings);

            handler.UpdateItemProperties(item);

            Assert.Equal(18, item.Quality);
        }

        [Fact]
        public void UpdateItemProperties_DecreaseQualityBy4AfterSellByDate()
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

            ConjuredItemHandler handler = new ConjuredItemHandler(itemStateService, itemSettings);

            handler.UpdateItemProperties(item);

            Assert.Equal(6, item.Quality);
        }

        [Fact]
        public void UpdateItemProperties_EnsureQualityIsWithinMax()
        {
            Item item = new StandardItem
            {
                Name = "Legendary Item",
                SellIn = 34,
                Quality = 49
            };

            IOptions<ItemSettings> itemSettings = Options.Create(new ItemSettings
            {
                DefaultQualityIncrement = 1,
                MaxQuality = 40
            });

            ItemStateService itemStateService = new ItemStateService(itemSettings);

            ConjuredItemHandler handler = new ConjuredItemHandler(itemStateService, itemSettings);

            handler.UpdateItemProperties(item);

            Assert.Equal(40, item.Quality);
        }

        // KRB: Handle edge cases where the quality has values outside the range before starting the program.
        [Fact]
        public void UpdateItemProperties_EnsureQualityIsWithinMin()
        {
            Item item = new StandardItem
            {
                Name = "Legendary Item",
                SellIn = 34,
                Quality = -30
            };

            IOptions<ItemSettings> itemSettings = Options.Create(new ItemSettings
            {
                DefaultQualityIncrement = 1,
                MaxQuality = 40
            });

            ItemStateService itemStateService = new ItemStateService(itemSettings);

            ConjuredItemHandler handler = new ConjuredItemHandler(itemStateService, itemSettings);

            handler.UpdateItemProperties(item);

            Assert.Equal(0, item.Quality);
        }
    }
}
