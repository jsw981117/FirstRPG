class Character
{
    public string Name { get; private set; }
    public string Job { get; private set; }
    public int Level { get; private set; } = 1;
    public int BaseAttack { get; private set; } = 10;
    public int BaseDefense { get; private set; } = 5;
    public int Health { get; private set; } = 100;
    public int Gold { get; set; } = 1500;
    public List<Item> Inventory { get; private set; } = new List<Item>();

    public Character(string name, string job)
    {
        Name = name;
        Job = job;
    }

    public void ShowStatus()
    {
        int equippedAttack = 0, equippedDefense = 0;
        foreach (var item in Inventory)
        {
            if (item.IsEquipped)
            {
                equippedAttack += item.Attack;
                equippedDefense += item.Defense;
            }
        }

        Console.Clear();
        Console.WriteLine("상태 보기\n");
        Console.WriteLine($"Lv. {Level}  {Name} ({Job})");
        Console.WriteLine($"공격력 : {BaseAttack + equippedAttack} (+{equippedAttack})");
        Console.WriteLine($"방어력 : {BaseDefense + equippedDefense} (+{equippedDefense})");
        Console.WriteLine($"체 력 : {Health}");
        Console.WriteLine($"Gold : {Gold} G\n");
        Console.WriteLine("0. 나가기");
        Console.ReadLine();
    }

    public void ManageInventory()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("인벤토리 - 장착 관리\n");
            if (Inventory.Count == 0)
            {
                Console.WriteLine("보유 중인 아이템이 없습니다.");
            }
            else
            {
                Console.WriteLine("[아이템 목록]");
                for (int i = 0; i < Inventory.Count; i++)
                {
                    Console.WriteLine($"- {i + 1} {(Inventory[i].IsEquipped ? "[E]" : "")} {Inventory[i]}");
                }
                Console.WriteLine("\n1. 장착 관리");
            }
            Console.WriteLine("0. 나가기");
            Console.Write("\n원하시는 행동을 입력해주세요.\n>>");

            string input = Console.ReadLine();
            if (input == "0") break;
            else if (input == "1") ManageEquipment();
        }
    }

    private void ManageEquipment()
    {
        Console.Write("장착/해제할 아이템 번호를 입력해주세요: ");
        if (int.TryParse(Console.ReadLine(), out int index) && index >= 1 && index <= Inventory.Count)
        {
            Item item = Inventory[index - 1];
            item.IsEquipped = !item.IsEquipped;
            Console.WriteLine(item.IsEquipped ? "장착되었습니다." : "장착이 해제되었습니다.");
        }
        else
        {
            Console.WriteLine("잘못된 입력입니다.");
        }
        Console.ReadLine();
    }

    public void AddItem(Item item)
    {
        Inventory.Add(item);
    }
}