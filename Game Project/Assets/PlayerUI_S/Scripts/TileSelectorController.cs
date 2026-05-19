using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TileSelectorController : MonoBehaviour
{
    // options background
    [SerializeField] GameObject Options;

    // options layer 1
    [SerializeField] GameObject OptionL1;

    // options layer 2
    [SerializeField] GameObject OptionL2;

    // for managing selected tile (highlighting tile effects)
    private TileInfo tileOBGtoclose;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setLayer1()
    {
        OptionL1.SetActive(true);
    }

    public void setLayer2()
    {
        OptionL1.SetActive(false);
        OptionL2.SetActive(true);
    }

    public void backButton()
    {
        OptionL2.SetActive(false);
        OptionL1.SetActive(true);
    }
    public void closeOption()
    {
        if (tileOBGtoclose != null)
        {
            tileOBGtoclose.GetComponent<MeshRenderer>().enabled = false;
        }

        OptionL2.SetActive(false);
        OptionL1.SetActive(true);
        Options.SetActive(false);
    }

    public void tiletoclose(TileInfo obj)
    {
        tileOBGtoclose = obj;
    }

    
}
