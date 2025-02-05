class Item
{
    // 아이템 정보를 관리합니다.
    // 타입을 넣을 때 공격력, 방어력 둘 중 하나만 뜨게 수정했어야 했는데 까먹었습니다;
    public string Name { get; private set; }
    public int Attack { get; private set; }
    public int Defense { get; private set; }
    public int Price { get; private set; }
    public bool IsEquipped { get; set; } = false;
    public bool Purchased { get; set; } = false;
    public ItemType Type { get; private set; }

    public Item(string name, ItemType type, int attack, int defense, int price)
    {
        Name = name;
        Type = type;
        Attack = attack;
        Defense = defense;
        Price = price;
    }

    public override string ToString()
    {
        return $"{Name} | {Type} | 공격력 {Attack} | 방어력 {Defense} | {(Purchased ? "구매완료" : Price + " G")}";
    }
}

// 아이템 타입을 처음에 그냥 string으로 했는데 구글 뒤져보니까 enum으로 관리하는게 좋다해서 수정했습니다.
public enum ItemType
{
    Weapon,
    Armor
}