using Xunit;
using System.Linq;
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
                    new Item { Name = "Backstage passes to a TAFKAL80ETC concert",SellIn = 10,Quality = 9},
                    new Item { Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = 5, Quality = 9 },
				    // this conjured item does not work properly yet
				    new Item { Name = "Conjured Mana Cake", SellIn = 3, Quality = 6 }
                }
            };
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
        public void QualityOfItemCanNotBeOver50(){    
            var item = new Item { Name = "Aged Brie", SellIn = 5, Quality = 50 };
            app.Items.Add(item);
        
            app.UpdateQuality();

            Assert.Equal(50, item.Quality);
        }

        [Fact]
        public void QualityOfBackstagePassesIncreasesInQualityBy2WhenThereAre10DaysLeft(){
           var backstagePass = app.Items.Where(item => item.Name.Contains("Backstage passes") && item.SellIn == 10).FirstOrDefault();

           app.UpdateQuality();
           Assert.Equal(11, backstagePass.Quality);


        }

        [Fact]
        public void QualityOfBackstagePassesIncreasesInQualityBy2WhenThereAre5DaysLeft(){
           var backstagePass = app.Items.Where(item => item.Name.Contains("Backstage passes") && item.SellIn == 5).FirstOrDefault();

           app.UpdateQuality();
           Assert.Equal(12, backstagePass.Quality);


        }

        [Fact]
        public void QualityOfBackstagePassesis0whenTheSellInIs0(){
           var backstagePass = app.Items.Where(item => item.Name.Contains("Backstage passes") && item.SellIn == 5).FirstOrDefault();

           for(int i = 0; i<6; i++){
                app.UpdateQuality();
           }

           Assert.Equal(0, backstagePass.Quality);


        }



        [Fact]
        public void AgedBriesQualityIncreases()
        {
            app.UpdateQuality();
            var brie = app.Items.Where(item => item.Name == "Aged Brie").FirstOrDefault();
            Assert.Equal(1, brie.Quality);
        }

        [Fact]
        public void common_item_degrade_twice_as_fast_when_sellin_date_reached()
        {
            var item = new Item { Name = "Common Item", SellIn = 2, Quality = 10 };
            app.Items.Add(item);

            for (int i = 0; i < 3; i++)
            {
                app.UpdateQuality();
            }

            Assert.Equal(6, item.Quality);
        }

        [Fact]
        public void common_item_cannot_reach_negative_quality()
        {

            var item = new Item { Name = "Common Item", SellIn = 10, Quality = 0 };
            app.Items.Add(item);
            app.UpdateQuality();

            Assert.Equal(0, item.Quality);
        }

        [Fact]
        public void sulfuras_quality_and_sellin_properties_never_change()
        {

            Assert.Equal(0, app.Items[3].SellIn);
            Assert.Equal(80, app.Items[3].Quality);

            app.UpdateQuality();

            Assert.Equal(0, app.Items[3].SellIn);
            Assert.Equal(80, app.Items[3].Quality);
        }

        //Tests to make:
        // "Backstage passes", like aged brie, increases in Quality as it's SellIn value approaches; Quality increases by 2 when there are 10 days or less and by 3 when there are 5 days or less but Quality drops to 0 after the concert
    }
}