using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TIE_Fighter_Pilot_Editor
{
    public class Mission
    {
        public int Num { get; set; }
        public uint MissionScore { get; set; }
        
        public Mission(int missionNumber)
        {
            Num = missionNumber;
            MissionScore = 0;
        }

        public Mission(byte[] bytes, int missionScoreOffset, int missionNumber)
        {
            MissionScore = BitConverter.ToUInt32(bytes, missionScoreOffset);
            Num = missionNumber;
        }

        public void WriteData(byte[] bytes, int missionScoreOffset)
        {
            MissionScore.CopyToByteArrayLE(bytes, missionScoreOffset);
        }

    }
}
