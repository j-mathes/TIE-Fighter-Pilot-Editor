using System.Collections.Generic;

namespace TIE_Fighter_Pilot_Editor
{
    public class ListOfBattles
    {
        public int ActiveBattle { get; set; }
        public List<Battle> BattlesList { get; set; }

        public ListOfBattles(int numberOfBattles)
        {
            ActiveBattle = 0;
            BattlesList = new List<Battle>();

            BattlesList.Clear();

            for (int i = 0; i < numberOfBattles; i++)
            {
                switch (i)
                {
                    case 0:
                        BattlesList.Add(new Battle(6));
                        break;
                    case 1:
                        BattlesList.Add(new Battle(5));
                        break;
                    case 2:
                        BattlesList.Add(new Battle(6));
                        break;
                    case 3:
                        BattlesList.Add(new Battle(5));
                        break;
                    case 4:
                        BattlesList.Add(new Battle(5));
                        break;
                    case 5:
                        BattlesList.Add(new Battle(4));
                        break;
                    case 6:
                        BattlesList.Add(new Battle(5));
                        break;
                    case 7:
                        BattlesList.Add(new Battle(6));
                        break;
                    case 8:
                        BattlesList.Add(new Battle(6));
                        break;
                    case 9:
                        BattlesList.Add(new Battle(6));
                        break;
                    case 10:
                        BattlesList.Add(new Battle(7));
                        break;
                    case 11:
                        BattlesList.Add(new Battle(7));
                        break;
                    case 12:
                        BattlesList.Add(new Battle(8));
                        break;
                }
            }
        }

        public ListOfBattles(byte[] bytes, int numberOfBattles, int activeBattleOffset)
        {
            ActiveBattle = bytes[activeBattleOffset];
            BattlesList = new List<Battle>();

            BattlesList.Clear();

            for (int i = 0; i < numberOfBattles; i++)
            {
                switch (i)
                {
                    case 0:
                        BattlesList.Add(new Battle(bytes, 6, 617, 637, 986, 913, 933));
                        break;
                    case 1:
                        BattlesList.Add(new Battle(bytes, 5, 618, 638, 1018, 914, 934));
                        break;
                    case 2:
                        BattlesList.Add(new Battle(bytes, 6, 619, 639, 1050, 915, 935));
                        break;
                    case 3:
                        BattlesList.Add(new Battle(bytes, 5, 620, 640, 1082, 916, 936));
                        break;
                    case 4:
                        BattlesList.Add(new Battle(bytes, 5, 621, 641, 1114, 917, 937));
                        break;
                    case 5:
                        BattlesList.Add(new Battle(bytes, 4, 622, 642, 1146, 918, 938));
                        break;
                    case 6:
                        BattlesList.Add(new Battle(bytes, 5, 623, 643, 1178, 919, 939));
                        break;
                    case 7:
                        BattlesList.Add(new Battle(bytes, 6, 624, 644, 1210, 920, 940));
                        break;
                    case 8:
                        BattlesList.Add(new Battle(bytes, 6, 625, 645, 1242, 921, 941));
                        break;
                    case 9:
                        BattlesList.Add(new Battle(bytes, 6, 626, 646, 1274, 922, 942));
                        break;
                    case 10:
                        BattlesList.Add(new Battle(bytes, 7, 627, 647, 1306, 923, 943));
                        break;
                    case 11:
                        BattlesList.Add(new Battle(bytes, 7, 628, 648, 1338, 924, 944));
                        break;
                    case 12:
                        BattlesList.Add(new Battle(bytes, 8, 629, 649, 1370, 925, 945));
                        break;
                }
            }
        }

        public void WriteData(byte[] bytes, int numberOfBattles, int activeBattleOffset)
        {
            bytes[activeBattleOffset] = (byte)ActiveBattle;

            for (int i = 0; i < numberOfBattles; i++)
            {
                switch (i)
                {
                    case 0:
                        BattlesList[i].WriteData(bytes, 6, 617, 637, 986, 913, 933);
                        break;
                    case 1:
                        BattlesList[i].WriteData(bytes, 5, 618, 638, 1018, 914, 934);
                        break;
                    case 2:
                        BattlesList[i].WriteData(bytes, 6, 619, 639, 1050, 915, 935);
                        break;
                    case 3:
                        BattlesList[i].WriteData(bytes, 5, 620, 640, 1082, 916, 936);
                        break;
                    case 4:
                        BattlesList[i].WriteData(bytes, 5, 621, 641, 1114, 917, 937);
                        break;
                    case 5:
                        BattlesList[i].WriteData(bytes, 4, 622, 642, 1146, 918, 938);
                        break;
                    case 6:
                        BattlesList[i].WriteData(bytes, 5, 623, 643, 1178, 919, 939);
                        break;
                    case 7:
                        BattlesList[i].WriteData(bytes, 6, 624, 644, 1210, 920, 940);
                        break;
                    case 8:
                        BattlesList[i].WriteData(bytes, 6, 625, 645, 1242, 921, 941);
                        break;
                    case 9:
                        BattlesList[i].WriteData(bytes, 6, 626, 646, 1274, 922, 942);
                        break;
                    case 10:
                        BattlesList[i].WriteData(bytes, 7, 627, 647, 1306, 923, 943);
                        break;
                    case 11:
                        BattlesList[i].WriteData(bytes, 7, 628, 648, 1338, 924, 944);
                        break;
                    case 12:
                        BattlesList[i].WriteData(bytes, 8, 629, 649, 1370, 925, 945);
                        break;
                }
            }
        }
    }
}
