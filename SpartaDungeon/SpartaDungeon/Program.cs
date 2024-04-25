using System;
using System.Numerics;
using static System.Net.Mime.MediaTypeNames;

namespace SpartaDungeon
{
    internal class Program
    {
        class OutPutText()
        {

            // 씬 선택을 담당
            public int SceneManager(int num, ref Character player, Item weapon, Item armor)
            {
                Console.Clear();
                if (num == 0)
                {
                    Village();
                    return select(1, 5);
                }

                else if (num == 1)
                {
                    Status(player);
                    return select(0, 0);
                }

                else if (num == 2)
                {
                    Inventory(player, weapon, armor);
                    int a = select(0, 1);
                    if (a == 1)
                    {
                        // 장비 기능 구현
                        int switchnum = 0; int maxColumn1 = 0; int maxColumn2 = 0;
                        player.InventoryLength(ref maxColumn1, ref maxColumn2);
                        do
                        {
                            Console.Clear();
                            InventorySelect(player, weapon, armor);
                            a = selectItem(0, 0, maxColumn1, maxColumn2, ref switchnum);
                            if (switchnum == 1)
                                player.Equip(switchnum, a, weapon);
                            else if (switchnum == 2)
                                player.Equip(switchnum, a, armor);
                        }
                        while (switchnum != 0);
                    }

                    return a;
                }

                else if (num == 3)
                {
                    Shop(player, weapon, armor);
                    int a = select(0, 2);
                    while (a != 0)
                    {
                        if (a == 1)
                        {
                            // 상점 기능 구현
                            int switchnum = 0;
                            int maxColumn1 = weapon.arrStr.GetLength(1); int maxColumn2 = maxColumn1;
                            Console.Clear();
                            do
                            {
                                ShopBuy(player, weapon, armor);
                                a = selectItem(0, 2, maxColumn1, maxColumn2, ref switchnum);
                                Console.Clear();
                                if (switchnum == 1)
                                {
                                    int price = weapon.GivePrice(a);
                                    player.BuyItem(price, switchnum, a);
                                }
                                else if (switchnum == 2)
                                {
                                    int price = armor.GivePrice(a);
                                    player.BuyItem(price, switchnum, a);
                                }
                            }
                            while (switchnum != 0);
                        }
                        if (a == 2)
                        {
                            // 상점 판매 기능 구현
                            int switchnum = 0; int maxColumn1 = 0; int maxColumn2 = 0;
                            player.InventoryLength(ref maxColumn1, ref maxColumn2);
                            Console.Clear();
                            do
                            {
                                ShopSell(player, weapon, armor);
                                a = selectItem(0, 1, maxColumn1, maxColumn2, ref switchnum);
                                Console.Clear();
                                List<int> b = new List<int>();
                                if (switchnum == 1)
                                {
                                    player.GiveInventory(weapon, ref b);
                                    int price = weapon.arrInt[1, b[a]];
                                    player.SellItem(price, switchnum, a);
                                }
                                else if (switchnum == 2)
                                {
                                    player.GiveInventory(armor, ref b);
                                    int price = armor.arrInt[1, b[a]];
                                    player.SellItem(price, switchnum, a);
                                }
                            }
                            while (switchnum != 0);
                        }
                    }

                    return a;
                }

                else if (num == 4)
                {
                    Dungeon(player, weapon, armor);
                    int a = select(0, 3);
                    Console.Clear();
                    bool clear = false; int damaged = 0; float bonus = 0; int price;
                    switch (a)
                    {
                        
                        case 1:
                            DungeonCheck(player, 5, ref clear, ref damaged, ref bonus);
                            if (clear)
                            {
                                price = Convert.ToInt32(1000 * bonus);
                                DungeonClear(player, damaged, price, "쉬운");
                                player.UseGold(-price);
                                player.HealthUp(-damaged);
                                player.ExperienceUp();
                            }
                            else
                            {
                                DungeonFalse(player, "쉬운");
                                player.HealthUp(-Convert.ToInt32(player.HealthPoint * 0.5));
                            }
                            break;
                        case 2:
                            DungeonCheck(player, 11, ref clear, ref damaged, ref bonus);
                            if (clear)
                            {
                                price = Convert.ToInt32(1700 * bonus);
                                DungeonClear(player, damaged, price, "일반");
                                player.UseGold(-price);
                                player.HealthUp(-damaged);
                                player.ExperienceUp();
                            }
                            else
                            {
                                DungeonFalse(player, "일반");
                                player.HealthUp(-Convert.ToInt32(player.HealthPoint * 0.5));
                            }
                            break;

                            break;
                        case 3:
                            price = Convert.ToInt32(2500 * bonus);
                            DungeonCheck(player, 17, ref clear, ref damaged, ref bonus);
                            if (clear)
                            {
                                price = Convert.ToInt32(1000 * bonus);
                                DungeonClear(player, damaged, price, "어려운");
                                player.UseGold(-price);
                                player.HealthUp(-damaged);
                                player.ExperienceUp();
                            }
                            else
                            {
                                DungeonFalse(player, "어려운");
                                player.HealthUp(-Convert.ToInt32(player.HealthPoint * 0.5));
                            }
                            break;
                        default:
                            return 0;
                    }
                    a = select(0, 0);
                }

                else if (num == 5)
                {
                    int a;
                    do
                    {
                        Rest(player);
                        a = select(0, 1);
                        Console.Clear();
                        if ( a == 1 )
                        {
                            if (player.Gold >= 500 )
                            {
                                player.HealthUp(100);
                                player.UseGold(500);
                                Paint("체력이 회복되었습니다!"); Blank();
                            }
                            else
                            {
                                Paint("돈이 부족합니다!", "r"); Blank();
                            }
                        }
                    } while (a != 0);
                }
                return 0;
            }

            // 선택
            int select(int minNum, int maxNum)
            {
                int b;
                Blank();
                Console.WriteLine("원하시는 행동을 입력해주세요:");
                Paint(">> ");
                
                for (;;)
                {
                    string[] a = Console.ReadLine().Split(" ");
                    if (a.Length > 1) { Paint("제대로 입력해주세요", ""); Blank(); continue; }
                        int.TryParse(a[0], out b);
                    if (b >= minNum && b <= maxNum )
                    {
                        break;
                    }
                    else
                    {
                        Paint("제대로 입력해주세요", ""); Blank();
                        Paint(">> ");
                    }
                }
                return b;
            }

            // 아이템 선택
            int selectItem(int minNum, int maxNum, int maxColumn1, int maxColumn2, ref int selected)
            {
                Blank();
                Console.WriteLine("예시: 1 2 \n원하시는 아이템을 입력해주세요:");
                Paint(">> ");

                int num;
                int num1;

                for (;;)
                {
                    string[] a = Console.ReadLine().Split(" ");
                    if (a.Length < 2)
                    {
                        if (int.TryParse(a[0], out num))
                        {
                            if (num >= minNum && num <= maxNum)
                            {
                                selected = 0;
                                return num;
                            }
                        }
                    }
                    else
                    {
                        if (int.TryParse(a[0], out num1) && int.TryParse(a[1], out num))
                        {
                            if (num1 == 1 && num >= 0 && num < maxColumn1)
                            {
                                selected = num1;
                                return num;
                            }
                            else if (num1 == 2 && num >= 0 && num < maxColumn2)
                            {
                                selected = num1;
                                return num;
                            }
                        }
                    }
                    Paint("잘못된 입력입니다.", ""); Blank();
                    Paint(">> ");
                }
            }

            public void DungeonCheck(Character player, int minDef, ref bool clear, ref int damaged, ref float bonus)
            {
                Random rand = new Random();
                int playerDef = player.Defense + player.ExtraDefense;
                int playerDam = player.Damage + player.ExtraDamage;

                if (playerDef < minDef)
                {
                    int a = rand.Next(0, 10);
                    if (a < 4) { clear = false; return; }
                    else
                    {
                        damaged = rand.Next(20, 36);
                        damaged -= (player.Defense - minDef);
                        if (damaged < 0) { damaged = 0; }
                        bonus = rand.Next(playerDam, playerDam * 2) * 0.1f;
                        clear = true;
                        return;
                    }
                }
                else
                {
                    damaged = rand.Next(20, 36);
                    damaged -= (playerDef - minDef);
                    if (damaged < 0 ) { damaged = 0; }
                    bonus = rand.Next(playerDam, playerDam*2) * 0.1f;
                    clear = true;
                    return;
                }
            }


                // 해당 씬의 장면 연출

            // 마을
            public void Village()
            {
                Console.WriteLine("스파르타 마을에 오신 여러분 환영합니다.\n이곳에서 던전으로 들어가기 전 활동을 할 수 있습니다.\n");
                Paint(1); Console.WriteLine(". 상태 보기");
                Paint(2); Console.WriteLine(". 인벤토리");
                Paint(3); Console.WriteLine(". 상점");
                Paint(4); Console.WriteLine(". 던전");
                Paint(5); Console.WriteLine(". 휴식하기");
            }

            // 상태창 관련
            public void Status(Character p)
            {
                Paint("상태 보기"); Blank();
                Console.WriteLine("캐릭터의 정보가 표시됩니다.");
                Blank();
                Console.Write("Lv. "); Paint(p.Level); Blank();
                Console.Write(p.Name); Console.Write(" ( 전사 )"); Blank();
                Console.Write("공격력 : "); Paint(p.Damage + p.ExtraDamage); Console.Write(" ("); Paint("+");Paint(p.ExtraDamage); Console.Write(")"); Blank();
                Console.Write("방어력 : "); Paint(p.Defense + p.ExtraDefense); Console.Write(" (");Paint("+"); Paint(p.ExtraDefense); Console.Write(")"); Blank();
                Console.Write("체 력 : "); Paint(p.HealthPoint); Console.Write("/"); Paint(p.MaxHealth, 0); Blank();
                Console.Write("Gold : "); Paint(p.Gold);Paint(" G"); Blank();
                Blank();
                ExitText();
            }

            // 인벤토리 관련
            public void Inventory(Character player, Item weapon, Item armor)
            {
                Paint("인벤토리"); Blank();
                Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.");
                Blank();
                Console.WriteLine("[아이템 목록]");
                InventoryShow(weapon, player);
                InventoryShow(armor, player);
                Blank();
                Paint(1); Console.WriteLine(". 장착 관리");
                ExitText();
            }
            public void InventorySelect(Character player, Item weapon, Item armor)
            {
                Paint("인벤토리 - 장착 관리"); Blank();
                Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.");
                Blank();
                Console.WriteLine("[아이템 목록]");
                InventoryShow(weapon, player, 1);
                InventoryShow(armor, player, 1);
                Blank();
                ExitText();
            }

            // 인벤토리 보여주기
            void InventoryShow(Item item, Character player)
            {
                List<int> a = new List<int>();
                player.GiveInventory(item, ref a);
                switch (item.itemPool)
                {
                    case 1:
                        if (a.Count != 0)
                        {
                            foreach (int i in a)
                            {
                                if (player.WeaponEquip == i) { Paint("[E]"); }
                                ShowItem(item, i);
                            }
                        }
                        else { Paint("공격 무기가 없습니다!"); Blank(); }
                        break;
                    case 2:
                        if (a.Count != 0)
                        {
                            foreach (int i in a)
                            {
                                if (player.ArmorEquip == i) { Paint("[E]"); }
                                ShowItem(item, i);
                            }
                        }
                        else { Paint("방어구가 없습니다!"); Blank(); }
                        break;
                }
            }
            void InventoryShow(Item item, Character player, int nothing)
            {
                List<int> a = new List<int>();
                player.GiveInventory(item, ref a);
                switch (item.itemPool)
                {
                    case 1:
                        if (a.Count != 0)
                        {
                            for (int i = 0; i < a.Count; i++)
                            {
                                Paint(1); Space(); Paint(i); Space();
                                Paint("- ");
                                if (player.WeaponEquip == a[i]) { Paint("[E]"); }
                                ShowItem(item, a[i]);
                            }
                        }
                        else { Paint("공격 무기가 없습니다!"); Blank(); }
                        break;
                    case 2:
                        if (a.Count != 0)
                        {
                            for (int i = 0; i < a.Count; i++)
                            {
                                Paint(2); Space(); Paint(i); Space();
                                Paint("- ");
                                if (player.ArmorEquip == a[i]) { Paint("[E]"); }
                                ShowItem(item, a[i]);
                            }
                        }
                        else { Paint("방어구가 없습니다!"); Blank(); }
                        break;
                }
            }

            // 상점 관련
            public void Shop(Character player, Item weapon, Item armor)
            {
                Paint("상점"); Blank();
                Console.WriteLine("필요한 아이템을 얻을 수 있습니다.");
                Blank();
                Console.WriteLine("[보유 골드] "); 
                Paint(player.Gold); Paint(" G"); Blank();
                Blank();
                Console.WriteLine("[아이템 목록]");

                ShowItem(weapon);
                Blank();
                ShowItem(armor);

                Blank();
                Paint(1); Console.WriteLine(". 아이템 구매");
                Paint(2); Console.WriteLine(". 아이템 판매");
                ExitText();
            }
            public void ShopBuy(Character player, Item weapon, Item armor)
            {
                Paint("상점 - 아이템 구매"); Blank();
                Console.WriteLine("필요한 아이템을 얻을 수 있습니다.");
                Blank();
                Console.WriteLine("[보유 골드] "); 
                Paint(player.Gold); Paint(" G"); Blank();
                Blank();
                Console.WriteLine("[아이템 목록]");

                ShowShop(weapon, player);
                Blank();
                ShowShop(armor, player);
                Blank();
                Paint(2); Console.WriteLine(". 아이템 판매");
                Blank();
                ExitText();
            }
            public void ShopSell(Character player, Item weapon, Item armor)
            {
                Paint("상점 - 아이템 판매"); Blank();
                Console.WriteLine("어떤 아이템을 파시겠습니까?.");
                Blank();
                Console.WriteLine("[보유 골드] ");
                Paint(player.Gold); Paint(" G"); Blank();
                Console.WriteLine("[아이템 목록]");
                InventoryShow(weapon, player, 1);
                InventoryShow(armor, player, 1);
                Blank();
                Paint(1); Console.WriteLine(". 아이템 구매");
                Blank();
                ExitText();
            }

            void Dungeon(Character player, Item weapon, Item armor)
            {
                Paint("던전입장"); Blank();
                Console.WriteLine("이곳에서 던전으로 들어가기 전 활동을 할 수 있습니다.");
                Blank();
                Paint("[현재 장비]"); Blank();
                if (player.WeaponEquip != -1) { Console.Write("무기"); Tab(); Paint("| "); Paint(weapon.arrStr[0, player.WeaponEquip], false); Blank(); }
                else { Paint("-- 현재 장비하고 있는 무기 없음 --", 0); Blank(); }
                if (player.ArmorEquip != -1) { Console.Write("방어구"); Tab(); Paint("| "); Paint(armor.arrStr[0, player.ArmorEquip], false); Blank(); }
                else { Paint("-- 현재 장비하고 있는 방어구 없음 --", 0); Blank();}
                Blank();
                Paint(1); Console.Write(". 쉬운 던전"); Tab(); Paint("| "); Console.Write("방어력 ");       Paint(5); Console.Write(" 이상 권장"); Blank();
                Paint(2); Console.Write(". 일반 던전"); Tab(); Paint("| "); Console.Write("방어력 ");      Paint(11); Console.Write(" 이상 권장"); Blank();
                Paint(3); Console.Write(". 어려운 던전"); Tab(); Paint("| "); Console.Write("방어력 ");     Paint(17); Console.Write(" 이상 권장"); Blank();
                ExitText();
            }
            void DungeonClear(Character player, int damaged, int price, string Text)
            {
                Paint("던전 클리어"); Blank();
                Console.Write("축하합니다"); Paint("!!"); Blank();
                Console.WriteLine($"{Text} 던전을 클리어하셨습니다.");
                Blank();
                Console.WriteLine($"[탐험 결과]");
                Console.Write($"체력"); Tab(); Paint(player.HealthPoint); Paint(" -> "); Paint(player.HealthPoint - damaged); Blank();
                Console.Write($"Gold"); Tab(); Paint(player.Gold); Paint(" -> "); Paint(player.Gold + price); Paint(" G"); Blank();
                Blank();
                ExitText();
            }
            void DungeonFalse(Character player, string Text)
            {
                Paint("던전 클리어"); Blank();
                Console.WriteLine($"{Text} 던전을 클리어에 실패했습니다!");
                Blank();
                Console.WriteLine($"[탐험 결과]");
                Console.Write($"체력"); Tab(); Paint(player.HealthPoint); Paint(" -> "); Paint(Convert.ToInt32(player.HealthPoint * 0.5)); Blank();
                Blank();
                ExitText();
            }


            // 여관
            void Rest(Character player)
            {
                Paint("휴식하기"); Blank();
                Paint(500); Paint(" G "); Console.Write("를 내면 체력을 회복할 수 있습니다. (보유 골드 : "); Paint(player.Gold); Paint(" G"); Console.Write(" )"); Blank();
                Blank();
                Paint("현재 체력 : "); Paint(player.HealthPoint); Paint($"/{player.MaxHealth}"); Blank();
                Blank();
                Paint(1); Console.WriteLine(". 휴식하기");
                ExitText();
            }



            // 아이템 보여주기
            void ShowItem(Item item)  // 전부 공개
            {
                int a = 0; int b = 0; string c = null; string d = null;
                for (int i = 0; i < item.arrStr.GetLength(1); i++)
                {
                    item.GiveValue(i, ref a, ref b, ref c, ref d);
                    Paint("-"); Console.Write($" {c}\t");
                    Paint("|"); Console.Write($" {item.itemText} "); Paint("+"); Paint(a); Tab();
                    Paint("|"); Console.Write($" {d}\t\t");
                    Paint("| "); Paint(b); Paint(" G");
                    Blank();
                }
            }
             void ShowItem(Item item, int i) // 단일 공개
            {
                int a = 0; int b = 0; string c = null; string d = null;
                item.GiveValue(i, ref a, ref b, ref c, ref d);
                Console.Write($"{c}\t");
                Paint("|"); Console.Write($" {item.itemText} "); Paint("+"); Paint(a); Tab();
                Paint("|"); Console.Write($" {d}\t\t");
                Paint("| "); Paint(b); Paint(" G");
                Blank();
            }

            // 구매 화면
            void ShowShop(Item item, Character player)
            {
                int[,] a = item.arrInt;
                List<int> b = new List<int>();
                player.GiveInventory(item, ref b);
                switch (item.itemPool)
                {
                    case 1:
                        for (int i = 0; i < a.GetLength(1); i++)
                        {
                            if (b.Contains(i)) { Paint("-----이미 소지중은 아이템입니다-----", 1); Blank(); }
                            else 
                            { 
                                Paint(1); Space(); Paint(i); Space();
                                Paint("- ");
                                ShowItem(item, i);
                            }
                        }
                        break;
                    case 2:
                        for (int i = 0; i < a.GetLength(1); i++)
                        {
                            if (b.Contains(i)) { Paint("-----이미 소지중은 아이템입니다-----", 1); Blank(); }
                            else
                            {
                                Paint(2); Space(); Paint(i); Space();
                                Paint("- ");
                                ShowItem(item, i);
                            }
                        }
                        break;
                }
            }

            // 씬 매니저 전용 텍스트 출력
            void ExitText()
            {
                Paint(0); Console.WriteLine(". 나가기");
            }
        }

        // 캐릭터 정보
        class Character
        {
            public string Name { get; }
            public int level;
            public int Level
            {
                get { return level; }
                private set { level = value; }
            }
            public int experience;
            public int Experience
            {
                get { return experience; }
                private set 
                { 
                    experience = value;
                    if (experience >= level)
                    {
                        Blank();
                        Paint("레벨이 올랐습니다!"); Blank();
                        level += 1;
                        experience = 0;
                        defense += 1;
                        damage += 1;
                    }
                }    
            }

            private int damage;
            public int Damage
            {
                get { return damage; }
                private set { damage = value; }
            }
            int extraDamage = 0;
            public int ExtraDamage
            {
                get { return extraDamage; }
                private set { extraDamage = value; }
            }
            private int defense;
            public int Defense 
            {
                get { return defense; }
                private set { defense = value; }
            }
            int extraDefense = 0;
            public int ExtraDefense
            {
                get { return extraDefense; }
                private set { extraDefense = value; }
            }
            private int healthPoint;
            private int maxHealth;
            public int MaxHealth
            {
                get { return maxHealth; }
            }

            public int HealthPoint 
            {
                get { return healthPoint; }
                private set 
                { 
                    healthPoint = value; 
                    if ( healthPoint > maxHealth )
                    {
                        healthPoint = maxHealth;
                    }
                }
            }
            private int gold;
                public int Gold
            {
                get { return gold; }
                private set { gold = value; }
            }

            private List<int> weaponInventory = new List<int>();
            private List<int> armorInventory = new List<int>();

            private int weaponEquip;
            public int WeaponEquip 
            {  
                get { return weaponEquip; }
                private set { weaponEquip = value; }
            }
            private int armorEquip;
            public int ArmorEquip
            {
                get { return armorEquip; }
                private set { armorEquip = value; }
            }

            public Character()
            {
                Name = "Rtan";
                Level = 1;
                Damage = 10;
                Defense = 5;
                maxHealth = 100; // 순서 조심해야한다.
                HealthPoint = 100;
                Gold = 1500;

                weaponEquip = -1;
                armorEquip = -1;

            }

            public Character(string str)
            {
                Name = str;
                Level = 1;
                Damage = 10;
                Defense = 5;
                maxHealth = 100;
                HealthPoint = 100;
                Gold = 1500;

                weaponEquip = -1;
                armorEquip = -1;
            }

            public void InventoryLength(ref int i, ref int j)
            {
                i = weaponInventory.Count;
                j = armorInventory.Count;
            }

            public void GiveInventory(Item item, ref List<int> a)
            {
                switch(item.itemPool)
                {
                    case 1:
                        a = weaponInventory;
                        break;
                    case 2:
                        a = armorInventory;
                        break;
                }
            }

            public void Equip(int pool, int select, Item item)
            {
                
                switch (pool)
                {
                    case 1:
                        if (weaponEquip == weaponInventory[select])
                        {
                            weaponEquip = -1;
                            ExtraDamage = 0;
                            return;
                        }
                        weaponEquip = weaponInventory[select];
                        ExtraDamage = item.GiveValue(weaponInventory[select]);
                        break;
                    case 2:
                        if (armorEquip == armorInventory[select])
                        {
                            armorEquip = -1;
                            ExtraDefense = 0;
                            return;
                        }
                        armorEquip = armorInventory[select];
                        ExtraDefense = item.GiveValue(armorInventory[select]);
                        break;
                }
            }

            public void BuyItem(int price, int pool, int select)
            {
                switch (pool)
                {
                    case 1:
                        if (weaponInventory.Contains(select)) { Paint("이미 소지중입니다!", ""); Blank(); return; }
                        break;
                    case 2:
                        if (armorInventory.Contains(select)) { Paint("이미 소지중입니다!", ""); Blank(); return; }
                        break;
                }
                if (price <= Gold)
                {
                    switch (pool)
                    {
                        case 1:
                            Paint("구매완료!"); Blank();
                            Gold -= price;
                            weaponInventory.Add(select);
                            break;
                        case 2:
                            Paint("구매완료!"); Blank();
                            Gold -= price;
                            armorInventory.Add(select);
                            break;
                    }
                }
                else
                {
                    Paint("돈이 부족합니다!", ""); Blank();
                }
           
            }

            public void SellItem(int price, int pool, int idx)
            {
                switch(pool)
                {
                    case 1:
                        weaponInventory.RemoveAt(idx);
                        gold += Convert.ToInt32(price * 0.85f);
                        break;
                    case 2:
                        armorInventory.RemoveAt(idx);
                        gold += Convert.ToInt32(price * 0.85f);
                        break;
                }
                Paint("판매완료!"); Blank();
            }
            public void UseGold(int price)
            {
                Gold -= price;
            }
            public void HealthUp(int hp)
            {
                HealthPoint += hp;
            }

            public void ExperienceUp()
            {
                Experience += 1;
            }
        }

        // 아이템 사전
        class Item
        {
            public int itemPool { get; }
            public int[,] arrInt { get; }
            public string[,] arrStr { get; }

            public string itemText { get; }
            public Item()
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

            public Item(int j)
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
            public int GivePrice(int idx)
            {
                return arrInt[1, idx];
            }
            public int GiveValue(int idx)
            {
                return arrInt[0, idx];
            }
            public void GiveValue(int idx, ref int a, ref int b, ref string c, ref string d)
            {
                a = arrInt[0, idx];
                b = arrInt[1, idx];
                c = arrStr[0, idx];
                d = arrStr[1, idx];
            }



        }

        // 밑에 애들은 텍스트 출력 도우미들
        public static void Paint(int num)
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.Write(num);
            Console.ResetColor();
        }
        public static void Paint(int num, int Y)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write(num);
            Console.ResetColor();
        }
        public static void Paint(string text)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write(text);
            Console.ResetColor();
        }
        public static void Paint(string text, int gray)
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write(text);
            Console.ResetColor();
        }
        public static void Paint(string text, string red)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(text);
            Console.ResetColor();
        }
        public static void Paint(string text, bool magenta)
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.Write(text);
            Console.ResetColor();
        }
        public static void Blank()
        {
            Console.WriteLine();
        }
        public static void Tab()
        {
            Console.Write("\t");
        }
        public static void Space()
        {
            Console.Write(" ");
        }

        static void Main(string[] args)
        {
            int sceneNumber = 0;    // 0 마을 1 상태창 2 인벤토리 3 상점 
            OutPutText output = new OutPutText();
            Character player = new Character();
            Item weapon = new Item();
            Item armor = new Item(0);

            for (; ;)
            {
                sceneNumber = output.SceneManager(sceneNumber, ref player, weapon, armor);
            }
        }
    }
}
