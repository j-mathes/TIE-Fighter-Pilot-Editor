using System;

namespace TIE_Fighter_Pilot_Editor
{
    public enum Health
    {
        Alive,
        Captured,
        Killed
    }

    public enum Rank
    {
        Cadet,
        Officer,
        Lieutenant,
        Captain,
        Commander,
        General
    }

    public enum Difficulty
    {
        Easy,
        Medium,
        Hard
    }

    public enum SecretOrder
    {
        None, 
        First_Initiate, 
        Second_Circle,
        Third_Circle,
        Fourth_CCircle,
        Inner_Circle, 
        Emperors_Hand,
        Emperors_Eyes,
        Emperors_Voice,
        Emperors_Reach
    }

    public enum FlyableShipType
    {
        TF,
        TI,
        TB,
        TA,
        GUN,
        TD,
        MB
    }

    public enum NPCShipType
    {                       // Kills offset  

        XW,         //660-661   1632-1633   X-Wing
        YW,         //662-663   1634-1635   Y-Wing
        AW,         //664-665   1636-1637   A-Wing
        BW,         //666-667   1638-1639   B-Wing
        TF,         //668-669   1640-1641   TIE Fighter
        TI,         //66A-66B   1642-1643   TIE Interceptor
        TB,         //66C-66D   1644-1645   TIE Bomber
        TA,         //66E-66F   1646-1647   TIE Advanced
        TD,         //670-671   1648-1649   TIE Defender
        TN1,        //672-673   1650-1651   TIE New 1
        TN2,        //674-675   1652-1653   TIE New 2
        MIS,        //676-677   1654-1655   Missile Boat
        TW,         //678-679   1656-1657   T-Wing
        Z95,        //67A-67B   1658-1659   Z-95 Headhunter
        R41,        //67C-67D   1660-1661   R-41 Starchaser
        GUN,        //67E-67F   1662-1663   Assault Gunboat
        SHU,        //680-681   1664-1665   Tyderian Shuttle
        ES,         //682-683   1666-1667   Escort Shuttle
        SPC,        //684-685   1668-1669   Patrol Craft
        SCT,        //686-687   1670-1671   Scout Craft
        TRN,        //688-689   1672-1673   Transport
        ATR,        //68A-68B   1674-1675   Assault Transport
        ETR,        //68C-68D   1676-1677   Escort Transport
        TUG,        //68E-68F   1678-1679   Tug
        CUV,        //690-691   1680-1681   Combat Utility Vehicle
        CNA,        //692-693   1682-1683   Container A
        CNB,        //694-695   1684-1685   Container B
        CNC,        //696-697   1686-1687   Container C
        CND,        //698-699   1688-1689   Container D
        HLF,        //69A-69B   1690-1691   Heavy Lifter
        BB,         //69C-69D   1692-1693   Bulk Barge
        FRT,        //69E-69F   1694-1695   Freighter
        CARG,       //6A0-6A1   1696-1697   Cargo Ferry
        CNVYR,      //6A2-6A3   1698-1699   Modular Conveyor
        CTRNS,      //6A4-6A5   1700-1701   Con Transport
        NF3,        //6A6-6A7   1702-1703   New Freighter 3
        MUTR,       //6A8-6A9   1704-1705   Murrian Transport
        CORT,       //6AA-6AB   1706-1707   Corellian Transport
        MILL,       //6AC-6AD   1708-1709   Millenium
        CRV,        //6AE-6AF   1710-1711   Corvette
        MCRV,       //6B0-6B1   1712-1713   Modified Corvette
        FRG,        //6B2-6B3   1714-1715   Nebulon-B Frigate
        MFRG,       //6B4-6B5   1716-1717   Modified Frigate
        LINER,      //6B6-6B7   1718-1719   C-3 Passenger Liner
        CRKC,       //6B8-6B9   1720-1721   Carrack Cruiser
        STRKC,      //6BA-6BB   1722-1723   Strike Cruiser
        ESC,        //6BC-6BD   1724-1725   Escort Carrier
        DREAD,      //6BE-6BF   1726-1727   Dreadnaught
        CRS,        //6C0-6C1   1728-1729   Calamari
        CLR,        //6C2-6C3   1730-1731   Light Calamari
        INT,        //6C4-6C5   1732-1733   Interdictor
        VSD,        //6C6-6C7   1734-1735   Victory Class SD
        ISD,        //6C8-6C9   1736-1737   Star Destroyer
        SSD,        //6CA-6CB   1738-1739   Super Destroyer
        CNE,        //6CC-6CD   1740-1741   Container E
        CNF,        //6CE-6CF   1742-1743   Container F
        CNG,        //6D0-6D1   1744-1745   Container G
        CNH,        //6D2-6D3   1746-1747   Container H
        CNI,        //6D4-6D5   1748-1749   Container I
        PLT1,       //6D6-6D7   1750-1751   Platform Class A
        PLT2,       //6D8-6D9   1752-1753   Platform Class B
        PLT3,       //6DA-6DB   1754-1755   Platform Class C
        PLT4,       //6DC-6DD   1756-1757   Platform Class D
        PLT5,       //6DE-6DF   1758-1759   Platform Class E
        PLT6,       //6E0-6E1   1760-1761   Platform Class F
        RDFC,       //6E2-6E3   1762-1763   R&D Facility
        LASBAT,     //6E4-6E5   1764-1765   Laser Battery
        WLNCHR,     //6E6-6E7   1766-1767   Weapons Launcher
        FAC1        //6E8-6E9   1768-1769   X/7 Factory Station
    }

    public enum BattleStatus
    {
        Inactive,
        Active,
        Pending,
        Done
    }

    [Flags]
    public enum SecondaryObjectives
    {
        First   = 0b_0000_0000,  // 0
        Second  = 0b_0000_0001,  // 1
        Third   = 0b_0000_0010,  // 2
        Fourth  = 0b_0000_0100,  // 4
        Fifth   = 0b_0000_1000,  // 8
        Sixth   = 0b_0001_0000,  // 16
        Seventh = 0b_0010_0000,  // 32
        Eighth  = 0b_0100_0000   // 64
    }

    [Flags]
    public enum BonusObjectives
    {
        First   = 0b_0000_0000,  // 0
        Second  = 0b_0000_0001,  // 1
        Third   = 0b_0000_0010,  // 2
        Fourth  = 0b_0000_0100,  // 4
        Fifth   = 0b_0000_1000,  // 8
        Sixth   = 0b_0001_0000,  // 16
        Seventh = 0b_0010_0000,  // 32
        Eighth  = 0b_0100_0000   // 64
    }
}
