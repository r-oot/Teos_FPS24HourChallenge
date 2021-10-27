using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public bool Fired;
    [SerializeField] private float BulletFlyTime;
    [SerializeField] private float BulletSpeed;
    private float timer;
    public Gun Gun;

    private void OnEnable()
    {
        timer = 0;
        transform.SetParent(null);
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
    }
    private void OnCollisionEnter(Collision collision)
    {
        DeactivateBullet();
    }
    public void SetPositionAndRotation(Vector3 newPos, Quaternion newRot)
    {
        transform.position = newPos;
        transform.localRotation = newRot;
        Fired = true;
        
    }
}
