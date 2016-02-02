using UnityEngine;
using System.Collections;

public class SlowBullet : MonoBehaviour
{
    public float slowPercentage = 0.7f;
    public float slowTime = 2f;
    RestoreSpeed restoreSpeedObject;

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
            //PathThroughObjects path = collision.gameObject.GetComponent<PathThroughObjects>();
            //path.speed *= slowPercentage;
            //restoreSpeedObject = new RestoreSpeed(); // Instantiate< RestoreSpeed>() as RestoreSpeed;
            restoreSpeedObject = collision.gameObject.GetComponent<RestoreSpeed>();
            GameObject slowDownEffect = null;
            if (restoreSpeedObject == null)
            {
                restoreSpeedObject = collision.gameObject.AddComponent<RestoreSpeed>();
                slowDownEffect = Instantiate(Resources.Load("SlowDownEffect"), collision.gameObject.transform.position, Quaternion.identity) as GameObject;
                slowDownEffect.transform.parent = collision.gameObject.transform;
            }
            restoreSpeedObject.Begin(slowPercentage, slowTime, slowDownEffect);
            Destroy(gameObject);
        }
    }
}

class RestoreSpeed : MonoBehaviour
{
    float originalSpeed;
    PathThroughObjects path;
    //float time = 1f;
    GameObject slowDownEffect;

    int calledCount = 0;

    public void Begin(float percentage, float time, GameObject slowDownEffect)
    {
        Invoke("Restore", time);
        if (calledCount == 0)
        {
            path = gameObject.GetComponent<PathThroughObjects>();
            originalSpeed = path.speed;
            path.speed *= percentage;
        }
        if (slowDownEffect != null)
            this.slowDownEffect = slowDownEffect;
        calledCount++;
    }

    void Restore()
    {
        calledCount--;
        if (calledCount == 0)
        {
            path.speed = originalSpeed;
            Destroy(gameObject.GetComponent<RestoreSpeed>());
            //Destroy(gameObject);
            Destroy(slowDownEffect);
        }
    }
}