using UnityEngine;
using System.Collections;

public class MortarTower : MonoBehaviour
{

    public GameObject bullet;
    //public float bulletSpeed = 1f;
    public float fireRate = .3f;
    public float lifeTime = 8f;
    public float fireRadius = 8f;
    public float lobAmount = 10f;

    // Use this for initialization
    void Start()
    {
        InvokeRepeating("SpawnBullet", 1 / fireRate, 1 / fireRate);
    }

    void SpawnBullet()
    {
        GameObject target = null; // = GameObject.FindGameObjectWithTag("Enemy");
        foreach (Collider col in Physics.OverlapSphere(transform.position, fireRadius))
        {
            if (col.tag == "Enemy")
            {
                target = col.gameObject;
                break;
            }
        }
        if (target == null)
            return;
        var newBullet = Instantiate(bullet, transform.position, bullet.transform.rotation) as GameObject;

        var verticalForce = Vector3.up * lobAmount;
        var horizontalForce = (target.transform.position - transform.position).normalized;
        float speed = lobAmount / -Physics.gravity.y;

        newBullet.GetComponent<Rigidbody>().AddForce((verticalForce + horizontalForce) * speed, ForceMode.VelocityChange);
        //Destroy(newBullet, lifeTime);
    }
}