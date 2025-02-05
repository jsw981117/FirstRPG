class DungeonManager
{
    private List<Dungeon> dungeons = new List<Dungeon>
    {
        new Dungeon("쉬운 던전", 5, 1000),
        new Dungeon("일반 던전", 11, 1700),
        new Dungeon("어려운 던전", 17, 2500),
        new Dungeon("가장 어두운 던전", 25, 4000)
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

        int failChance = player.Defense < dungeon.RecommendedDefense ? 40 : 0; // 권장 방어력보다 낮으면 실패 확률 있음

        if (RandomGenerator.Instance.Next(100) < failChance) // 던전 실패
        {
            int healthLoss = (player.Health / 2); // 체력 절반 삭제
            player.SetHealth(player.Health - healthLoss);
            Console.WriteLine("던전 실패...\n");
            Console.WriteLine($"체력 {player.Health + healthLoss} -> {player.Health}");
            Console.WriteLine("보상을 얻지 못했습니다.");
        }
        else // 던전 클리어
        {
            // 던전 클리어 시에는 캐릭터 방어력만큼 깎이는 체력이 조정되기 때문에 따로 메서드 만들어줬습니다.
            // 보상도 마찬가지로 메서드 만들어서 관리해줍니다.
            int healthLoss = CalculateHealthLoss(player.Defense, dungeon.RecommendedDefense);
            int reward = CalculateReward(player.Attack, dungeon.BaseReward);

            player.SetHealth(player.Health - healthLoss);
            player.Gold += reward;
            player.CompleteDungeon();

            Console.WriteLine("던전 클리어!");
            Console.WriteLine($"축하합니다!! {dungeon.Name}을(를) 클리어하였습니다.\n");
            Console.WriteLine("[탐험 결과]");
            Console.WriteLine($"체력 {player.Health + healthLoss} -> {player.Health}");
            Console.WriteLine($"Gold {player.Gold - reward} G -> {player.Gold} G");
        }

        Console.WriteLine("\n0. 나가기");
        Console.ReadLine();
    }

    // 던전 클리어 시 방어력에 따른 체력 감소 수치 계산 시스템
    private int CalculateHealthLoss(int playerDefense, int recommendedDefense)
    {
        int defenseDiff = recommendedDefense - playerDefense; // 권장 방어력에서 캐릭터 방어력을 빼줌
        int minLoss = 20 + Math.Max(defenseDiff, 0);  // 최소 체력 감소량 조정
        int maxLoss = 35 + Math.Max(defenseDiff, 0);  // 최대 체력 감소량 조정

        return RandomGenerator.Instance.Next(minLoss, maxLoss + 1);
    }

    // 보상 시스템
    private int CalculateReward(int attack, int baseReward)
    {
        double bonusMultiplier = 1 + RandomGenerator.Instance.NextDouble() * attack; // 공격력 ~ 공격력 * 2 범위
        return (int)(baseReward * bonusMultiplier);
    }
}
