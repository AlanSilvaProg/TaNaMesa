using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameStates
{
    LOAD,
    MENU,
    SINGLE,
    MULTI
}

public class GameState : MonoBehaviour {

    public static GameStates actualGameState;

}
