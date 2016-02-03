using UnityEngine;
using System.Collections;

class Explosion : MonoBehaviour
{
    public float explosionDuration = .3f;
    public float initialScale = 0.5f;
    public float endScale = 3.5f;

    void Start()
    {
        gameObject.GetComponent<SphereCollider>().enabled = false;
        StartCoroutine("ScaleOverTime");
    }

    IEnumerator ScaleOverTime()
    {
        float t = 0;
        while (t <= explosionDuration)
        {
            t += Time.deltaTime;
            float scale = Mathf.Lerp(initialScale, endScale, t / explosionDuration);
            transform.localScale = new Vector3(scale, scale, scale);
            yield return null;
        }
        var sc = gameObject.GetComponent<SphereCollider>();
        sc.enabled = true;
        //sc.transform.localScale = Vector3.one * endScale;
        //sc.isTrigger = true;
        //for (int i = 0; i < 2; ++i)
        yield return null; // give time for ontrigger
        //yield return null; // give time for ontrigger
        Destroy(gameObject);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Enemy")
        {
            var enemy = collision.collider.gameObject.GetComponent<BasicEnemy>();
            enemy.Hit(5); // 13);
            //Destroy(gameObject);
        }
    }
}