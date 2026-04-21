using GildedRoseKata.Domain.Inventory.Handlers;
using GildedRoseKata.Domain.Inventory.Models;
using GildedRoseKata.Domain.Inventory.Services;
using Microsoft.Extensions.Options;
using Xunit;

namespace GildedRoseTests.Domain.Inventory.Handlers
{
    public class BackstagePassItemHandlerTests
    {
        [Fact]
        public void UpdateItemProperties_DecreaseSellInBy1()
        {
            Item item = new BackstagePassItem
            {
                Name = "BackstagePass Item",
                SellIn = 10,
                Quality = 20
            };

            IOptions<ItemSettings> itemSettings = Options.Create(new ItemSettings
            {
                DefaultQualityIncrement = 1,
                MaxQuality = 40
            });

            ItemStateService itemStateService = new ItemStateService(itemSettings);

            BackstagePassItemHandler handler = new BackstagePassItemHandler(itemStateService, itemSettings);

            handler.UpdateItemProperties(item);

            Assert.Equal(9, item.SellIn);
        }

        [Fact]
        public void UpdateItemProperties_IncreaseQualityBy1WhenSellInIsMoreThan7()
        {
            Item item = new BackstagePassItem
            {
                Name = "BackstagePass Item",
                SellIn = 10,
                Quality = 20
            };

            IOptions<ItemSettings> itemSettings = Options.Create(new ItemSettings
            {
                DefaultQualityIncrement = 1,
                MaxQuality = 40
            });

            ItemStateService itemStateService = new ItemStateService(itemSettings);

            BackstagePassItemHandler handler = new BackstagePassItemHandler(itemStateService, itemSettings);

            handler.UpdateItemProperties(item);

            Assert.Equal(21, item.Quality);
        }

        [Fact]
        public void UpdateItemProperties_IncreaseQualityBy3WhenSellInIs7OrLess()
        {
            Item item = new BackstagePassItem
            {
                Name = "BackstagePass Item",
                SellIn = 7,
                Quality = 20
            };

            IOptions<ItemSettings> itemSettings = Options.Create(new ItemSettings
            {
                DefaultQualityIncrement = 1,
                MaxQuality = 40
            });

            ItemStateService itemStateService = new ItemStateService(itemSettings);

            BackstagePassItemHandler handler = new BackstagePassItemHandler(itemStateService, itemSettings);

            handler.UpdateItemProperties(item);

            Assert.Equal(23, item.Quality);
        }

        [Fact]
        public void UpdateItemProperties_IncreaseQualityBy4WhenSellInIs2OrLess()
        {
            Item item = new BackstagePassItem
            {
                Name = "BackstagePass Item",
                SellIn = 2,
                Quality = 20
            };

            IOptions<ItemSettings> itemSettings = Options.Create(new ItemSettings
            {
                DefaultQualityIncrement = 1,
                MaxQuality = 40
            });

            ItemStateService itemStateService = new ItemStateService(itemSettings);

            BackstagePassItemHandler handler = new BackstagePassItemHandler(itemStateService, itemSettings);

            handler.UpdateItemProperties(item);

            Assert.Equal(24, item.Quality);
        }

        [Fact]
        public void UpdateItemProperties_QualityIs0AfterSellInDate()
        {
            Item item = new BackstagePassItem
            {
                Name = "BackstagePass Item",
                SellIn = -1,
                Quality = 40
            };

            IOptions<ItemSettings> itemSettings = Options.Create(new ItemSettings
            {
                DefaultQualityIncrement = 1,
                MaxQuality = 40
            });

            ItemStateService itemStateService = new ItemStateService(itemSettings);

            BackstagePassItemHandler handler = new BackstagePassItemHandler(itemStateService, itemSettings);

            handler.UpdateItemProperties(item);
            handler.UpdateItemProperties(item);

            Assert.Equal(0, item.Quality);
        }

        [Fact]
        public void UpdateItemProperties_QualityNeverGoesBelow0()
        {
            Item item = new BackstagePassItem
            {
                Name = "BackstagePass Item",
                SellIn = 0,
                Quality = 1
            };

            IOptions<ItemSettings> itemSettings = Options.Create(new ItemSettings
            {
                DefaultQualityIncrement = 1,
                MaxQuality = 40
            });

            ItemStateService itemStateService = new ItemStateService(itemSettings);

            BackstagePassItemHandler handler = new BackstagePassItemHandler(itemStateService, itemSettings);

            handler.UpdateItemProperties(item);
            handler.UpdateItemProperties(item);

            Assert.Equal(0, item.Quality);
        }

        [Fact]
        public void UpdateItemProperties_EnsureQualityIsWithinMax()
        {
            Item item = new BackstagePassItem
            {
                Name = "BackstagePass Item",
                SellIn = 34,
                Quality = 49
            };

            IOptions<ItemSettings> itemSettings = Options.Create(new ItemSettings
            {
                DefaultQualityIncrement = 1,
                MaxQuality = 40
            });

            ItemStateService itemStateService = new ItemStateService(itemSettings);

            BackstagePassItemHandler handler = new BackstagePassItemHandler(itemStateService, itemSettings);

            handler.UpdateItemProperties(item);

            Assert.Equal(40, item.Quality);
        }

        [Fact]
        public void UpdateItemProperties_EnsureQualityIsWithinMin()
        {
            Item item = new BackstagePassItem
            {
                Name = "BackstagePass Item",
                SellIn = 34,
                Quality = -30
            };

            IOptions<ItemSettings> itemSettings = Options.Create(new ItemSettings
            {
                DefaultQualityIncrement = 1,
                MaxQuality = 40
            });

            ItemStateService itemStateService = new ItemStateService(itemSettings);

            BackstagePassItemHandler handler = new BackstagePassItemHandler(itemStateService, itemSettings);

            handler.UpdateItemProperties(item);

            Assert.Equal(1, item.Quality); // KRB: Quality increases so it will be a more positive value, not exactly 0.
        }
    }
}
