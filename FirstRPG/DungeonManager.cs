class DungeonManager
{
    private List<Dungeon> dungeons = new List<Dungeon>
    {
        new Dungeon("쉬운 던전", 5, 1000),
        new Dungeon("일반 던전", 11, 1700),
        new Dungeon("어려운 던전", 17, 2500)
    };

    public void EnterDungeon(Character player)
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("던전 입장\n");
            for (int i = 0; i < dungeons.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {dungeons[i].Name} | 방어력 {dungeons[i].RecommendedDefense} 이상 권장");
            }
            Console.WriteLine("0. 나가기");

            Console.Write("\n원하시는 던전을 선택해주세요.\n>>");
            string input = Console.ReadLine();

            if (input == "0") break;

            if (int.TryParse(input, out int index) && index >= 1 && index <= dungeons.Count)
            {
                StartDungeon(player, dungeons[index - 1]);
            }
            else
            {
                Console.WriteLine("잘못된 입력입니다."); Console.ReadLine();
            }
        }
    }

    private void StartDungeon(Character player, Dungeon dungeon)
    {
        Console.Clear();
        Console.WriteLine($"{dungeon.Name}에 도전합니다...\n");

        Random rand = new Random();
        int failChance = player.Defense < dungeon.RecommendedDefense ? 40 : 0; // 권장 방어력보다 낮으면 40% 실패 확률

        if (rand.Next(100) < failChance)
        {
            int healthLoss = (20 + rand.Next(16)) / 2; // 체력 소모 절반
            player.SetHealth(player.Health - healthLoss);
            Console.WriteLine("던전 실패...\n");
            Console.WriteLine($"체력 {player.Health + healthLoss} -> {player.Health}");
            Console.WriteLine("보상을 얻지 못했습니다.");
        }
        else
        {
            int healthLoss = CalculateHealthLoss(player.Defense, dungeon.RecommendedDefense);
            int reward = CalculateReward(player.Attack, dungeon.BaseReward);

            player.SetHealth(player.Health - healthLoss);
            player.Gold += reward;

            Console.WriteLine("던전 클리어!");
            Console.WriteLine($"축하합니다!! {dungeon.Name}을(를) 클리어하였습니다.\n");
            Console.WriteLine("[탐험 결과]");
            Console.WriteLine($"체력 {player.Health + healthLoss} -> {player.Health}");
            Console.WriteLine($"Gold {player.Gold - reward} G -> {player.Gold} G");
        }

        Console.WriteLine("\n0. 나가기");
        Console.ReadLine();
    }

    private int CalculateHealthLoss(int playerDefense, int recommendedDefense)
    {
        int defenseDiff = recommendedDefense - playerDefense;
        int minLoss = 20 + Math.Max(defenseDiff, 0);  // 최소 체력 감소량 조정
        int maxLoss = 35 + Math.Max(defenseDiff, 0);  // 최대 체력 감소량 조정

        Random rand = new Random();
        return rand.Next(minLoss, maxLoss + 1);
    }

    private int CalculateReward(int attack, int baseReward)
    {
        Random rand = new Random();
        double bonusMultiplier = 1 + rand.NextDouble() * attack; // 공격력 ~ 공격력 * 2 범위
        return (int)(baseReward * bonusMultiplier);
    }
}