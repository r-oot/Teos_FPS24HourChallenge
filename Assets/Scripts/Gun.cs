using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : GunBase
{
    [SerializeField] private GunType gunType = GunType.Single;
    [SerializeField] private Transform bulletInitialPoint;

    private void Awake()
    {
        
    }
    private void Start()
    {
        
    }

    private void Update()
    {
        
    }
    public override void Fire()
    {
        throw new System.NotImplementedException();
    }
}
