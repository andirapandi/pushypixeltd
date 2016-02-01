using UnityEngine;
using System.Collections;
using System;

public class PathThroughObjects : MonoBehaviour
{
    public GameObject[] pathPoints;
    public float speed = 10f;
    int currentPathIndex;
    Vector3 movementDirection;
    //public float goalDistance = 0.1f;
    float lastDistance = float.MaxValue;

    public GameObject pathWayIcon;

    // Use this for initialization
    void Start()
    {
        currentPathIndex = 0;
        UpdateDirection();
        CreatePath3DRepresentation();
    }

    private void CreatePath3DRepresentation()
    {
        // Create object between transform.position and first waypoint
        var directionVector = pathPoints[0].transform.position - transform.position;
        var pathObjectPosition = directionVector / 2 + transform.position;
        var pathObjectOrientation = Quaternion.LookRotation(pathPoints[0].transform.position - transform.position);
        var pathObject = Instantiate(pathWayIcon, pathObjectPosition, pathObjectOrientation) as GameObject;
        var geometryScale = Vector3.one;
        geometryScale.z = directionVector.magnitude;
        pathObject.transform.localScale = geometryScale;
        var textureScale = Vector2.one;
        textureScale.y = Mathf.Ceil(directionVector.magnitude);
        pathObject.GetComponent<Renderer>().material.mainTextureScale = textureScale;

        for (var i = 1; i < pathPoints.Length; ++i)
        {
            directionVector = pathPoints[i].transform.position - pathPoints[i - 1].transform.position;
            pathObjectPosition = directionVector / 2 + pathPoints[i - 1].transform.position;
            pathObjectOrientation = Quaternion.LookRotation(pathPoints[i].transform.position - pathPoints[i - 1].transform.position);
            pathObject = Instantiate(pathWayIcon, pathObjectPosition, pathObjectOrientation) as GameObject;
            geometryScale = Vector3.one;
            geometryScale.z = directionVector.magnitude;
            pathObject.transform.localScale = geometryScale;
            textureScale = Vector2.one;
            textureScale.y = Mathf.Ceil(directionVector.magnitude);
            pathObject.GetComponent<Renderer>().material.mainTextureScale = textureScale;

        }
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