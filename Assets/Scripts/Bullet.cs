using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public bool Fired;
    [SerializeField] private float BulletFlyTime;
    [SerializeField] private float BulletSpeed;
    private float timer;
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
            transform.Translate(Vector3.forward * Time.deltaTime * BulletSpeed);
            CheckFlyTime();
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
    private void OnTriggerEnter(Collider other)
    {
        DeactivateBullet();
        Vector3 contactPoint = other.ClosestPoint(transform.position);
        Instantiate(Crater, contactPoint, Quaternion.identity);
        Debug.Log("Collided");
    }
    public void SetPositionAndRotation(Vector3 newPos, Quaternion newRot)
    {
        transform.position = newPos;
        transform.localRotation = newRot;
        Fired = true;
        
    }
}
