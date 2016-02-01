using UnityEngine;
using System.Collections;

public class BasicTower : MonoBehaviour
{

    public GameObject bullet;
    public float bulletSpeed = 1f;
    public float fireRate = 1f;
    public float lifeTime = 5f;
    // Use this for initialization
    void Start()
    {
        InvokeRepeating("SpawnBullet", 1 / fireRate, 1/fireRate);
    }

    void SpawnBullet()
    {
        var target = GameObject.FindGameObjectWithTag("Enemy");
        var newBullet = Instantiate(bullet, transform.position, bullet.transform.rotation) as GameObject;
        newBullet.GetComponent<Rigidbody>().AddForce((target.transform.position - transform.position).normalized * bulletSpeed, ForceMode.VelocityChange);
        Destroy(newBullet, lifeTime);
    }

    // Update is called once per frame
    void Update()
    {

    }
}