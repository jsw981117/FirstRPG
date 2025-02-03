class Item
{
    public string Name { get; private set; }
    public int Attack { get; private set; }
    public int Defense { get; private set; }
    public int Price { get; private set; }
    public bool IsEquipped { get; set; } = false;
    public bool Purchased { get; set; } = false;

    public Item(string name, int attack, int defense, int price)
    {
        Name = name;
        Attack = attack;
        Defense = defense;
        Price = price;
    }

    public override string ToString()
    {
        return $"{Name} | 공격력 {Attack} | 방어력 {Defense} | {(Purchased ? "구매완료" : Price + " G")}";
    }
}