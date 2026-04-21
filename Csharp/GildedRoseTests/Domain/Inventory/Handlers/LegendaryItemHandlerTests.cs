using GildedRoseKata.Domain.Inventory.Handlers;
using GildedRoseKata.Domain.Inventory.Models;
using GildedRoseKata.Domain.Inventory.Services;
using Microsoft.Extensions.Options;
using Xunit;

namespace GildedRoseTests.Domain.Inventory.Handlers
{
    public class LegendaryItemHandlerTests
    {
        [Fact]
        public void UpdateItemProperties_EnsureSellInIsMinus1()
        {
            Item item = new StandardItem
            {
                Name = "Legendary Item",
                SellIn = 34,
                Quality = 20
            };

            IOptions<ItemSettings> itemSettings = Options.Create(new ItemSettings
            {
                DefaultQualityIncrement = 1,
                MaxQuality = 40
            });

            ItemStateService itemStateService = new ItemStateService(itemSettings);

            LegendaryItemHandler handler = new LegendaryItemHandler(itemStateService);

            handler.UpdateItemProperties(item);

            Assert.Equal(-1, item.SellIn);
        }

        [Fact]
        public void UpdateItemProperties_DoesNotChangeWithinRangeQuality()
        {
            Item item = new StandardItem
            {
                Name = "Legendary Item",
                SellIn = 34,
                Quality = 20
            };

            IOptions<ItemSettings> itemSettings = Options.Create(new ItemSettings
            {
                DefaultQualityIncrement = 1,
                MaxQuality = 40
            });

            ItemStateService itemStateService = new ItemStateService(itemSettings);

            LegendaryItemHandler handler = new LegendaryItemHandler(itemStateService);

            handler.UpdateItemProperties(item);

            Assert.Equal(20, item.Quality);
        }

        // KRB: Handle edge cases where the quality has values outside the range before starting the program.
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

            LegendaryItemHandler handler = new LegendaryItemHandler(itemStateService);

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

            LegendaryItemHandler handler = new LegendaryItemHandler(itemStateService);

            handler.UpdateItemProperties(item);

            Assert.Equal(0, item.Quality);
        }
    }
}
