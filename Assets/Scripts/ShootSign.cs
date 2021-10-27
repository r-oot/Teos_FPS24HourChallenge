using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShootSign : MonoBehaviour
{
    private int shootedBullet;
    [SerializeField,Tooltip("Sign static Text")] 
    private string signText = "Shoot Count : ";
    private TextMeshPro signTMP;

    private void Awake()
    {
        signTMP = GetComponent<TextMeshPro>();
    }

    public int ShootedBullet
    {
        get => shootedBullet;
        set
        {
            shootedBullet += value;
            signTMP.text = signText + shootedBullet.ToString();
        }
    }
}
