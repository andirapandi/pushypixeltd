using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using System.Linq;

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

    IEnumerable<Transform> getPathTransforms ()
    {
        yield return this.transform;
        foreach (var t in this.pathPoints)
            yield return t.transform;
    }

    private void CreatePath3DRepresentation()
    {
        var list = getPathTransforms().ToList();
        for (var i = 1; i < list.Count; ++i)
        {
            var last = list[i - 1];
            var cur = list[i];
            // Create object between transform.position and first waypoint
            var directionVector = cur.position - last.position;
            var pathObjectPosition = directionVector / 2 + last.position;
            var pathObjectOrientation = Quaternion.LookRotation(cur.position - last.position);
            var pathObject = Instantiate(pathWayIcon, pathObjectPosition, pathObjectOrientation) as GameObject;
            var geometryScale = Vector3.one;
            geometryScale.z = directionVector.magnitude;
            pathObject.transform.localScale = geometryScale;
            var textureScale = Vector2.one;
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
            {
                //movementDirection = Vector3.zero;
                Destroy(gameObject);
                // add logic to deduct player health
            }
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