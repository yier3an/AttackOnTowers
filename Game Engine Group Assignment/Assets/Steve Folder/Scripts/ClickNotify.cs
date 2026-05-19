using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//for UI
using UnityEngine.UI;


public class ClickNotify : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NotifyMSG()
    {
        Debug.Log("Game Obj: " + gameObject + " clicked");
    }

}
