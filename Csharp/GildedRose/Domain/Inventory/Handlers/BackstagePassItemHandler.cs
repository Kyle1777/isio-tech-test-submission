using GildedRoseKata.Domain.Inventory.Handlers.Interfaces;
using GildedRoseKata.Domain.Inventory.Models;
using GildedRoseKata.Domain.Inventory.Services;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GildedRoseKata.Domain.Inventory.Handlers
{
    public class BackstagePassItemHandler : IItemHandler
    {
        public Type SupportedType { get; set; } = typeof(BackstagePassItem);

        private readonly ItemStateService _itemStateService;
        private readonly ItemSettings _itemSettings;

        public BackstagePassItemHandler(ItemStateService itemStateService, IOptions<ItemSettings> itemSettings)
        {
            _itemStateService = itemStateService;
            _itemSettings = itemSettings.Value;
        }

        // KRB: I try to avoid nesting where possible to make it easier to read. This also means we're not running more conditions than necessary.
        public void UpdateItemProperties(Item item)
        {
            _itemStateService.ConstrainQualityBetweenMinMax(item);

            if (_itemStateService.HasSellByPassed(item))
            {
                item.Quality = 0;

                _itemStateService.DecreaseSellIn(item);

                return;
            }

            _itemStateService.IncreaseQuality(item, CalculateQualityIncrement(item));

            _itemStateService.DecreaseSellIn(item);
        }

        private int CalculateQualityIncrement(Item item)
        {
            if (item.SellIn <= 2)
            {
                return 4;
            }
            
            if (item.SellIn <= 7)
            {
                return 3;
            }

            return _itemSettings.DefaultQualityIncrement;
        }
    }
}
