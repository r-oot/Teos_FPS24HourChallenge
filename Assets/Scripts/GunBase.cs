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
    public abstract void Fire();

}
