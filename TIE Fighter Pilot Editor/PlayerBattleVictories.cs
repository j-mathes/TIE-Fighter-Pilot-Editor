using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TIE_Fighter_Pilot_Editor
{
    public class PlayerBattleVictories
    {
        public NPCShipType VictoryNPC { get; set; }
        public ushort Victories { get; set; }

        public PlayerBattleVictories (NPCShipType victoryNPC)
        {
            VictoryNPC = victoryNPC;
            Victories = 0;
        }

        public PlayerBattleVictories(byte[] bytes, NPCShipType victoryNPC, int victoriesOffset)
        {
            VictoryNPC = victoryNPC;
            Victories = BitConverter.ToUInt16(bytes, victoriesOffset);
        }

        //Not really used, but here for completeness sake.  BattleStats implements its own WriteData that takes care of this.
        public void WriteData(byte[] bytes, int victoriesOffset)
        {
            Victories.CopyToByteArrayLE(bytes, victoriesOffset);
        }
    }
}
