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
    public class StandardItemHandler : IItemHandler
    {
        public Type SupportedType { get; set; } = typeof(StandardItem);

        private readonly ItemStateService _itemStateService;
        private readonly ItemSettings _itemSettings;

        public StandardItemHandler(ItemStateService itemStateService, IOptions<ItemSettings> itemSettings) 
        {
            _itemStateService = itemStateService;
            _itemSettings = itemSettings.Value;
        }

        public void UpdateItemProperties(Item item)
        {
            _itemStateService.ConstrainQualityBetweenMinMax(item);

            int degradeAmount = _itemSettings.DefaultQualityIncrement;

            degradeAmount = _itemStateService.HasSellByPassed(item) ? degradeAmount * 2 : degradeAmount;

            _itemStateService.DecreaseQuality(item, degradeAmount);

            _itemStateService.DecreaseSellIn(item);
        }
    }
}
