using UnityEngine;
using System.Collections;

public class CreateTowerOnClick : MonoBehaviour
{
    public GameObject tower;

    void Clicked(Vector3 position)
    {
        Instantiate(tower, position + Vector3.up * 0.5f, tower.transform.rotation);
    }
}