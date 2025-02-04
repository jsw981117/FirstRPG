class Inn
{
    private const int RestCost = 500;

    public void EnterInn(Character player)
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("휴식하기\n");
            Console.WriteLine($"{RestCost} G를 내면 체력을 회복할 수 있습니다. (보유 골드 : {player.Gold} G)\n");
            Console.WriteLine("1. 휴식하기");
            Console.WriteLine("0. 나가기");
            Console.Write("\n원하시는 행동을 입력해주세요.\n>>");

            string input = Console.ReadLine();
            switch (input)
            {
                case "1":
                    Rest(player);
                    break;
                case "0":
                    return;
                default:
                    Console.WriteLine("잘못된 입력입니다.");
                    Console.ReadLine();
                    break;
            }
        }
    }

    private void Rest(Character player)
    {
        if (player.Gold >= RestCost)
        {
            player.Gold -= RestCost;
            player.RestoreHealth();
            Console.WriteLine("휴식을 완료했습니다. 체력이 회복되었습니다.");
        }
        else
        {
            Console.WriteLine("Gold가 부족합니다.");
        }
        Console.ReadLine();
    }
}