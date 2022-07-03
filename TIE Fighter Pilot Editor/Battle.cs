using System;
using System.Collections.Generic;

namespace TIE_Fighter_Pilot_Editor
{
    public class Battle
    {
        public BattleStatus Status { get; set; }
        public int LastMissionCompleted { get; set; }
        public List<Mission> MissionsList { get; set; }
        public uint TotalScore { get; set; }
        public byte SecondaryObjectivesCompleted { get; set;}
        public byte BonusObjectivesCompleted { get; set; }

        int MissionScoreOffset;

        public Battle(int numberOfMissions)
        {
            Status = 0;
            LastMissionCompleted = 0;
            MissionsList = new List<Mission>();
            TotalScore = 0;
            SecondaryObjectivesCompleted = 0;
            BonusObjectivesCompleted = 0;

            MissionsList.Clear();

            for (int i = 0; i < numberOfMissions; i++)
            {
                MissionsList.Add(new Mission(i+1));
            }
        }

        public Battle(byte[] bytes, int numberOfMissions, int statusOffset, int lastMissionCompletedOffset, int firstMissionScoreOffset, int secondaryObjectiveOffset, int secretObjectiveOffset)
        {
            Status = (BattleStatus)bytes[statusOffset];
            LastMissionCompleted = bytes[lastMissionCompletedOffset];
            MissionsList = new List<Mission>();
            MissionScoreOffset = firstMissionScoreOffset;
            SecondaryObjectivesCompleted = bytes[secondaryObjectiveOffset];
            BonusObjectivesCompleted = bytes[secretObjectiveOffset];

            for (int i = 0; i < numberOfMissions; i++)
            {
                MissionsList.Add(new Mission(bytes, MissionScoreOffset, i + 1));
                MissionsList[i].MissionScore = BitConverter.ToUInt32(bytes, MissionScoreOffset);
                TotalScore += MissionsList[i].MissionScore;
                MissionScoreOffset = MissionScoreOffset + 4;
            }
        }

        public void UpdateBattleTotalScore(int numberOfMissions)
        {
            TotalScore = 0;

            for (int i = 0; i < numberOfMissions; i++)
            {
                TotalScore += MissionsList[i].MissionScore;
            }
        }

        public void WriteData(byte[] bytes, int numberOfMissions, int statusOffset, int lastMissionCompletedOffset, int firstMissionScoreOffset, int secondaryObjectiveOffset, int secretObjectiveOffset)
        {
            bytes[statusOffset] = (byte)Status;
            bytes[lastMissionCompletedOffset] = (byte)LastMissionCompleted;
            bytes[secondaryObjectiveOffset] = SecondaryObjectivesCompleted;
            bytes[secretObjectiveOffset] = BonusObjectivesCompleted;

            MissionScoreOffset = firstMissionScoreOffset;

            for (int i = 0; i < numberOfMissions; i++)
            {
                MissionsList[i].MissionScore.CopyToByteArrayLE(bytes, MissionScoreOffset);
                MissionScoreOffset = MissionScoreOffset + 4;
            }
        }
    }
}
