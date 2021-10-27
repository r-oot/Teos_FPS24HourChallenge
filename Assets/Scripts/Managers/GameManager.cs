using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    private GlobalVariables.GameStates gameState;

    public GlobalVariables.GameStates GameState
    {
        get => gameState;
        set => gameState = value;
    }

    private void Awake()
    {
        GameState = GlobalVariables.GameStates.InGame; //TODO: UI taptoplay eklendiðinde TapToPlay'e deðiþecek.
    }
}
