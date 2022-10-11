namespace GildedRose.Tests;

using GildedRose;
public class ProgramTests
{
    //Run tests using: 
        // dotnet test /p:CollectCoverage=true
    private readonly Program _program;
    private readonly IList<Item> _items;
    public ProgramTests()
    {
        _program = new Program() {Items = new List<Item>()};
        _items = _program.Items;
    }
    
    [Fact]
    public void TestTheTruth()
    {
        true.Should().BeTrue();
    }

    [Fact]
    public void UpdateQuality_standarditem()
    {
        // Arrange
        _program.Items.Add(new StandardItem{Name = "+5 Dexterity Vest", SellIn = 10, Quality = 20});    

        // Act
        _program.UpdateQuality();

        //Assert
        _program.Items[0].SellIn.Should().Be(9);
        _program.Items[0].Quality.Should().Be(19);
    }

    [Fact]
    public void UpdateQuality_standarditem_sellin_has_been_passed()
    {
        // Arrange
        _program.Items.Add(new StandardItem{Name = "+5 Dexterity Vest", SellIn = 0, Quality = 20});    

        // Act
        _program.UpdateQuality();

        //Assert
        _program.Items[0].SellIn.Should().Be(-1);
        _program.Items[0].Quality.Should().Be(18);
    }

    [Fact]
    public void UpdateQuality_standarditem_quality_never_negative()
    {
        // Arrange
        _program.Items.Add(new StandardItem{Name = "+5 Dexterity Vest", SellIn = 2, Quality = 5});    

        // Act
        for (int i = 0; i < 5; i++)
        {
            _program.UpdateQuality();
        }

        //Assert
        _program.Items[0].SellIn.Should().Be(-3);
        _program.Items[0].Quality.Should().Be(0);
    }

    [Fact]
    public void UpdateQuality_generic_item_past_sellin_date()
    {
        // Arrange
        _program.Items.Add(new StandardItem{Name = "+5 Dexterity Vest", SellIn = -1, Quality = 20});    

        // Act
        _program.UpdateQuality();

        //Assert
        _program.Items[0].SellIn.Should().Be(-2);
        _program.Items[0].Quality.Should().Be(18);
    }
    
    [Fact]
    public void UpdateQuality_ETC_item_more_than_10()
    {
        // Arrange
        _program.Items.Add(new BackstageItem{Name = "Backstage passes to a TAFKAL80ETC concert",
                                    SellIn = 15,
                                    Quality = 20});    

        // Act
        _program.UpdateQuality();

        //Assert
        _program.Items[0].SellIn.Should().Be(14);
        _program.Items[0].Quality.Should().Be(21);
    }

    [Fact]
    public void UpdateQuality_ETC_item_less_than_11()
    {
        // Arrange
        _program.Items.Add(new BackstageItem{Name = "Backstage passes to a TAFKAL80ETC concert",
                                    SellIn = 9,
                                    Quality = 20});    

        // Act
        _program.UpdateQuality();

        //Assert
        _program.Items[0].SellIn.Should().Be(8);
        _program.Items[0].Quality.Should().Be(22);
    }

    [Fact]
    public void UpdateQuality_ETC_item_less_than_6()
    {
        // Arrange
        _program.Items.Add(new BackstageItem{Name = "Backstage passes to a TAFKAL80ETC concert",
                                    SellIn = 5,
                                    Quality = 20});    

        // Act
        _program.UpdateQuality();

        //Assert
        _program.Items[0].SellIn.Should().Be(4);
        _program.Items[0].Quality.Should().Be(23);
    }

    [Fact]
    public void UpdateQuality_ETC_item_worthless()
    {
        // Arrange
        _program.Items.Add(new BackstageItem{Name = "Backstage passes to a TAFKAL80ETC concert",
                                    SellIn = 0,
                                    Quality = 20});    

        // Act
        _program.UpdateQuality();

        //Assert
        _program.Items[0].SellIn.Should().Be(-1);
        _program.Items[0].Quality.Should().Be(0);
    }
    
    [Fact]
    public void UpdateQuality_brie_item()
    {
        // Arrange
        _program.Items.Add(new BrieItem { Name = "Aged Brie", SellIn = 2, Quality = 0 });    

        // Act
        _program.UpdateQuality();

        //Assert
        _program.Items[0].SellIn.Should().Be(1);
        _program.Items[0].Quality.Should().Be(1);
    }

    [Fact]
    public void UpdateQuality_brie_item_past_sellin_date()
    {
        // Arrange
        _program.Items.Add(new BrieItem { Name = "Aged Brie", SellIn = -2, Quality = 4 });    

        // Act
        _program.UpdateQuality();

        //Assert
        _program.Items[0].SellIn.Should().Be(-3);
        _program.Items[0].Quality.Should().Be(6);
    }

    [Fact]
    public void UpdateQuality_brie_item_quality_never_surpass_50()
    {
        // Arrange
        _program.Items.Add(new BrieItem { Name = "Aged Brie", SellIn = -2, Quality = 49 });    

        // Act
        _program.UpdateQuality();

        //Assert
        _program.Items[0].SellIn.Should().Be(-3);
        _program.Items[0].Quality.Should().Be(50);
    }
    
    [Fact]
    public void UpdateQuality_legendary_item()
    {
        // Arrange
        var SellInBefore = 10;
        var QualityBefore = 80;
        _program.Items.Add(
            new LegendaryItem{   Name = "Sulfuras, Hand of Ragnaros", 
                        SellIn = SellInBefore, 
                        Quality = QualityBefore }
            );    

        // Act
        _program.UpdateQuality();

        //Assert
        _program.Items[0].SellIn.Should().Be(SellInBefore);
        _program.Items[0].Quality.Should().Be(QualityBefore);
    }
    [Fact]
    public void UpdateQuality_conjured_item_within_sellin(){
       
        // Arrange
        _program.Items.Add(new ConjuredItem{Name = "Conjured Mana Cake", SellIn = 5, Quality = 20});    

        // Act
        _program.UpdateQuality();

        //Assert
        _program.Items[0].SellIn.Should().Be(4);
        _program.Items[0].Quality.Should().Be(18);
    }

    [Fact]
    public void UpdateQuality_conjured_item_past_sellin_date(){
       
        // Arrange
        _program.Items.Add(new ConjuredItem{Name = "Conjured Mana Cake", SellIn = 0, Quality = 20});    

        // Act
        _program.UpdateQuality();

        //Assert
        _program.Items[0].SellIn.Should().Be(-1);
        _program.Items[0].Quality.Should().Be(16);
    }

    // Gotta pump the numbers yo
    [Theory]
    [InlineData("+5 Dexterity Vest",10, 20)]
    [InlineData("Sulfuras, Hand of Ragnaros", 0, 80)]
    [InlineData("Backstage passes to a TAFKAL80ETC concert",15, 20)]
    [InlineData("Aged Brie", 2, 0)]
    [InlineData( "Conjured Mana Cake", 3, 6)]
    public void InitItems_contains(string itemName, int itemSellIn,int itemQuality)
    {
        // Arrange

        // Act
        var res = Program.InitItems();

        // Assert
        Assert.Contains(res, i => i.Name == itemName 
                                && i.SellIn == itemSellIn 
                                && i.Quality == itemQuality);
    }
}