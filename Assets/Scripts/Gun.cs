using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : GunBase
{
    [SerializeField] private GunType gunType = GunType.Single;
    [SerializeField] private Transform bulletInitialPoint;
    private ShootSign shootSign;

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
        shootSign = FindObjectOfType<ShootSign>();
    }
    private void Start()
    {
        InstantiatePoolObjects();
    }

    private void Update()
    {
        SetAimToCrosshair();
        if (Input.GetKeyDown(KeyCode.V))
        {
            SwitchFireType();        
        }
    }

    private void SwitchFireType()
    {
        switch (gunType)
        {
            case GunType.Single:
                gunType = GunType.Burst;
                break;
            case GunType.Burst:
                gunType = GunType.Single;
                break;
            default:
                break;
        }
    }

    RaycastHit hit;
    private void SetAimToCrosshair()
    {
        Ray RayOrigin = Cam.ScreenPointToRay(Crosshair.position);
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
                StartCoroutine(FireBurstBullet());
                break;
            default:
                break;
        }
    }

    private void SetBulletFeatures()
    {
        if (BulletFeaturePanel.Instance.isRedActive)
        {
            //Material deðiþtir
        }
        if (BulletFeaturePanel.Instance.isBigToggleActive)
        {
            //Scale arttýr
        }
        if (BulletFeaturePanel.Instance.isExplosiveActive)
        {
            //Collision olduktan 1 sn sonra yok et
        }
    }

    public void FireSingleBullet()
    {
        
        if (pooledObjects.Count != 0)
        {
            Bullet bullet = pooledObjects[0].GetComponent<Bullet>();
            pooledObjects.RemoveAt(0);
            SetBulletFeatures();
            bullet.SetPositionAndRotation(bulletInitialPoint.transform.position, bulletInitialPoint.transform.localRotation);
            bullet.BulletDirection = BulletDirectionNormalized;
            bullet.Gun = this;
            shootSign.ShootedBullet++;
            bullet.gameObject.SetActive(true);
        }
    }

    private IEnumerator FireBurstBullet()
    {
        if (pooledObjects.Count != 0)
        {
            for (int i = 0; i < 3; i++)
            {
                Bullet bullet = pooledObjects[0].GetComponent<Bullet>();
                pooledObjects.RemoveAt(0);
                SetBulletFeatures();
                bullet.SetPositionAndRotation(bulletInitialPoint.transform.position, bulletInitialPoint.transform.localRotation);
                Vector3 deflectionRate = Random.insideUnitSphere;
                bullet.BulletDirection = ((hit.point + ((i == 0) ? Vector3.zero : new Vector3(deflectionRate.x, deflectionRate.y, 0))) - bulletInitialPoint.transform.position);
                bullet.Gun = this;
                shootSign.ShootedBullet++;
                bullet.gameObject.SetActive(true);

                yield return new WaitForSeconds(0.1f);
            }
        }
    }

    public void AddBulletToPool(GameObject bullet)
    {
        bullet.SetActive(false);
        bullet.transform.SetParent(PoolTransform);
        pooledObjects.Add(bullet);
    }
}
