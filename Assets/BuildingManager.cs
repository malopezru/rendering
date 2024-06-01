using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BuildingManager : MonoBehaviour
{
    public GameObject[] objects;
    public GameObject pendingObject;
    [SerializeField] private Material[] materials;
    private Vector3 pos;
    private RaycastHit hit;
    [SerializeField] private LayerMask layerMask;

    const float gridSize = 0.5f;
    public bool canPlace = true;
    public int wood;
    public int money;
    public TextMeshProUGUI woodText;
    public TextMeshProUGUI moneyText;
    public GameObject errorPanel;
    public TextMeshProUGUI errorText;

    private void Start()
    {
        wood = 100;
        money = 5000;
    }

    void Update()
    {
        woodText.text = "Wood: " + wood.ToString();
        moneyText.text = "Money: " + money.ToString();

        if (pendingObject != null)
        {
            pendingObject.transform.position = new Vector3(RoundToNearestGrid(pos.x), RoundToNearestGrid(pos.y), RoundToNearestGrid(pos.z));

            if (Input.GetMouseButtonDown(0) && canPlace)
            {
                PlaceObject();
            }
            UpdateMaterials();
        }
    }

    public void PlaceObject()
    {
        pendingObject.GetComponent<MeshRenderer>().material = materials[2];
        pendingObject = null;
    }

    private void FixedUpdate()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, 1000, layerMask))
        {
            pos = hit.point;
            pos.y = pos.y - 0.5f;
        }
    }

    public void SelectObject(int index)
    {
        if (index == 0 && money >= 1500 && wood >= 100)
        {
            money -= 1500;
            wood -= 100;
            pendingObject = Instantiate(objects[index], pos, transform.rotation);
        }
        else if (index == 1 && money >= 3000 && wood >= 200)
        {
            money -= 3000;
            wood -= 200;
            pendingObject = Instantiate(objects[index], new Vector3(pos.x, pos.y - 0.5f, pos.z), Quaternion.Euler(transform.rotation.x, transform.rotation.y + 270, transform.rotation.z));
        }
        else if (index == 2 && money >= 10000 && wood >= 300)
        {
            money -= 10000;
            wood -= 300;
            pendingObject = Instantiate(objects[index], pos, transform.rotation);
        }
        else
        {
            errorPanel.SetActive(true);
            errorText.text = "Not enough resources";
        }
    }

    float RoundToNearestGrid(float pos)
    {
        float xDiff = pos % gridSize;
        pos -= xDiff;

        if (xDiff < (gridSize / 2))
        {
            pos += gridSize;
        }
        return pos;
    }

    void UpdateMaterials()
    {
        if (canPlace && pendingObject)
        {
            pendingObject.GetComponent<MeshRenderer>().material = materials[0];
        }
        else if (!canPlace  && pendingObject)
        {
            pendingObject.GetComponent<MeshRenderer>().material = materials[1];
        }
    }
}
