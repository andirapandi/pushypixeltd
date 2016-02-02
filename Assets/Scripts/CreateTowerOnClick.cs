using UnityEngine;
using System.Collections;

public class CreateTowerOnClick : MonoBehaviour
{
    public TowerSelector towerSelector;

    void Clicked(Vector3 position)
    {
        var tower = towerSelector.GetSelectedTower();
        Instantiate(tower, position + Vector3.up * 0.5f, tower.transform.rotation);
    }
}