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

    public Transform Crosshair;
    public Vector3 BulletDirectionNormalized;
    public Camera Cam;
    private void Awake()
    {
        Cam = Camera.main;
    }
    private void Start()
    {
        InstantiatePoolObjects();
    }

    private void Update()
    {
        SetAimToCrosshair();
    }

    private void SetAimToCrosshair()
    {
        Ray RayOrigin = Cam.ScreenPointToRay(Crosshair.position);
        RaycastHit hit;
        if (Physics.Raycast(RayOrigin, out hit))
        {
            if (hit.collider != null)
            {
                Vector3 direction = hit.point - bulletInitialPoint.transform.position;
                BulletDirectionNormalized = direction.normalized;
                Debug.DrawRay(bulletInitialPoint.transform.position, direction, Color.red, Time.deltaTime);
            }
        }
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
            bullet.SetPositionAndRotation(bulletInitialPoint.transform.position, bulletInitialPoint.transform.localRotation);
            bullet.BulletDirection = BulletDirectionNormalized;
            bullet.Gun = this;

            bullet.gameObject.SetActive(true);
        }
    }

    public void AddBulletToPool(GameObject bullet)
    {
        bullet.SetActive(false);
        bullet.transform.SetParent(PoolTransform);
        pooledObjects.Add(bullet);
    }
}
