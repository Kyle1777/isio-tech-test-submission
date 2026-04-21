using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GildedRoseKata.Domain.Inventory.Models;

namespace GildedRoseKata.Domain.Inventory.Handlers.Interfaces
{
    public interface IItemHandler
    {
        Type SupportedType { get; set; }

        void UpdateItemProperties(Item item);
    }
}
