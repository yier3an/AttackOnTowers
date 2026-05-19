using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TileInfo : MonoBehaviour
{
    [SerializeField] private string towertype;

    // store instance of instantiated tower
    private GameObject instantiatedTower;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool isTowerInstantiate()
    {
        return instantiatedTower != null;
    }

    public void instantiateTowerModel(GameObject obj)
    {
        instantiatedTower = obj;
    }

    public void destroyTowerModel()
    {
        Destroy(instantiatedTower);
    }
    
    public void setTileInfo(string str)
    {
        towertype = str;
    }

    public string getTileTowerType()
    {
        return towertype;
    }

    public TileInfo getTileInfo()
    {
        return this;
    }
}
