using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class TowerBuildingButtons : MonoBehaviour
{
    public GameObject arrowTowerPrefab;
    public Button buildArrowTowerButton;

    public GameObject gatlingTowerPrefab;
    public Button buildGatlingTowerButton;

    public GameObject explosiveTowerPrefab;
    public Button buildExplosiveTowerButton;

    public Transform interactiveGrid;
    public Button sellButton;

    private GameObject currentTower;


    private void Start()
    {
        buildArrowTowerButton.onClick.AddListener(() => OnBuildArrowTowerButtonClicked());
        buildGatlingTowerButton.onClick.AddListener(() => OnBuildGatlingTowerButtonClicked());
        buildExplosiveTowerButton.onClick.AddListener(() => OnBuildExplosiveTowerButtonClicked());
        sellButton.onClick.AddListener(() => OnSellCurrentTowerButtonClicked());
    }

    private void OnBuildArrowTowerButtonClicked()
    {
        if (currentTower != null)
        {
            Debug.LogWarning("This grid is OCCUPIED!");
        }
        else
        {
            Debug.Log("Arrow Tower is BUILT!");
            currentTower = Instantiate(arrowTowerPrefab, interactiveGrid.position, Quaternion.identity);
        }
    }

    private void OnBuildGatlingTowerButtonClicked()
    {
        if (currentTower != null)
        {
            Debug.LogWarning("This grid is OCCUPIED!");
        }
        else
        {
            Debug.Log("Gatling Tower is BUILT!");
            currentTower = Instantiate(gatlingTowerPrefab, interactiveGrid.position, Quaternion.identity);
        }
    }
    private void OnBuildExplosiveTowerButtonClicked()
    {
        if (currentTower != null)
        {
            Debug.LogWarning("This grid is OCCUPIED!");
        }
        else
        {
            Debug.Log("Explosive Tower is BUILT!");
            currentTower = Instantiate(explosiveTowerPrefab, interactiveGrid.position, Quaternion.identity);
        }
    }

    private void OnSellCurrentTowerButtonClicked()
    {
        if (currentTower != null)
        {
            Debug.Log("Tower SOLD!");
            Destroy(currentTower);
        }
        else
        {
            Debug.LogWarning("NO TOWER FOUND");
        }
    }

    private void BuildTower(int x, int y)
    {

    }
    public void SetGridSize(int newGridSizeX, int newGridSizeY)
    {

    }
}
