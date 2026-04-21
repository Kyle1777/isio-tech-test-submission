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
    public class MaturableItemHandler : IItemHandler
    {
        public Type SupportedType { get; set; } = typeof(MaturableItem);

        // KRB: Even though this is shared logic, I'm not going set up inheritance yet because of overcomplicating things for the scale of the app.
        private readonly ItemStateService _itemStateService;

        public MaturableItemHandler(ItemStateService itemStateService)
        {
            _itemStateService = itemStateService;
        }

        public void UpdateItemProperties(Item item)
        {
            // KRB: Only need to increase the quality as per the requirements.
            _itemStateService.DecreaseSellIn(item);

            _itemStateService.IncreaseQuality(item);
        }
    }
}
