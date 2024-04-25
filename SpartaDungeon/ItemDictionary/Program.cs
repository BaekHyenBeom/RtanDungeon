namespace ItemDictionary
{
    internal class Program
    {
        class Item
        {
            public string[] name { get; }
            private string[] desc { get; }
            private int[] value { get; }
            private int[] price { get; }

            private string itemText;

            public Item()
            {
                name =
                [
                    "몽둥이",
                    "낡은 검",
                    "보급형 검",
                    "전투 도끼",
                    "스파르타의 창",
                    "클레이모어"
                ];
                desc =
                [
                    "옛날에 말을 안 듣는 사람에게는 이게 약이었다",
                    "제 기능을 하기에는 많이 낡아보이는 검이다",
                    "대량 생산에 초점을 맞춘 병사 보급용 검이다",
                    "전투를 위해 날을 규격보다 크게 늘린 도끼이다",
                    "스파르타의 전사들이 사용했다는 전설의 창이다",
                    "140cm에 달하는 양손검으로 다루기 힘든 검이다"
                ];
                value =
                [ 2, 3, 5, 8, 15, 30 ];
                price =
                [ 200, 300, 400, 600, 1000, 2000 ];

                itemText = "공격력";
        }
            public Item(int a)
            {
                name =
                [
                    "천 갑옷",
                    "가죽 갑옷",
                    "사슬 갑옷",
                    "스파르타 갑옷",
                    "판금 갑옷",
                    "전신판금 갑옷"
                ];
                desc =
                [
                    "갑옷이라기엔 단순히 생활복에 가까운 장비이다",
                    "공방에서 가죽을 이용해 만들어진 가죽갑옷이다",
                    "사슬을 엮어 만든 갑옷, 좋은 방어력을 제공한다",
                    "스파르타 전사들이 사용했다는 전설의 갑옷이다",
                    "장인이 만든 갑옷으로 최고의 방어력을 제공한다",
                    "야금술의 정점. 최강의 방어력을 자랑한다"
                ];
                value =
                [ 2, 4, 8, 15, 25, 40 ];
                price =
                [ 100, 200, 450, 800, 1500, 3000 ];
            }
            public void GiveItemValue(int idx, ref int c)
            {
                c = value[idx];
            }

            public void GiveItemValue(int idx, ref string a, ref string b, ref int c, ref int d)
            {
                a = name[idx];
                b = desc[idx];
                c = value[idx];
                d = price[idx];
            }
        }

        class DimensionItems
        {
            public int itemPool    { get; }
            int[,] arrInt    { get; }
            string[,] arrStr { get; }

            string itemText;
            public DimensionItems()
            {
                itemPool = 1;
                arrInt = new int[,]
                {
                    { 2, 3, 5, 8, 15, 30 },
                    { 100, 200, 450, 800, 1500, 3000 }
                };

                arrStr = new string[,]
                {
                    {
                    "몽둥이",
                    "낡은 검",
                    "보급형 검",
                    "전투 도끼",
                    "스파르타의 창",
                    "클레이모어"
                    },
                    {
                    "옛날에 말을 안 듣는 사람에게는 이게 약이었다",
                    "제 기능을 하기에는 많이 낡아보이는 검이다",
                    "대량 생산에 초점을 맞춘 병사 보급용 검이다",
                    "전투를 위해 날을 규격보다 크게 늘린 도끼이다",
                    "스파르타의 전사들이 사용했다는 전설의 창이다",
                    "140cm에 달하는 양손검으로 다루기 힘든 검이다"
                    }
                };

                itemText = "공격력";

            }

            public DimensionItems(int j)
            {
                itemPool = 2;
                arrInt = new int[,]
                {
                    { 2, 4, 8, 15, 25, 40 },
                    { 100, 200, 450, 800, 1500, 3000 }
                };

                arrStr = new string[,]
                {
                    {
                    "천 갑옷",
                    "가죽 갑옷",
                    "사슬 갑옷",
                    "스파르타 갑옷",
                    "판금 갑옷",
                    "전신판금 갑옷"
                    },
                    {
                    "갑옷이라기엔 단순히 생활복에 가까운 장비이다",
                    "공방에서 가죽을 이용해 만들어진 가죽갑옷이다",
                    "사슬을 엮어 만든 갑옷, 좋은 방어력을 제공한다",
                    "스파르타 전사들이 사용했다는 전설의 갑옷이다",
                    "장인이 만든 갑옷으로 최고의 방어력을 제공한다",
                    "야금술의 정점. 최강의 방어력을 자랑한다"
                    }
                };

                itemText = "방어력";

            }

            void GiveValue(ref int dPoint)
            {

            }
            void GiveValue(int idx, ref int a, ref int b, ref string c, ref string d)
            {
                a = arrInt[0,idx];
                b = arrInt[1, idx];
                c = arrStr[0, idx];
                d = arrStr[1, idx];
            }

            public void ShowDictionary(int i)   // 단일 공개
            {
                for (int j = 0; j < arrStr.GetLength(0); j++)
                {
                    Console.Write(arrStr[j, i]); Tab();
                    Console.Write(arrInt[j, i]); Tab();
                }
                    Console.WriteLine();
            }

            public void ShowDictionary()       // 전부 공개
            {
                for(int i = 0; i < arrInt.GetLength(1); i++)
                {
                    for(int j = 0; j < arrStr.GetLength(0); j++)
                    {
                        Console.Write(arrStr[j, i]); Tab();
                        Console.Write(arrInt[j, i]); Tab();
                    }
                    Console.WriteLine();
                }
            }

            public void ShowItem()
            {
                int a = 0; int b = 0; string c = null; string d = null;
                for (int i = 0; i < arrStr.GetLength(1); i++)
                {
                    GiveValue(i, ref a, ref b, ref c, ref d);
                    Paint("-"); Console.Write($" {c}\t");
                    Paint("|"); Console.Write($" {itemText} +{a} \t");
                    Paint("|"); Console.Write($" {d}\t\t");
                    Paint("|"); Console.Write($" {b} "); Paint("G");
                    Blank();
                }
            }
            public void ShowItem(int i)
            {
                int a = 0; int b = 0; string c = null; string d = null;
                    GiveValue(i, ref a, ref b, ref c, ref d);
                    Paint("-"); Console.Write($" {c}\t");
                    Paint("|"); Console.Write($" {itemText} +{a} \t");
                    Paint("|"); Console.Write($" {d}\t\t");
                    Paint("|"); Console.Write($" {b} "); Paint("G");
                    Blank();
            }

        }

        // 캐릭터 정보창
        class Character
        {
            public string Name { get; }
            public int Level { get; }
            public int Damage { get; }
            public int Defense { get; }
            public int HealthPoint { get; }
            public int Gold { get; }

            private List<int> weaponInventory = new List<int>();
            private List<int> armorInventory = new List<int>();

            int[] weaponEquip = [-1];
            int[] armorEquip = [-1];

            public Character()
            {
                Name = "Rtan";
                Level = 1;
                Damage = 10;
                Defense = 5;
                HealthPoint = 100;
                Gold = 1500;

                weaponInventory.Add(2);
                //armorInventory.Add(2);
            }

            public Character(string str)
            {
                Name = str;
                Level = 1;
                Damage = 10;
                Defense = 5;
                HealthPoint = 100;
                Gold = 1500;
            }

            public void InventoryShow(DimensionItems item)
            {
                switch (item.itemPool)
                {
                    case 1:
                        if (weaponInventory.Count != 0)
                        {
                            foreach (int i in weaponInventory)
                            {
                                item.ShowItem(i);
                            }
                        }
                        else { Paint("공격 무기가 없습니다!"); Blank(); }
                        break;
                    case 2:
                        if (armorInventory.Count != 0)
                        {
                            foreach (int i in weaponInventory)
                            {
                                item.ShowItem(i);
                            }
                        }
                        else { Paint("방어 무기가 없습니다!"); Blank(); }
                        break;
                }
            }

            void Equip()
            {

            }

            void BuyItem()
            {

            }
        }

        // 색칠 도구들
        public static void Paint(int num)
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.Write(num);
            Console.ResetColor();
        }
        public static void Paint(string text)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write(text);
            Console.ResetColor();
        }


        public static void Tab()
        {
            Console.Write("\t");
        }

        public static void Blank()
        {
            Console.WriteLine();
        }



        class DictionaryItems
        {
            Dictionary<int, int> itemValue = new Dictionary<int, int>();    // 2개가 될 줄 알았는데 아니었다;
            public DictionaryItems()
            {
                itemValue.Add(1, 01010101);
                itemValue.Add(2, 00000000);
                itemValue.Add(3, 00000000);
                itemValue.Add(4, 00000000);
                itemValue.Add(5, 00000000);
                itemValue.Add(6, 00000000);
                itemValue.Add(7, 00000000);
                itemValue.Add(8, 00000000);
                itemValue.Add(9, 00000000);
            }

            public void ItemInfo()
            {

            }
        }

        static void Main(string[] args)
        {
            DimensionItems weapons = new DimensionItems();
            DimensionItems armors = new DimensionItems(1);
            Character player = new Character();

            weapons.ShowDictionary();
            Console.WriteLine();
            weapons.ShowDictionary(1);
            Console.WriteLine();
            weapons.ShowItem();

            Console.WriteLine();
            Console.WriteLine();

            armors.ShowDictionary();
            Console.WriteLine();
            armors.ShowDictionary(1);
            Console.WriteLine();
            armors.ShowItem();



            player.InventoryShow(weapons);
            player.InventoryShow(armors);

            Console.WriteLine();
            int[] value;
            value = [2, 4, 8, 15, 25, 40];
            Console.WriteLine($"value[0]의 값 : {value[0]}");
        }
    }
}
