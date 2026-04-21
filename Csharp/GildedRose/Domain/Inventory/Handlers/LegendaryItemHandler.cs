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
    public class LegendaryItemHandler : IItemHandler
    {
        public Type SupportedType { get; set; } = typeof(LegendaryItem);

        private readonly ItemStateService _itemStateService;

        public LegendaryItemHandler(ItemStateService itemStateService)
        {
            _itemStateService = itemStateService;
        }

        public void UpdateItemProperties(Item item)
        {
            // KRB: Legendary items don't degrade and don't have a sell by date, so we don't need to do anything here.
            // KRB: For items where they're already entered with the wrong values, it would be better to handle it by making a correction script and ensuring it's not possible when adding them in future.
            // KRB: The reason being, because otherwise in this function it'll be running an update for every day when it's not necessary.
            // KRB: EG: Where the SellIn date is currently 0 instead of -1 for the legendary item.
            // KRB: That being said, as it's a small program I don't think it's too bad to just set them here.

            item.SellIn = -1;
            _itemStateService.IncreaseQuality(item, 0); // KRB: Runs the validation to ensure quality is within range.
        }
    }
}
