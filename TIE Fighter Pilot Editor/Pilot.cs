using System;
using System.Collections.Generic;
using System.IO;

namespace TIE_Fighter_Pilot_Editor
{
    public class Pilot
    {
        // Pilot data fields

        public string PltName { get; set; }
        public Health PltHealth { get; set; }
        public Rank PltRank { get; set; }
        public Difficulty PltDifficulty { get; set; }
        public int PltBattleScore { get; set; }
        public uint PltSkillLevel { get; set; } //experience level - rookie to top ace
        public SecretOrder PltSecretOrderLevel { get; set; }

        public List<TrainingRecord> TrainingRecordList = new List<TrainingRecord>();
        public List<HistoricCombatRecord> HistoricCombatRecordList = new List<HistoricCombatRecord>();
        public BattleStats BattleStats;
        public ListOfBattles ListOfBattles;

        private int totalBattles = 13;

        public Pilot() // all values should be initialized here before a pilot file is loaded
        {
            PltName = "No Pilot File Loaded";
            PltHealth = Health.Alive;
            PltRank = Rank.Cadet;
            PltDifficulty = Difficulty.Easy;
            PltBattleScore = 0;
            PltSkillLevel = 0;
            PltSecretOrderLevel = 0;



            foreach (FlyableShipType playerShip in Enum.GetValues(typeof(FlyableShipType)))
            {
                TrainingRecordList.Add(new TrainingRecord(playerShip));
            }

            foreach (FlyableShipType missionType in Enum.GetValues(typeof(FlyableShipType)))
            {
                switch (missionType)
                {
                    case FlyableShipType.TF:
                        HistoricCombatRecordList.Add(new HistoricCombatRecord(missionType, 4));
                        break;
                    case FlyableShipType.TI:
                        HistoricCombatRecordList.Add(new HistoricCombatRecord(missionType, 4));
                        break;
                    case FlyableShipType.TB:
                        HistoricCombatRecordList.Add(new HistoricCombatRecord(missionType, 4));
                        break;
                    case FlyableShipType.TA:
                        HistoricCombatRecordList.Add(new HistoricCombatRecord(missionType, 4));
                        break;
                    case FlyableShipType.GUN:
                        HistoricCombatRecordList.Add(new HistoricCombatRecord(missionType, 4));
                        break;
                    case FlyableShipType.TD:
                        HistoricCombatRecordList.Add(new HistoricCombatRecord(missionType, 4));
                        break;
                    case FlyableShipType.MB:
                        HistoricCombatRecordList.Add(new HistoricCombatRecord(missionType, 4));
                        break;
                }
            }

            BattleStats = new BattleStats();
            ListOfBattles = new ListOfBattles(totalBattles);
        }

        public void GetData(string fileName, byte[] bytes)
        {
            PltName = Path.GetFileNameWithoutExtension(fileName);
            PltHealth = (Health)bytes[1];
            PltRank = (Rank)bytes[2];
            PltDifficulty = (Difficulty)bytes[3];
            PltBattleScore = BitConverter.ToInt32(bytes, 4);
            PltSkillLevel = BitConverter.ToUInt16(bytes, 8);
            PltSecretOrderLevel = (SecretOrder)bytes[10];

            int nextTrainingLevelOffset = 29;
            int trainingScoreOffset = 42;
            int trainingCompletedOffset = 90;
            int scoreMultiplier = 0;
            int completedMultuplier = 0;

            TrainingRecordList.Clear();

            foreach (FlyableShipType playerShip in Enum.GetValues(typeof(FlyableShipType)))
            {
                TrainingRecordList.Add(new TrainingRecord(playerShip, bytes, nextTrainingLevelOffset + completedMultuplier, trainingScoreOffset + scoreMultiplier, trainingCompletedOffset + completedMultuplier));
                scoreMultiplier += 4; // increments offset to read score
                completedMultuplier += 1; // increments offset to read how many training levels were completed
            }

            HistoricCombatRecordList.Clear();

            foreach (FlyableShipType missionType in Enum.GetValues(typeof(FlyableShipType)))
            {
                switch (missionType)
                {
                    case FlyableShipType.TF:
                        HistoricCombatRecordList.Add(new HistoricCombatRecord(missionType, bytes, 4, 520, 136));
                        break;
                    case FlyableShipType.TI:
                        HistoricCombatRecordList.Add(new HistoricCombatRecord(missionType, bytes, 4, 528, 168));
                        break;
                    case FlyableShipType.TB:
                        HistoricCombatRecordList.Add(new HistoricCombatRecord(missionType, bytes, 4, 536, 200));
                        break;
                    case FlyableShipType.TA:
                        HistoricCombatRecordList.Add(new HistoricCombatRecord(missionType, bytes, 4, 544, 232));
                        break;
                    case FlyableShipType.GUN:
                        HistoricCombatRecordList.Add(new HistoricCombatRecord(missionType, bytes, 4, 552, 264));
                        break;
                    case FlyableShipType.TD:
                        HistoricCombatRecordList.Add(new HistoricCombatRecord(missionType, bytes, 4, 560, 296));
                        break;
                    case FlyableShipType.MB:
                        HistoricCombatRecordList.Add(new HistoricCombatRecord(missionType, bytes, 4, 568, 328));
                        break;
                }
            }

            BattleStats = new BattleStats(bytes);
            ListOfBattles = new ListOfBattles(bytes, totalBattles, 616);
        }

        public void WriteData(string fileName, byte[] oldbytes)
        {
            byte[] bytes = oldbytes;

            // Write Pilot level data
            bytes[1] = (byte)PltHealth;
            bytes[2] = (byte)PltRank;
            bytes[3] = (byte)PltDifficulty;
            PltBattleScore.CopyToByteArrayLE(bytes, 4);
            PltSkillLevel.CopyToByteArrayLE(bytes, 8);
            bytes[10] = (byte)PltSecretOrderLevel;

            // Write Training Record data
            foreach (FlyableShipType playerShip in Enum.GetValues(typeof(FlyableShipType)))
            {
                TrainingRecordList[(int)playerShip].WriteData(playerShip, bytes); 
            }

            // Write Historic Combat Record data
            foreach (FlyableShipType missionType in Enum.GetValues(typeof(FlyableShipType)))
            {
                switch (missionType)
                {
                    case FlyableShipType.TF:
                        HistoricCombatRecordList[(int)missionType].WriteData(bytes, 4, 520, 136);
                        break;
                    case FlyableShipType.TI:
                        HistoricCombatRecordList[(int)missionType].WriteData(bytes, 4, 528, 168);
                        break;
                    case FlyableShipType.TB:
                        HistoricCombatRecordList[(int)missionType].WriteData(bytes, 4, 536, 200);
                        break;
                    case FlyableShipType.TA:
                        HistoricCombatRecordList[(int)missionType].WriteData(bytes, 4, 544, 232);
                        break;
                    case FlyableShipType.GUN:
                        HistoricCombatRecordList[(int)missionType].WriteData(bytes, 4, 552, 264);
                        break;
                    case FlyableShipType.TD:
                        HistoricCombatRecordList[(int)missionType].WriteData(bytes, 4, 560, 296);
                        break;
                    case FlyableShipType.MB:
                        HistoricCombatRecordList[(int)missionType].WriteData(bytes, 4, 568, 328);
                        break;
                }
            }

            // Write Battle Stats data
            BattleStats.WriteData(bytes);

            // Write Battle / Mission data
            ListOfBattles.WriteData(bytes, totalBattles, 616);
        }
    }
}

