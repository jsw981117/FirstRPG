class Dungeon
{
    // 던전 정보 관리
    public string Name { get; private set; } // 던전 이름
    public int RecommendedDefense { get; private set; } // 권장 방어력, 이 수치보다 낮으면 실패확률 40퍼
    public int BaseReward { get; private set; } // 기본 보상, 공격력에 따라 랜덤해져야함

    public Dungeon(string name, int recommendedDefense, int baseReward)
    {
        Name = name;
        RecommendedDefense = recommendedDefense;
        BaseReward = baseReward;
    }
}