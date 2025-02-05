class Character
{
    // 캐릭터의 상태 관리
    public string Name { get; private set; }
    public string Job { get; private set; }
    public int Level { get; private set; } = 1;
    public int BaseAttack { get; private set; } = 10;
    public int BaseDefense { get; private set; } = 5;
    public int Health { get; private set; } = 100;
    public int Gold { get; set; } = 1500;
    public int DungeonClearCount { get; private set; } = 0;

    public List<Item> Inventory { get; private set; } = new List<Item>();
    private Item equippedWeapon;
    private Item equippedArmor;

    public Character(string name, string job)
    {
        Name = name;
        Job = job;
    }

    public int Attack => BaseAttack + (equippedWeapon?.Attack ?? 0); // 무기, 방어구 장비 했으면 공격력, 방어력 가져옴 null 이면 0 리턴
    public int Defense => BaseDefense + (equippedArmor?.Defense ?? 0);

    // 여기서 캐릭터 정보를 출력합니다
    public void ShowStatus()
    {
        Console.Clear();
        Console.WriteLine("상태 보기\n");
        Console.WriteLine($"Lv. {Level}  {Name} ({Job})");
        Console.WriteLine($"공격력 : {Attack} {(equippedWeapon != null ? $"(+{equippedWeapon.Attack})" : "")}");
        Console.WriteLine($"방어력 : {Defense} {(equippedArmor != null ? $"(+{equippedArmor.Defense})" : "")}");
        Console.WriteLine($"체 력 : {Health}");
        Console.WriteLine($"Gold : {Gold} G\n");
        Console.WriteLine("0. 나가기");
        Console.ReadLine();
    }

    // 인벤토리도 여기서 관리합니다. 능력치 변경 자체를 Character 클래스에서만 하게 하려고
    public void ManageInventory()
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
            Console.WriteLine("\n원하는 아이템 번호를 입력하여 장착/해제하세요.");
        }
        Console.WriteLine("0. 나가기");

        string input = Console.ReadLine();
        if (int.TryParse(input, out int index) && index >= 1 && index <= Inventory.Count)
        {
            EquipItem(Inventory[index - 1]);
        }
        else if (input != "0")
        {
            Console.WriteLine("잘못된 입력입니다.");
            Console.ReadLine();
        }
    }

    // 아이템 장착
    public void EquipItem(Item item)
    {
        if (item.Type == ItemType.Weapon)
        {
            if (equippedWeapon != null)
            {
                equippedWeapon.IsEquipped = false; // 기존 무기 해제
            }
            equippedWeapon = item;
        }
        else if (item.Type == ItemType.Armor)
        {
            if (equippedArmor != null)
            {
                equippedArmor.IsEquipped = false; // 기존 방어구 해제
            }
            equippedArmor = item;
        }

        item.IsEquipped = true;
        Console.WriteLine($"{item.Name}을(를) 장착했습니다.");
        Console.ReadLine();
    }

    public void AddItem(Item item)
    {
        Inventory.Add(item);
    }
    public void RestoreHealth() // Inn 클래스에서 Health를 건드릴 수가 없어서 회복 메서드가 필요했습니다
    {
        Health = 100;
    }
    public void SetHealth(int value)
    {
        Health = Math.Max(0, value); // 체력이 0 이하로 내려가지 않도록. 게임 오버를 구현 못했음
    }
    public void LevelUp()
    {
        int requiredClears = Level; // 다음 레벨까지 필요한 클리어 횟수 = 현재 레벨이므로
        if (DungeonClearCount >= requiredClears)
        {
            DungeonClearCount = 0; // 클리어 횟수 초기화
            Level++; // 렙 증가
            BaseAttack += 1; // 0.5 증가여야 하는데 처음에 int로 설정해둬서 못고쳤습니다;;
            BaseDefense += 1;
            Console.WriteLine($"레벨 업! {Level - 1} → {Level}");
            Console.WriteLine($"공격력 +1, 방어력 +1 증가!");
        }
    }

    public void CompleteDungeon()
    {
        DungeonClearCount++;
        LevelUp();
    }
}