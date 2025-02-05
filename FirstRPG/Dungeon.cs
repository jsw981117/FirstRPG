class Dungeon
{
    public string Name { get; private set; } // 던전 이름
    public int RecommendedDefense { get; private set; } // 권장 방어력
    public int BaseReward { get; private set; } // 기본 보상

    public Dungeon(string name, int recommendedDefense, int baseReward)
    {
        Name = name;
        RecommendedDefense = recommendedDefense;
        BaseReward = baseReward;
    }
}