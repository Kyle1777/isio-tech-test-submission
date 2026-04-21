using GildedRoseKata.Domain.Inventory.Models;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GildedRoseKata.Domain.Inventory.Services
{
    // KRB: I've created a helper class because multiple item types share the same logic such as capped values etc.
    // It doesn't need to be injected because it doesn't use any dependencies and is purely a helper class and doesn't need abstractions.

    // TODO: Actually I think it would be better to not hard code these values. Instead have them defined in a config file as there are other properties I should put in there too.
    // That would make this injectable rather than being static, because there would then be dependencies.

    // KRB: Have turned it into a service because it has business logic and also dependencies so this can be brought into the DI side of things.
    public class ItemStateService
    {
        private readonly ItemSettings _itemSettings;

        public ItemStateService(IOptions<ItemSettings> itemSettings)
        {
            _itemSettings = itemSettings.Value; // TODO: Need to validate this is there in the main program.
        }

        public void ConstrainQualityBetweenMinMax(Item item)
        {
            if (item.Quality < 0)
            {
                item.Quality = 0;
            }

            if (item.Quality > _itemSettings.MaxQuality)
            {
                item.Quality = _itemSettings.MaxQuality;
            }
        }

        public void DecreaseSellIn(Item item)
        {
            item.SellIn--;
        }

        public void DecreaseQuality(Item item, int amount = 1)
        {
            item.Quality -= amount;

            if (item.Quality < 0)
            {
                item.Quality = 0;
            }
        }

        public void IncreaseQuality(Item item, int amount = 1)
        {
            item.Quality += amount;

            if (item.Quality > _itemSettings.MaxQuality)
            {
                item.Quality = _itemSettings.MaxQuality;
            }
        }

        public bool HasSellByPassed(Item item)
        {
            return item.SellIn < 0;
        }
    }
}
