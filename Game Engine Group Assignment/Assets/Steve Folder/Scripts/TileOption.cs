using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TileOption : MonoBehaviour
{
    // used to store tower type
    [SerializeField] private string towertype;

    // TMP_Text for displaying title
    [SerializeField] private TMP_Text tiletitle;

    // TileInfo obj...
    [SerializeField] private TileInfo tileInfo;

    // store current position of selected tower
    private Transform tilePosition;
   
    // Start is called before the first frame update
    void Start()
    {
        tiletitle.text = this.towertype;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void loadStatus(TileInfo ti)
    {
        this.towertype = ti.getTileTowerType();

        tiletitle.text = this.towertype;

        tileInfo = ti;

        tilePosition = ti.transform;
    }

    public void setTileinfo(string str)
    {
        tileInfo.setTileInfo(str);
        tiletitle.text = str;
    }

    public Vector3 getTransform()
    {
        return tilePosition.position;
    }

    public string getTileTowerType()
    {
        return tileInfo.getTileTowerType();
    }

    public bool isTowerInstantiate()
    {
        return tileInfo.isTowerInstantiate();
    }

    public void instantiateTowerModel(GameObject obj)
    {
        tileInfo.instantiateTowerModel(obj);
    }

    public void destroyTowerModel()
    {
        tileInfo.destroyTowerModel();
    }


}
