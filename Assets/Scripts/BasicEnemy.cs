using UnityEngine;
using System.Collections;

public class BasicEnemy : MonoBehaviour
{
    public int health = 10;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    //void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.collider.tag == "Bullet")
    //    {
    //        health--;
    //        Destroy(collision.collider.gameObject);
    //        if (health <= 0)
    //        {
    //            Destroy(collision.collider.gameObject);
    //            Destroy(gameObject);
    //        }
    //    }
    //}

    public void Hit(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}