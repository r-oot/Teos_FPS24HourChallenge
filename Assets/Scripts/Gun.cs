using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : GunBase
{
    [SerializeField] private GunType gunType = GunType.Single;
    private ShootSign shootSign;

    public int BulletPoolAmount = 30;
    public List<GameObject> pooledObjects;

    public Transform Crosshair;
    private void Awake()
    {
        base.Cam = Camera.main;
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

    public override void SwitchFireType()
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
            Vector3 direction;
            if (hit.collider != null)
            {
                direction = hit.point - base.BulletInitialPoint.transform.position;
                base.BulletDirectionNormalized = direction.normalized;
                Debug.DrawRay(base.BulletInitialPoint.transform.position, direction, Color.red, Time.deltaTime);
            }
        }
    }

    private void InstantiatePoolObjects()
    {
        pooledObjects = new List<GameObject>();
        GameObject temp;
        for (int i = 0; i < BulletPoolAmount; i++)
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

    private void FireSingleBullet()
    {
        
        if (pooledObjects.Count != 0)
        {
            Bullet bullet = pooledObjects[0].GetComponent<Bullet>();
            pooledObjects.RemoveAt(0);
            SetBulletFeatures();
            bullet.SetPositionAndRotation(base.BulletInitialPoint.transform.position, base.BulletInitialPoint.transform.localRotation);
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
                bullet.SetPositionAndRotation(base.BulletInitialPoint.transform.position, base.BulletInitialPoint.transform.localRotation);
                Vector3 deflectionRate = Random.insideUnitSphere;
                bullet.BulletDirection = ((hit.point + ((i == 0) ? Vector3.zero : new Vector3(deflectionRate.x, deflectionRate.y, 0))) - base.BulletInitialPoint.transform.position);
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
