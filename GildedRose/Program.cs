using System;
using System.Collections.Generic;

namespace GildedRose
{
    public class Program
    {
        public IList<Item> Items { init; get; }

        static void Main(string[] args)
        {
            System.Console.WriteLine("OMGHAI!");

            var app = new Program()
            {
                Items = new List<Item>
                {
                    new Item { Name = "+5 Dexterity Vest", SellIn = 10, Quality = 20 },
                    new IncreasingItem { Name = "Aged Brie", SellIn = 2, Quality = 0 },
                    new Item { Name = "Elixir of the Mongoose", SellIn = 5, Quality = 7 },
                    new LegendaryItem { Name = "Sulfuras, Hand of Ragnaros", SellIn = 0, Quality = 80 },
                    new LegendaryItem { Name = "Sulfuras, Hand of Ragnaros", SellIn = -1, Quality = 80 },
                    new TimeDependantItem { Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = 15, Quality = 20 },
                    new TimeDependantItem { Name = "Backstage passes to a TAFKAL80ETC concert",SellIn = 10,Quality = 49},
                    new TimeDependantItem { Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = 5, Quality = 49 },
				    // this conjured item does not work properly yet
				    new ConjuredItem { Name = "Conjured Mana Cake", SellIn = 3, Quality = 6 }
                }
            };

            for (var i = 0; i < 31; i++)
            {
                Console.WriteLine("-------- day " + i + " --------");
                Console.WriteLine("name, sellIn, quality");
                for (var j = 0; j < app.Items.Count; j++)
                {
                    Console.WriteLine(app.Items[j].Name + ", " + app.Items[j].SellIn + ", " + app.Items[j].Quality);
                }
                Console.WriteLine("");
                app.UpdateQuality();
            }

        }

        public void UpdateQuality()
        {
            foreach (var item in Items)
            {
                item.Tick();
            }
        }

    }

    public class Item
    {
        public string Name { get; set; }

        public int SellIn { get; set; }

        public int Quality { get; set; }
        public void Tick()
        {
            _UpdateQuality();
            _UpdateSellIn();
        }

        protected virtual void _UpdateSellIn()
        {
            if (SellIn > 0)
            {
                SellIn--;
            }
        }

        protected virtual void _UpdateQuality()
        {
            if (SellIn == 0 && Quality > 1)
            {
                Quality -= 2;
            }
            else if (Quality > 0)
            {
                Quality--;
            }
        }
    }

    public class LegendaryItem : Item
    {
        protected override void _UpdateQuality() { }
    }

    public class IncreasingItem : Item
    {
        protected override void _UpdateQuality()
        {
            if (Quality < 50)
            {
                Quality++;
            }
        }
    }

    public class TimeDependantItem : Item
    {
        protected override void _UpdateQuality()
        {
            if (SellIn == 0)
            {
                Quality = 0;
            }
            else if (SellIn < 6)
            {
                Quality += 3;
            }
            else if (SellIn < 11)
            {
                Quality += 2;
            }
            else
            {
                Quality++;
            }
        }
    }

    public class ConjuredItem : Item
    {
        protected override void _UpdateQuality()
        {
            if (SellIn == 0 && Quality > 3)
            {
                Quality -= 4;
            }
            else if (Quality > 1)
            {
                Quality -= 2;
            }
            else if (Quality > 0)
            {
                Quality--;
            }
        }
    }
}