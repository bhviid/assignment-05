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
                new Item { Name = "+5 Dexterity Vest", SellIn = 10, Quality = 20 },
                new Item { Name = "Aged Brie", SellIn = 2, Quality = 0 },
                new Item { Name = "Elixir of the Mongoose", SellIn = 5, Quality = 7 },
                new Item { Name = "Sulfuras, Hand of Ragnaros", SellIn = 0, Quality = 80 },
                new Item { Name = "Sulfuras, Hand of Ragnaros", SellIn = -1, Quality = 80 },
                new Item
                {
                    Name = "Backstage passes to a TAFKAL80ETC concert",
                    SellIn = 15,
                    Quality = 20
                },
                new Item
                {
                    Name = "Backstage passes to a TAFKAL80ETC concert",
                    SellIn = 10,
                    Quality = 49
                },
                new Item
                {
                    Name = "Backstage passes to a TAFKAL80ETC concert",
                    SellIn = 5,
                    Quality = 49
                },
				// this conjured item does not work properly yet
				new Item { Name = "Conjured Mana Cake", SellIn = 3, Quality = 6 }
            };            
        }

        public void UpdateQuality()
        {
            foreach(Item item in Items)
            {
                switch (item.Name)
                {
                    case "Sulfuras, Hand of Ragnaros":
                        break;
                    case "Aged Brie":
                        UpdateBrie(item);
                        break;
                    case "Backstage passes to a TAFKAL80ETC concert":
                        UpdateBackstage(item);
                        break;
                    case "Conjured Mana Cake":
                        UpdateDefault(item, 2);
                        break;
                    default:
                        UpdateDefault(item);
                        break;
                }
            }
        }

        public void UpdateDefault(Item item, int qualityDegrade = 1)
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

        public void UpdateBrie(Item item)
        {
            if (IsQualityLessThanMax(item))
            {
                item.Quality += 1;
            }

            item.SellIn -= 1;

            if (item.SellIn < 0 && IsQualityLessThanMax(item))
            {
                item.Quality += 1;
            }
        }

        public void UpdateBackstage(Item item)
        {
            if (IsQualityLessThanMax(item))
            {
                item.Quality += 1;
            }

            if (item.SellIn < 11 && IsQualityLessThanMax(item))
            {
                item.Quality += 1;
            }

            if (item.SellIn < 6 && IsQualityLessThanMax(item))
            {
                item.Quality += 1;
            }

            item.SellIn -= 1;

            if (item.SellIn < 0)
            {
                item.Quality = 0;
            }
        }

        private bool IsQualityLessThanMax(Item item)
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

    public class Item
    {
        public string Name { get; set; }

        public int SellIn { get; set; }

        public int Quality { get; set; }
    }

}