class Shop
{
    private List<Item> shopItems = new List<Item>
    {
        new Item("수련자 갑옷", "Armor", 0, 5, 1000),
        new Item("무쇠갑옷", "Armor", 0, 9, 1500),
        new Item("스파르타의 갑옷", "Armor", 0, 15, 3500),
        new Item("낡은 검", "Weapon", 2, 0, 600),
        new Item("청동 도끼", "Weapon", 5, 0, 1500),
        new Item("스파르타의 창", "Weapon", 7, 0, 2000)
    };

    public void OpenShop(Character player)
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("상점\n");
            Console.WriteLine("[보유 골드] " + player.Gold + " G\n");
            Console.WriteLine("[아이템 목록]");
            for (int i = 0; i < shopItems.Count; i++)
            {
                Console.WriteLine($"- {i + 1} {shopItems[i]}");
            }
            Console.WriteLine("\n1. 아이템 구매");
            Console.WriteLine("2. 아이템 판매");
            Console.WriteLine("0. 나가기");
            Console.Write("\n원하시는 행동을 입력해주세요.\n>>");

            string input = Console.ReadLine();
            if (input == "0") break;
            if (input == "1") PurchaseItem(player);
            if (input == "2") SellItem(player);
        }
    }

    private void PurchaseItem(Character player)
    {
        Console.Write("구매할 아이템 번호를 입력해주세요: ");
        if (int.TryParse(Console.ReadLine(), out int index) && index >= 1 && index <= shopItems.Count)
        {
            Item item = shopItems[index - 1];
            if (item.Purchased)
            {
                Console.WriteLine("이미 구매한 아이템입니다.");
            }
            else if (player.Gold >= item.Price)
            {
                player.Gold -= item.Price;
                item.Purchased = true;
                player.AddItem(new Item(item.Name, item.Type, item.Attack, item.Defense, item.Price));
                Console.WriteLine("구매를 완료했습니다.");
            }
            else
            {
                Console.WriteLine("Gold가 부족합니다.");
            }
        }
        else
        {
            Console.WriteLine("잘못된 입력입니다.");
        }
        Console.ReadLine();
    }

    public void SellItem(Character player)
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("아이템 판매\n");
            Console.WriteLine($"[보유 골드] {player.Gold} G\n");

            if (player.Inventory.Count == 0)
            {
                Console.WriteLine("판매할 아이템이 없습니다.");
                Console.WriteLine("0. 나가기");
                Console.ReadLine();
                return;
            }

            Console.WriteLine("[아이템 목록]");
            for (int i = 0; i < player.Inventory.Count; i++)
            {
                Item item = player.Inventory[i];
                int sellPrice = (int)(item.Price * 0.85);
                Console.WriteLine($"- {i + 1} {(item.IsEquipped ? "[E]" : "")} {item.Name} | 판매가: {sellPrice} G");
            }

            Console.WriteLine("\n0. 나가기");
            Console.Write("판매할 아이템 번호를 입력해주세요: ");

            if (int.TryParse(Console.ReadLine(), out int index) && index >= 1 && index <= player.Inventory.Count)
            {
                Item selectedItem = player.Inventory[index - 1];
                int sellPrice = (int)(selectedItem.Price * 0.85);

                if (selectedItem.IsEquipped)
                {
                    selectedItem.IsEquipped = false;
                    Console.WriteLine($"[{selectedItem.Name}] 장착이 해제되었습니다.");
                }

                player.Inventory.RemoveAt(index - 1);
                player.Gold += sellPrice;
                Console.WriteLine($"[{selectedItem.Name}]을(를) {sellPrice} G에 판매했습니다.");
            }
            else if (index == 0)
            {
                return;
            }
            else
            {
                Console.WriteLine("잘못된 입력입니다.");
            }
            Console.ReadLine();
        }
    }
}