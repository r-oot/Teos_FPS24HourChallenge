using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GunBase : MonoBehaviour
{
    public enum GunType 
    { 
        Single,
        Burst
    }
    public Transform BulletInitialPoint;
    public GameObject BulletPrefab;
    public Transform PoolTransform;
    public Vector3 BulletDirectionNormalized;
    public Camera Cam;

    public abstract void Fire();

    public virtual void SwitchFireType()
    {

    }

}
