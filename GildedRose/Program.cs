using System;
using System.Collections.Generic;

namespace GildedRose
{
    public class Program
    {
        private static int MAX_QUALITY = 50;
        public IList<Item> Items;
        public static void Main(string[] args)
        {
            System.Console.WriteLine("OMGHAI!");

            var app = new Program() {Items = InitItems()};

            app.TheWayOfTime(31);
        }

        public static IList<Item> InitItems()
        {
            return new List<Item>
            {
                new StandardItem { Name = "+5 Dexterity Vest", SellIn = 10, Quality = 20 },
                new BrieItem { Name = "Aged Brie", SellIn = 2, Quality = 0 },
                new StandardItem { Name = "Elixir of the Mongoose", SellIn = 5, Quality = 7 },
                new LegendaryItem { Name = "Sulfuras, Hand of Ragnaros", SellIn = 0, Quality = 80 },
                new LegendaryItem { Name = "Sulfuras, Hand of Ragnaros", SellIn = -1, Quality = 80 },
                new BackstageItem
                {
                    Name = "Backstage passes to a TAFKAL80ETC concert",
                    SellIn = 15,
                    Quality = 20
                },
                new BackstageItem
                {
                    Name = "Backstage passes to a TAFKAL80ETC concert",
                    SellIn = 10,
                    Quality = 49
                },
                new BackstageItem
                {
                    Name = "Backstage passes to a TAFKAL80ETC concert",
                    SellIn = 5,
                    Quality = 49
                },
				// this conjured item does not work properly yet
				new ConjuredItem { Name = "Conjured Mana Cake", SellIn = 3, Quality = 6 }
            };            
        }

        // Open-closed principle
        public void UpdateQuality()
        {
            foreach(Item item in Items)
            {
                item.Update();
            }
        }

        public static bool IsQualityLessThanMax(Item item)
        {
            return item.Quality < MAX_QUALITY;
        }

        private void TheWayOfTime(int days)
        {
            for (var i = 0; i < days; i++)
            {
                Console.WriteLine("-------- day " + i + " --------");
                Console.WriteLine("name, sellIn, quality");
                for (var j = 0; j < Items.Count; j++)
                {
                    Console.WriteLine(Items[j].Name + ", " + Items[j].SellIn + ", " + Items[j].Quality);
                }
                Console.WriteLine("");
                UpdateQuality();
            }
        }

    }

    /*  We chose to rewrite the Item class ever so slightly, making it an abstract class, with a new abstract method,
        Update(), which lets us use inheritance and polymorphism to refacture the code
        Also, We begged the goblin for mercy, and he decided he had a good day :),
        but as an emergency action, our hunter had tranq arrow for the enrage effect, and the dk anti magic shell to protect from the damage */
    public abstract class Item
    {
        public string Name { get; set; }

        public int SellIn { get; set;}

        public int Quality { get; set; }
        public abstract void Update();
    }

    public class LegendaryItem : Item{
        public override void Update() {  }
    }

    public class BrieItem : Item{
        public override void Update() { 
             if (Program.IsQualityLessThanMax(this))
            {
                Quality += 1;
            }

            SellIn -= 1;

            if (SellIn < 0 && Program.IsQualityLessThanMax(this))
            {
                Quality += 1;
            }
         }
    }

    public class ConjuredItem : StandardItem
    {
        public override void Update() 
        { 
            UpdateDefault(this,2);
        }
    }
    public class BackstageItem : Item{
        public override void Update() {
            if (Program.IsQualityLessThanMax(this))
            {
                Quality += 1;
            }

            if (SellIn < 11 && Program.IsQualityLessThanMax(this))
            {
                Quality += 1;
            }

            if (SellIn < 6 && Program.IsQualityLessThanMax(this))
            {
                Quality += 1;
            }

            SellIn -= 1;

            if (SellIn < 0)
            {
                Quality = 0;
            }
          }
    }

    public class StandardItem : Item{
        public override void Update() 
        { 
            UpdateDefault(this);
        }

        protected void UpdateDefault(Item item, int qualityDegrade = 1)
        {
            if (item.Quality > 0)
            {
                item.Quality -= qualityDegrade;
            }

            item.SellIn -= 1;

            if (item.SellIn < 0)
            {
                if (item.Quality > 0)
                {
                    item.Quality -= qualityDegrade;
                }
            }
        }
    }
}