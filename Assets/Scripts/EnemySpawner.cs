using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class EnemySpawner : MonoBehaviour
{
    GameObject[] pathPoints;
    public GameObject pathWayIcon;

    public GameObject[] spawnList;
    public float spawnTime;
    public float timeBeforeStart = 5f;
    int spawnIndex = 0;

    IEnumerable<Transform> getPathTransforms()
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

    // Use this for initialization
    void Start()
    {
        //List<Transform> children
        GetPathPointsFromHierarchy();
        //var test = transform as IEnumerable;
        //List<Transform> children2 = (transform as IEnumerable).Cast<Transform>().ToList();
        //var test2 = transform as IEnumerable<Transform>;
        //var test3 = transform as IEnumerable<Component>;
        //var test4 = transform.GetEnumerator();
        //var test5 = transform as IEnumerable<object>;
        ////pathPoints = test.Select(t => ((Transform)t).gameObject).ToArray();
        //foreach (var t in test)
        //{
        //    var x = t;
        //}
        //foreach (var t in transform)
        //{
        //    var x = t;
        //}
        //pathPoints = transform.GetEnumerator() (transform as IEnumerable<Transform>).Select(t => t.gameObject).ToArray(); //.Select(t => t).ToArray(); // GetComponents< GameObject>().Select(component => component.gameObject).ToArray();
        CreatePath3DRepresentation();
        InvokeRepeating("Spawn", timeBeforeStart, spawnTime);
    }

    private void GetPathPointsFromHierarchy()
    {
        pathPoints = transform.Cast<Transform>().Select(t => t.gameObject).ToArray();
    }

    void Spawn()
    {
        // spawn next enemy in spawn list
        var enemy = Instantiate(spawnList[spawnIndex]) as GameObject;

        enemy.transform.position = this.transform.position; // this.pathPoints[0].transform.position; // - 0.1f * (this.pathPoints[1].transform.position - this.pathPoints[0].transform.position);

        // Set enemy path information
        enemy.GetComponent<PathThroughObjects>().pathPoints = pathPoints; // this.pathPoints.ToList().GetRange(1, this.pathPoints.Length - 2).ToArray();

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

    #region gizmos (lines) drawn in editor, not at runtime
    void OnDrawGizmos()
    {
        GetPathPointsFromHierarchy();

        if (pathPoints == null || pathPoints.Length == 0)
            return;

        Gizmos.DrawLine(transform.position, pathPoints[0].transform.position);
        for (int i = 1; i < pathPoints.Length; i++)
            Gizmos.DrawLine(pathPoints[i - 1].transform.position, pathPoints[i].transform.position);
    }
    #endregion
}