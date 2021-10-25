using Xunit;
using GildedRose;
using System.Collections.Generic;

namespace GildedRose.Tests
{
    public class TestAssemblyTests
    {
        Program app;

        public TestAssemblyTests()
        {
            app = new Program()
            {
                Items = new List<Item>
                {
                    new Item { Name = "+5 Dexterity Vest", SellIn = 10, Quality = 20 },
                    new Item { Name = "Aged Brie", SellIn = 2, Quality = 0 },
                    new Item { Name = "Elixir of the Mongoose", SellIn = 5, Quality = 7 },
                    new Item { Name = "Sulfuras, Hand of Ragnaros", SellIn = 0, Quality = 80 },
                    new Item { Name = "Sulfuras, Hand of Ragnaros", SellIn = -1, Quality = 80 },
                    new Item { Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = 15, Quality = 20 },
                    new Item { Name = "Backstage passes to a TAFKAL80ETC concert",SellIn = 10,Quality = 49},
                    new Item { Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = 5, Quality = 49 },
				    // this conjured item does not work properly yet
				    new Item { Name = "Conjured Mana Cake", SellIn = 3, Quality = 6 }
                }
            };
        }

        [Fact]
        public void TestTheTruth()
        {
            Assert.True(true);
        }

        [Fact]
        public void CommonItemDegradeOneValuePerDay()
        {
            var item = new Item { Name = "Common Item", SellIn = 5, Quality = 5 };
            app.Items.Add(item);
            app.UpdateQuality();

            Assert.Equal(4, item.Quality);
        }

        [Fact]
        public void CommonItemDegradeOneSellInPerDay()
        {
            var item = new Item { Name = "Common Item", SellIn = 5, Quality = 5 };
            app.Items.Add(item);
            app.UpdateQuality();

            Assert.Equal(4, item.SellIn);
        }

        [Fact]
        public void test()
        {

        }




        //Tests to make:
        // Once the sell by date has passed, Quality degrades twice as fast
        // The Quality of an item is never negative
        // "Aged Brie" actually increases in Quality the older it gets
        // The Quality of an item is never more than 50
        // "Sulfuras", being a legendary item, never has to be sold or decreases in Quality
        // "Backstage passes", like aged brie, increases in Quality as it's SellIn value approaches; Quality increases by 2 when there are 10 days or less and by 3 when there are 5 days or less but Quality drops to 0 after the concert
    }
}