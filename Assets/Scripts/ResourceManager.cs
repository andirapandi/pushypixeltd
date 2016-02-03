using UnityEngine;
using System.Collections;
using System;

public class ResourceManager : MonoBehaviour
{
    public int initialEnergy = 500;
    public GameObject energyDisplay;

    public static int energy;

    // Use this for initialization
    void Start()
    {
        energy = initialEnergy;
    }

    // Update is called once per frame
    void Update()
    {
        energyDisplay.GetComponent<TextMesh>().text = "Resources" + Environment.NewLine + energy.ToString();
    }
}