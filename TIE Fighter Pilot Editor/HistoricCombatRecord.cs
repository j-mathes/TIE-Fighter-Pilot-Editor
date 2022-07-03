using System;
using System.Collections.Generic;

namespace TIE_Fighter_Pilot_Editor
{
    public class HistoricCombatRecord
    {
        public FlyableShipType MissionType { get; set; }
        public List<bool> MissionComplete { get; set; }
        public List<uint> MissionScore { get; set; }

        public HistoricCombatRecord(FlyableShipType missionType, int numberOfMissions)
        {
            MissionType = missionType;
            MissionComplete = new List<bool>(numberOfMissions);
            MissionScore = new List<uint>(numberOfMissions);

            for (int i = 0; i < numberOfMissions; i++)
            {
                MissionComplete.Add(false);
                MissionScore.Add(0);
            }
        }

        public HistoricCombatRecord(FlyableShipType missionType, byte[] bytes, int numberOfMissions, int missionCompletedOffset, int missionScoreOffset)
        {
            MissionType = missionType;
            MissionComplete = new List<bool>(numberOfMissions);
            MissionScore = new List<uint>(numberOfMissions);

            for (int i = 0; i < numberOfMissions; i++)
            {
                MissionComplete.Add(Convert.ToBoolean(bytes[missionCompletedOffset + i]));
                MissionScore.Add(BitConverter.ToUInt32(bytes, missionScoreOffset + (i * 4)));
            }
        }

        public void WriteData(byte[] bytes, int numberOfMissions, int missionCompletedOffset, int missionScoreOffset)
        {
            for (int i = 0; i < numberOfMissions; i++)
            {
                bytes[missionCompletedOffset] = Convert.ToByte(MissionComplete[i]);
                MissionScore[i].CopyToByteArrayLE(bytes, missionScoreOffset);
                missionCompletedOffset++;
                missionScoreOffset += 4;
            }
        }
    }
}
