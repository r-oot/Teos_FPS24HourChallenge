using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public bool Fired;
    [SerializeField] private float BulletFlyTime;
    [SerializeField] private float BulletSpeed;
    private float timer;
    private Vector3 bulletPrevPos;
    public Vector3 BulletDirection;
    public Gun Gun;
    public GameObject Crater;
    private void OnEnable()
    {
        timer = 0;
        transform.SetParent(null);
        transform.rotation = Quaternion.LookRotation(BulletDirection);
    }

    private void Update()
    {
        if (Fired)
        {
            bulletPrevPos = transform.position;
            transform.Translate(Vector3.forward * Time.deltaTime * BulletSpeed);
            CheckBulletCollisionWRay();
            CheckFlyTime();
        }
    }

    private void CheckBulletCollisionWRay()
    {
        RaycastHit hit;
        if (Physics.Raycast(bulletPrevPos,(transform.position - bulletPrevPos).normalized,out hit,(transform.position - bulletPrevPos).magnitude))
        {
            Instantiate(Crater, hit.point, Quaternion.identity);
            DeactivateBullet();
        }
    }

    private void CheckFlyTime()
    {
        timer += Time.deltaTime;
        if (timer >= BulletFlyTime)
        {
            DeactivateBullet();
        }
    }
    private void DeactivateBullet()
    {
        if (!Fired)
            return;
        Fired = false;
        Gun.AddBulletToPool(this.gameObject);
        //ToDo: bulletýn özelliklerini normale çevir.
    }
    public void SetPositionAndRotation(Vector3 newPos, Quaternion newRot)
    {
        transform.position = newPos;
        transform.localRotation = newRot;
        Fired = true;
        
    }
}
