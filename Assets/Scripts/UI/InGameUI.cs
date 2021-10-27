using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameUI : MonoBehaviour
{
    public Transform Crosshair;

    public void SetStatusCrosshair(bool value)
    {
        Crosshair.gameObject.SetActive(value);
    }
}
