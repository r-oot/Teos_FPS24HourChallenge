using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{

    public Transform TapToPlayUITransform;
    public InGameUI InGameUI;

    public void SetStatusTapToPlayUI(bool value)
    {
        TapToPlayUITransform.gameObject.SetActive(value);
    }
    public void SetStatusInGameUI(bool value)
    {
        InGameUI.gameObject.SetActive(value);
    }

    public void ButtonTapToPlayClicked()
    {
        SetStatusTapToPlayUI(false);
        SetStatusInGameUI(true);
        GameManager.Instance.GameState = GlobalVariables.GameStates.InGame;
    }
}
