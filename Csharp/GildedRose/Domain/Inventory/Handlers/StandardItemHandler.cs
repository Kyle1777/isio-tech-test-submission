using GildedRoseKata.Domain.Inventory.Handlers.Interfaces;
using GildedRoseKata.Domain.Inventory.Models;
using GildedRoseKata.Domain.Inventory.Services;
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

        public StandardItemHandler(ItemStateService itemStateService) 
        {
            _itemStateService = itemStateService;
        }

        public void UpdateItemProperties(Item item)
        {
            _itemStateService.DecreaseSellIn(item);

            int degradeAmount = _itemStateService.HasSellByPassed(item) ? 2 : 1;

            _itemStateService.DecreaseQuality(item, degradeAmount);
        }
    }
}
