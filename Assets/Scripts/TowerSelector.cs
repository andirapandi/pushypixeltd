using UnityEngine;
using System.Collections;

public class TowerSelector : MonoBehaviour
{
    public GameObject[] towerIcons;
    public GameObject[] towers;

    public float towerIconRotateRate = 90f;

    int selectedTower = 0;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        towerIcons[selectedTower].transform.Rotate(Vector3.up * Time.deltaTime * towerIconRotateRate);
    }

    public GameObject GetSelectedTower()
    {
        return towers[selectedTower];
    }

    public void SetSelectedTower(GameObject inputTower)
    {
        var index = 0;
        foreach (var towerIcon in towerIcons)
        {
            if (inputTower == towerIcon)
                selectedTower = index;
            index++;
        }
    }
}