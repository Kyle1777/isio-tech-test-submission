using GildedRoseKata.Domain.Inventory.Handlers;
using GildedRoseKata.Domain.Inventory.Models;
using GildedRoseKata.Domain.Inventory.Services;
using Microsoft.Extensions.Options;
using Xunit;

namespace GildedRoseTests.Domain.Inventory.Handlers
{
    public class MaturableItemHandlerTests
    {
        [Fact]
        public void UpdateItemProperties_DecreaseSellInBy1()
        {
            Item item = new StandardItem
            {
                Name = "Maturable Item",
                SellIn = 10,
                Quality = 20
            };

            IOptions<ItemSettings> itemSettings = Options.Create(new ItemSettings
            {
                DefaultQualityIncrement = 1,
                MaxQuality = 40
            });

            ItemStateService itemStateService = new ItemStateService(itemSettings);

            MaturableItemHandler handler = new MaturableItemHandler(itemStateService);

            handler.UpdateItemProperties(item);

            Assert.Equal(9, item.SellIn);
        }

        [Fact]
        public void UpdateItemProperties_IncreasesQualityBy1()
        {
            Item item = new StandardItem
            {
                Name = "Maturable Item",
                SellIn = 5,
                Quality = 20
            };

            IOptions<ItemSettings> itemSettings = Options.Create(new ItemSettings
            {
                DefaultQualityIncrement = 1,
                MaxQuality = 40
            });

            ItemStateService itemStateService = new ItemStateService(itemSettings);

            MaturableItemHandler handler = new MaturableItemHandler(itemStateService);

            handler.UpdateItemProperties(item);

            Assert.Equal(21, item.Quality);
        }

        [Fact]
        public void UpdateItemProperties_DoesNotIncreaseQualityBeyondMaxValue()
        {
            Item item = new StandardItem
            {
                Name = "Maturable Item",
                SellIn = 5,
                Quality = 40
            };

            IOptions<ItemSettings> itemSettings = Options.Create(new ItemSettings
            {
                DefaultQualityIncrement = 1,
                MaxQuality = 40
            });

            ItemStateService itemStateService = new ItemStateService(itemSettings);

            MaturableItemHandler handler = new MaturableItemHandler(itemStateService);

            handler.UpdateItemProperties(item);

            Assert.Equal(40, item.Quality);
        }
    }
}
