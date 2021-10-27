using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    private TapToPlayUI tapToPlayUI;

    private void Awake()
    {
        tapToPlayUI = GetComponentInChildren<TapToPlayUI>();
    }
}
