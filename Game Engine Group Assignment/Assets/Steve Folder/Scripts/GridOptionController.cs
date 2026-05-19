using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridOptionController : MonoBehaviour
{
    private GameObject option;
    public GridSelectorController selector;

    // Start is called before the first frame update
    void Start()
    {
        option = gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //to open layer 1 options
    public void OnOpenLayer1Options()
    {
        selector.setGridActive(gameObject);
    }

    //to open layer 2 options
    public void OnOpenLayer2Options()
    {
        selector.setGridActive(gameObject);
    }

    public void OnCloseOptions()
    {
        // don't display the settings popup
        option.gameObject.SetActive(false);
    }

    public void onSell()
    {
    }

    public void onBuildOptions()
    {
        selector.setGridActive(transform.Find("Build_options").gameObject);
    }

    public void onBuild1()
    {
    }

    public void onBuild2()
    {
    }

    public void onBuild3()
    {
    }

}
