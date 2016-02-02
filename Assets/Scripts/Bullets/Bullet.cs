using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour
{
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Enemy")
        {
            var enemy = collision.collider.gameObject.GetComponent<BasicEnemy>();
            enemy.Hit(1);
            Destroy(gameObject);
        }
    }
}