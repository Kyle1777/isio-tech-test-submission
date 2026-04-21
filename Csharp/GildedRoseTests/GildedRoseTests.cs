using Xunit;
using System.Collections.Generic;
using GildedRoseKata;

namespace GildedRoseTests;

public class GildedRoseTests
{
    // KRB: I've broken this up into smaller units which test individual requirements for certain functions. This makes it easier to build the dependencies and also to identify what goes wrong if there's an issue.
    //[Fact]
    //public void ExampleTest()
    //{
    //    IList<Item> Items = new List<Item> { new Item { Name = "foo", SellIn = 0, Quality = 0 } };
    //    GildedRose app = new GildedRose(Items);
    //    app.UpdateQuality();
    //    Assert.Equal("fixme", Items[0].Name);
    //}
}