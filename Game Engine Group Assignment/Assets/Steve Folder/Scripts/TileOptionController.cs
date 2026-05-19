using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEngine;

public class TileInfoOptionController : MonoBehaviour
{

    // insert tower prefab here
    [SerializeField] private GameObject tower1;
    [SerializeField] private GameObject tower2;
    [SerializeField] private GameObject tower3;

    // set tower buy price here // inspector view have higher control
    [SerializeField] private int tower1BuyCost = 10;
    [SerializeField] private int tower2BuyCost = 14;
    [SerializeField] private int tower3BuyCost = 20;

    // set tower sell price here // inspector view have higher control
    [SerializeField] private int tower1SellCost = 5;
    [SerializeField] private int tower2SellCost = 7;
    [SerializeField] private int tower3SellCost = 10;

    private GameObject instantiatedTower;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }   

    public void onBuild1()
    {
        Transform tileInfo = transform.parent.parent;

        TileOption ti = tileInfo.GetComponent<TileOption>();



        if (!ti.isTowerInstantiate())
        {
            GameObject playerStatus = GameObject.FindGameObjectWithTag("PlayerStatus");
            PlayerStatus ps = playerStatus.GetComponent<PlayerStatus>();

            if (ps.getPlayerMoney() >= tower1BuyCost) {

                ps.towerBought(tower1BuyCost);
                ti.setTileinfo("Arrows");

                Debug.Log("on build1");


                instantiatedTower = Instantiate(tower1, ti.getTransform(), Quaternion.identity);
                ti.instantiateTowerModel(instantiatedTower);
                instantiatedTower.transform.position += new Vector3(0, 4, 0);
            }
        }
        else Debug.Log("Tile already occupied by a tower, sell a tower first");

    }
    public void onBuild2()
    {
        Transform tileInfo = transform.parent.parent;

        TileOption ti = tileInfo.GetComponent<TileOption>();

        if (!ti.isTowerInstantiate())
        {
            GameObject playerStatus = GameObject.FindGameObjectWithTag("PlayerStatus");
            PlayerStatus ps = playerStatus.GetComponent<PlayerStatus>();

            if (ps.getPlayerMoney() >= tower2BuyCost)
            {

                ps.towerBought(tower2BuyCost);
                ti.setTileinfo("Explosives");

                Debug.Log("on build2");

                instantiatedTower = Instantiate(tower2, ti.getTransform(), Quaternion.identity);
                ti.instantiateTowerModel(instantiatedTower);
                instantiatedTower.transform.position += new Vector3(0, 4, 0);
            }
        }
        else Debug.Log("Tile already occupied by a tower, sell a tower first");
    }

    public void onBuild3()
    {
        Transform tileInfo = transform.parent.parent;

        TileOption ti = tileInfo.GetComponent<TileOption>();


        if (!ti.isTowerInstantiate())
        {
            GameObject playerStatus = GameObject.FindGameObjectWithTag("PlayerStatus");
            PlayerStatus ps = playerStatus.GetComponent<PlayerStatus>();

            if (ps.getPlayerMoney() >= tower3BuyCost)
            {

                ps.towerBought(tower3BuyCost);
                ti.setTileinfo("Gatling");

                Debug.Log("on build3");

                instantiatedTower = Instantiate(tower3, ti.getTransform(), Quaternion.identity);
                ti.instantiateTowerModel(instantiatedTower);
                instantiatedTower.transform.position += new Vector3(0, 4, 0);
            }
        }
        else Debug.Log("Tile already occupied by a tower, sell a tower first");
    }

    public void onSell()
    {
        Transform tileInfo = transform.parent.parent;

        TileOption to = tileInfo.GetComponent<TileOption>();

        // retreive tile tower info
        if (to.getTileTowerType() == "Arrows")
        {
            GameObject playerStatus = GameObject.FindGameObjectWithTag("PlayerStatus");
            PlayerStatus ps = playerStatus.GetComponent<PlayerStatus>();
            ps.towerSold(tower1SellCost);
        }

        else if (to.getTileTowerType() == "Explosives")
        {
            GameObject playerStatus = GameObject.FindGameObjectWithTag("PlayerStatus");
            PlayerStatus ps = playerStatus.GetComponent<PlayerStatus>();
            ps.towerSold(tower2SellCost);
        }

        else if (to.getTileTowerType() == "Gatling")
        {
            GameObject playerStatus = GameObject.FindGameObjectWithTag("PlayerStatus");
            PlayerStatus ps = playerStatus.GetComponent<PlayerStatus>();
            ps.towerSold(tower3SellCost);
        }

        Debug.Log("on sell");

        to.setTileinfo("empty");

        to.destroyTowerModel();
    }
}
