using System;
using System.Collections.Generic;
using System.Linq;
using GildedRoseKata.Domain.Inventory.Handlers.Interfaces;
using GildedRoseKata.Domain.Inventory.Models;

namespace GildedRoseKata.Domain.Inventory
{
    // KRB: Chose not to abstract this into an interface because I can't see a need to create multiple implementations of this class.
    public class ItemHandlerResolver
    {
        private readonly IEnumerable<IItemHandler> _handlers;

        public ItemHandlerResolver(IEnumerable<IItemHandler> handlers)
        {
            _handlers = handlers;
        }

        public IItemHandler Resolve(Item item)
        {
            Type itemType = item.GetType();

            IItemHandler itemHandler = _handlers.FirstOrDefault(handler => handler.SupportedType == itemType);

            if (itemHandler == null)
            {
                throw new System.Exception($"No handler found for item of type {itemType}");
            }

            return itemHandler;
        }
    }
}