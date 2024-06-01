using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingResources : MonoBehaviour
{
    private BuildingManager buildingManager;
    private float timer = 5.0f;
    [SerializeField] int moneyGenerated;

    void Start()
    {
        buildingManager = GameObject.Find("BuildingManager").GetComponent<BuildingManager>();
    }

    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            buildingManager.money += moneyGenerated;
            timer = 5.0f;
        }
    }
}
