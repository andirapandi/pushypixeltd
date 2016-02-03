using UnityEngine;
using System.Collections;

public class Mortar : MonoBehaviour
{
    public GameObject explosion;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    //GameObject curExplosion;
    void OnCollisionEnter(Collision collision)
    {
        //curExplosion = 
        Instantiate(explosion, collision.contacts[0].point, Quaternion.identity); // as GameObject;
        Destroy(gameObject);
        //gameObject.SetActive(false); // keep bullet alive to keep Coroutine running
        //GetComponent<MeshRenderer>().enabled = false;
        //GetComponent<SphereCollider>().enabled = false;
        //transform.parent = curExplosion.transform;
        //StartCoroutine("ScaleOverTime");
    }
}