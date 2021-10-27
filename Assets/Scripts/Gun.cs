using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : GunBase
{
    [SerializeField] private GunType gunType = GunType.Single;
    [SerializeField] private Transform bulletInitialPoint;
    public GameObject BulletPrefab;

    public Transform PoolTransform;
    public int PoolAmount = 20;
    public List<GameObject> pooledObjects;

    private void Awake()
    {
        
    }
    private void Start()
    {
        InstantiatePoolObjects();
    }

    private void Update()
    {
        
    }
    public void InstantiatePoolObjects()
    {
        pooledObjects = new List<GameObject>();
        GameObject temp;
        for (int i = 0; i < PoolAmount; i++)
        {
            temp = Instantiate(BulletPrefab);
            AddBulletToPool(temp);
        }
    }
    public override void Fire()
    {
        Debug.Log("Fire");
        switch (gunType)
        {
            case GunType.Single:
                FireSingleBullet();
                break;
            case GunType.Burst:
                break;
            default:
                break;
        }
    }
    public void FireSingleBullet()
    {
        if(pooledObjects.Count != 0)
        {
            Bullet bullet = pooledObjects[0].GetComponent<Bullet>();
            pooledObjects.RemoveAt(0);
            bullet.transform.SetParent(null);
            bullet.transform.position = bulletInitialPoint.transform.position;
            bullet.transform.rotation = bulletInitialPoint.transform.rotation;
            bullet.Gun = this;

            bullet.gameObject.SetActive(true);
            bullet.Fired = true;
        }
    }

    public void AddBulletToPool(GameObject bullet)
    {
        bullet.SetActive(false);
        bullet.transform.SetParent(PoolTransform);
        pooledObjects.Add(bullet);
    }
}
