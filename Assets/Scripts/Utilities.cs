using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utilities 
{
    public enum AITankState {
        Idle,       // dung yen
        Chasing,    // duoi theo
        Attacking,  // tan cong
        Fleeing     // bo chay
    } 

    public enum LevelResult
    {
        Win,
        Lose,
        Unknow
    }
    
    public enum LevelType
    {
        PVP,
        PVE
    }

}
