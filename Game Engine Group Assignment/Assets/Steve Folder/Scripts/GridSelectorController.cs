using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GridSelectorController : MonoBehaviour
{
    GameObject GridCurrent;
    Transform GridActivate;
    GameObject GridDeactivate;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setGridActive(GameObject obj)
    {
        //check if any active previously
        if (GridDeactivate != null)
        {
            //deactivate previously opened option
            GridDeactivate.SetActive(false);
        }

        //display new selected option 
        obj.SetActive(true);

        GridDeactivate = obj;
    }
}
