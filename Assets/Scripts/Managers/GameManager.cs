using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class GameManager : MonoBehaviour {

    private static GameManager instance;
    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<GameManager>();
                return instance;
            }
            return instance;
        }
    }

    private GlobalVariables.GameStates gameState;
    public GlobalVariables.GameStates GameState
    {
        get => gameState;
        set
        {
            switch (value)
            {
                case GlobalVariables.GameStates.TapToPlay:
                    break;
                case GlobalVariables.GameStates.InGame:
                    Cursor.lockState = CursorLockMode.Locked;
                    break;
                case GlobalVariables.GameStates.Settings:
                    Cursor.lockState = CursorLockMode.Confined;
                    break;
                case GlobalVariables.GameStates.Complete:
                    break;
                case GlobalVariables.GameStates.Fail:
                    break;
                default:
                    break;
            }
            gameState = value;
        }
    }

    private void Awake()
    {
        GameState = GlobalVariables.GameStates.TapToPlay;
        Application.targetFrameRate = 60;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            switch (Cursor.lockState)
            {
                case CursorLockMode.None:
                    break;
                case CursorLockMode.Locked:
                    Cursor.lockState = CursorLockMode.Confined;
                    break;
                case CursorLockMode.Confined:
                    Cursor.lockState = CursorLockMode.Locked;
                    break;
                default:
                    break;
            }
            
        }
    }
}
