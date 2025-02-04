using System;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;

class Program
{
    static Character player = new Character("정순원", "전사");
    static Shop shop = new Shop();
    static Inn inn = new Inn();
    static DungeonManager dungeonManager = new DungeonManager();

    static void Main()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("스파르타 마을에 오신 여러분 환영합니다.");
            Console.WriteLine("이곳에서 던전으로 들어가기 전 활동을 할 수 있습니다.\n");
            Console.WriteLine("1. 상태 보기");
            Console.WriteLine("2. 인벤토리");
            Console.WriteLine("3. 상점");
            Console.WriteLine("4. 던전입장");
            Console.WriteLine("5. 휴식하기");

            Console.Write("\n원하시는 행동을 입력해주세요.\n>>");

            string input = Console.ReadLine();
            switch (input)
            {
                case "1": player.ShowStatus(); break;
                case "2": player.ManageInventory(); break;
                case "3": shop.OpenShop(player); break;
                case "4": dungeonManager.EnterDungeon(player); break;
                case "5": inn.EnterInn(player); break;
                default: Console.WriteLine("잘못된 입력입니다."); Console.ReadLine(); break;
            }
        }
    }
}