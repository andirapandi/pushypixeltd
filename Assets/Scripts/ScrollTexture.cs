using UnityEngine;
using System.Collections;

public class ScrollTexture : MonoBehaviour
{
    public float xScrollRate = 0f;
    public float yScrollRate = -1f;

    // Use this for initialization
    void Start()
    {
        GetComponent<Renderer>().material.mainTextureOffset = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        var material = GetComponent<Renderer>().material;
        Vector2 newOffset = material.mainTextureOffset;
        newOffset.x += xScrollRate * Time.deltaTime;
        newOffset.y += yScrollRate * Time.deltaTime;
        material.mainTextureOffset = newOffset;
    }
}