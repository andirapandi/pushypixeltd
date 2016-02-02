using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] pathPoints;
    public GameObject pathWayIcon;

    public GameObject[] spawnList;
    public float spawnTime;
    int spawnIndex = 0;

    IEnumerable<Transform> getPathTransforms()
    {
        //yield return this.transform;
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

    // Use this for initialization
    void Start()
    {
        CreatePath3DRepresentation();
        InvokeRepeating("Spawn", spawnTime, spawnTime);
    }

    void Spawn()
    {
        // spawn next enemy in spawn list
        var enemy = Instantiate(spawnList[spawnIndex]) as GameObject;

        enemy.transform.position = this.pathPoints[0].transform.position; // - 0.1f * (this.pathPoints[1].transform.position - this.pathPoints[0].transform.position);

        // Set enemy path information
        enemy.GetComponent<PathThroughObjects>().pathPoints = this.pathPoints.ToList().GetRange(1, this.pathPoints.Length-2).ToArray();

        // or
        // reference.SendMessage("SetPathPoints", this.pathPoints);

        spawnIndex++;
        if (spawnIndex >= spawnList.Length)
            //spawnIndex = 0;
            CancelInvoke();
    }

    // Update is called once per frame
    void Update()
    {

    }
}