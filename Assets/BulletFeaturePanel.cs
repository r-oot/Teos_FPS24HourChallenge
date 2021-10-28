using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletFeaturePanel : MonoBehaviour
{

    private static BulletFeaturePanel instance;
    public static BulletFeaturePanel Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<BulletFeaturePanel>();
                return instance;
            }
            return instance;
        }
    }

    public bool isExplosiveActive;
    public bool isBigToggleActive;
    public bool isRedActive;

    public void ExplosiveToggleChanged()
    {
        isExplosiveActive = !isExplosiveActive;
        Debug.Log("ExplosiveToggleChanged");
    }

    public void BigToggleChanged()
    {
        isBigToggleActive = !isBigToggleActive;
        Debug.Log("BigToggleChanged");
    }

    public void RedToggleChanged()
    {
        isRedActive = !isRedActive;
        Debug.Log("RedToggleChanged");
    }

}