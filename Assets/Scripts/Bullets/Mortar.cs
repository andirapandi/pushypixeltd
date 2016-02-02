using UnityEngine;
using System.Collections;

public class Mortar : MonoBehaviour
{
    public GameObject explosion;

    public float explosionDuration = .4f;
    public float initialScale = 0.5f;
    public float endScale = 3f;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    GameObject curExplosion;
    void OnCollisionEnter(Collision collision)
    {
        curExplosion = Instantiate(explosion, collision.contacts[0].point, Quaternion.identity) as GameObject;
        //Destroy(gameObject);
        //gameObject.SetActive(false); // keep bullet alive to keep Coroutine running
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<SphereCollider>().enabled = false;
        transform.parent = curExplosion.transform;
        StartCoroutine("ScaleOverTime");
    }

    IEnumerator ScaleOverTime()
    {
        float t = 0;
        while (t <= explosionDuration)
        {
            t += Time.deltaTime;
            float scale = Mathf.Lerp(initialScale, endScale, t / explosionDuration);
            curExplosion.transform.localScale = new Vector3(scale, scale, scale);
            yield return null;
        }
        Destroy(curExplosion);
        Destroy(gameObject);
    }
}