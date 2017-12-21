using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TIE_Fighter_Pilot_Editor
{
    public class TrainingRecord
    {
        public FlyableShipType TrainingShipType { get; set; }
        public int NextTrainingLevel { get; set; }
        public uint TrainingScore { get; set; }
        public int TrainingLevelCompleted { get; set; }

        public TrainingRecord(FlyableShipType trainingShipType)
        {
            TrainingShipType = trainingShipType;
            NextTrainingLevel = 0;
            TrainingScore = 0;
            TrainingLevelCompleted = 0;
        }

        public TrainingRecord(FlyableShipType trainingShipType, byte[] bytes, int nextTrainingLevelOffset, int trainingScoreOffset, int trainingCompletedOffset)
        {
            TrainingShipType = trainingShipType;
            NextTrainingLevel = bytes[nextTrainingLevelOffset];
            TrainingScore = BitConverter.ToUInt32(bytes, trainingScoreOffset);
            TrainingLevelCompleted = bytes[trainingCompletedOffset];
        }

        public void WriteData(FlyableShipType playerShip, byte[] bytes)
        {
            switch (playerShip)
            {
                case FlyableShipType.TF:
                    bytes[29] = (byte)NextTrainingLevel;
                    bytes[90] = (byte)TrainingLevelCompleted;
                    TrainingScore.CopyToByteArrayLE(bytes, 42);
                    break;
                case FlyableShipType.TI:
                    bytes[30] = (byte)NextTrainingLevel;
                    bytes[91] = (byte)TrainingLevelCompleted;
                    TrainingScore.CopyToByteArrayLE(bytes, 46);
                    break;
                case FlyableShipType.TB:
                    bytes[31] = (byte)NextTrainingLevel;
                    bytes[92] = (byte)TrainingLevelCompleted;
                    TrainingScore.CopyToByteArrayLE(bytes, 50);
                    break;
                case FlyableShipType.TA:
                    bytes[32] = (byte)NextTrainingLevel;
                    bytes[93] = (byte)TrainingLevelCompleted;
                    TrainingScore.CopyToByteArrayLE(bytes, 54);
                    break;
                case FlyableShipType.GUN:
                    bytes[33] = (byte)NextTrainingLevel;
                    bytes[94] = (byte)TrainingLevelCompleted;
                    TrainingScore.CopyToByteArrayLE(bytes, 58);
                    break;
                case FlyableShipType.TD:
                    bytes[34] = (byte)NextTrainingLevel;
                    bytes[95] = (byte)TrainingLevelCompleted;
                    TrainingScore.CopyToByteArrayLE(bytes, 62);
                    break;
                case FlyableShipType.MB:
                    bytes[35] = (byte)NextTrainingLevel;
                    bytes[96] = (byte)TrainingLevelCompleted;
                    TrainingScore.CopyToByteArrayLE(bytes, 66);
                    break;
            }
        }
    }
}
