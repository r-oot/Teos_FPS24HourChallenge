using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : GunBase
{
    [SerializeField] private GunType gunType = GunType.Single;
    private ShootSign shootSign;
    [SerializeField, Range(2, 5)]
    private int BurstBulletCount;
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
    Collider col;
    private void SetAimToCrosshair()
    {
        Ray RayOrigin = Cam.ScreenPointToRay(Crosshair.position);
        col = null;
        if (Physics.Raycast(RayOrigin, out hit,50f))
        {
            Vector3 direction;
            if (hit.collider != null)
            {
                col = hit.collider;
                direction = hit.point - base.BulletInitialPoint.transform.position;
                base.BulletDirectionNormalized = direction.normalized;
                Debug.DrawRay(base.BulletInitialPoint.transform.position, direction, Color.red, Time.deltaTime);
            }
        }
        if(col == null) // havaya ateþ edildiyse
        {
            base.BulletDirectionNormalized = base.BulletInitialPoint.transform.forward;
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
        SetAimToCrosshair();
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
            //BulletPrefab.GetComponentInChildren<MeshRenderer>().material = redMat
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
            col = null;
        }
    }

    private IEnumerator FireBurstBullet()
    {
        if (pooledObjects.Count >= BurstBulletCount)
        {
            for (int i = 0; i < BurstBulletCount; i++)
            {
                Bullet bullet = pooledObjects[0].GetComponent<Bullet>();
                pooledObjects.RemoveAt(0);
                SetBulletFeatures();
                bullet.SetPositionAndRotation(base.BulletInitialPoint.transform.position, base.BulletInitialPoint.transform.localRotation);
                Vector3 deflectionRate = Random.insideUnitSphere;
                if (col != null)
                    bullet.BulletDirection = ((hit.point + ((i == 0) ? Vector3.zero : new Vector3(deflectionRate.x, deflectionRate.y, 0))) - base.BulletInitialPoint.transform.position);
                else
                    bullet.BulletDirection = BulletDirectionNormalized;
                bullet.Gun = this;
                shootSign.ShootedBullet++;
                bullet.gameObject.SetActive(true);

                yield return new WaitForSeconds(0.1f);
            }
            col = null;
        }
    }

    public void AddBulletToPool(GameObject bullet)
    {
        bullet.SetActive(false);
        bullet.transform.SetParent(PoolTransform);
        pooledObjects.Add(bullet);
    }
}
