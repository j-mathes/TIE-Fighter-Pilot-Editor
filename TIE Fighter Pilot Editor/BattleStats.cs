using System;
using System.Collections.Generic;

namespace TIE_Fighter_Pilot_Editor
{
    public class BattleStats
    {
        public List<PlayerBattleVictories> BattleVictoriesList { get; set; }
        public uint LasersFired { get; set; }
        public uint LaserCraftHits { get; set; }
        public ushort WarheadsFired { get; set; }
        public ushort WarheadHits { get; set; }
        public ushort CraftLost { get; set; }
        public ushort TotalKills { get; set; }
        public ushort TotalCaptures { get; set; }

        public BattleStats()
        {
            BattleVictoriesList = new List<PlayerBattleVictories>();
            LasersFired = 0;
            LaserCraftHits = 0;
            WarheadsFired = 0;
            WarheadHits = 0;
            CraftLost = 0;
            TotalKills = 0;
            TotalCaptures = 0;

            BattleVictoriesList.Clear();

            foreach (NPCShipType npcShip in Enum.GetValues(typeof(NPCShipType)))
            {
                BattleVictoriesList.Add(new PlayerBattleVictories(npcShip));
            }
        }

        public BattleStats(byte[] bytes)
        {
            BattleVictoriesList = new List<PlayerBattleVictories>();
            LasersFired = BitConverter.ToUInt32(bytes, 1908);
            LaserCraftHits = BitConverter.ToUInt32(bytes, 1912);
            WarheadsFired = BitConverter.ToUInt16(bytes, 1920);
            WarheadHits = BitConverter.ToUInt16(bytes, 1922);
            CraftLost = BitConverter.ToUInt16(bytes, 1926);
            TotalKills = BitConverter.ToUInt16(bytes, 1626);
            TotalCaptures = BitConverter.ToUInt16(bytes, 1628);

            BattleVictoriesList.Clear();
            int firstKillsOffset = 1632; 
            //int firstCapturesOffset = xxxx;
            int countMultiplier = 0;

            foreach (NPCShipType npcShip in Enum.GetValues(typeof(NPCShipType)))
            {
                BattleVictoriesList.Add(new PlayerBattleVictories(bytes, npcShip, firstKillsOffset + countMultiplier));
                countMultiplier += 2;
            }
        }

        public void WriteData(byte[] bytes)
        {
            LasersFired.CopyToByteArrayLE(bytes, 1908);
            LaserCraftHits.CopyToByteArrayLE(bytes, 1912);
            WarheadsFired.CopyToByteArrayLE(bytes, 1920);
            WarheadHits.CopyToByteArrayLE(bytes, 1922);
            CraftLost.CopyToByteArrayLE(bytes, 1926);
            TotalKills.CopyToByteArrayLE(bytes, 1626);
            TotalCaptures.CopyToByteArrayLE(bytes, 1628);

            int firstKillsOffset = 1632;
            int countMultiplier = 0;

            foreach (NPCShipType npcShip in Enum.GetValues(typeof(NPCShipType)))
            {
                BattleVictoriesList[(int)npcShip].Victories.CopyToByteArrayLE(bytes, firstKillsOffset + countMultiplier);
                countMultiplier += 2;
            }
        }
    }
}
