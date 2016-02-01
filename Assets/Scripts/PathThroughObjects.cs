using UnityEngine;
using System.Collections;

public class PathThroughObjects : MonoBehaviour
{
    public GameObject[] pathPoints;
    public float speed = 10f;
    int currentPathIndex;
    Vector3 movementDirection;
    //public float goalDistance = 0.1f;
    float lastDistance = float.MaxValue;

    // Use this for initialization
    void Start()
    {
        currentPathIndex = 0;
        UpdateDirection();
    }

    // Update is called once per frame
    void Update()
    {
        // where we are


        //movement code
        transform.position += movementDirection * speed * Time.deltaTime;

        // here: first, we move towards goal (distance decreasing), once distance increasea again, we are past goal and should change
        // direction
        // alternative: use OnTriggerEnter
        var sqrDistance = (pathPoints[currentPathIndex].transform.position - transform.position).sqrMagnitude;
        if (sqrDistance > lastDistance)
        {
            if (currentPathIndex < pathPoints.Length - 1)
            {
                currentPathIndex++;
                UpdateDirection();
                lastDistance = float.MaxValue;
            }
            else
                movementDirection = Vector3.zero;
        }
        else
            lastDistance = sqrDistance;
    }

    private void UpdateDirection()
    {
        // where we are going
        movementDirection = (pathPoints[currentPathIndex].transform.position - transform.position).normalized;
    }
}