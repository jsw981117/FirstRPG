public class RandomGenerator
{
    // 랜덤 싱글톤으로 구현하는게 더 확장성 있다해서 수정했습니다.
    private static readonly RandomGenerator instance = new RandomGenerator();
    private readonly Random random;

    private RandomGenerator()
    {
        random = new Random();
    }
    public static RandomGenerator Instance
    {
        get { return instance; }
    }
    public int Next(int maxValue)
    {
        return random.Next(maxValue);
    }

    public int Next(int minValue, int maxValue)
    {
        return random.Next(minValue, maxValue);
    }

    public double NextDouble()
    {
        return random.NextDouble();
    }
}