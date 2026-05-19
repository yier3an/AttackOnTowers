using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TileClickHandler : MonoBehaviour
{
    // for opening layer1 layer2 options
    [SerializeField] private TileSelectorController selector;

    // options background
    [SerializeField] private GameObject options;

    // selecting layer 1 options
    [SerializeField] private GameObject optionL1;

    // selecting layer 2 options
    [SerializeField] private GameObject optionL2;

    // used to store which tile selected for reference
    [SerializeField] private TileInfo tileInfo;

    // for Tile Options Menu, for storing temporary information.
    [SerializeField] private TileOption tileOption;

    // for Tile Options Menu, for storing temporary information.
    [SerializeField] private AudioSource clickSfx;

    // for hover effect
    private MeshRenderer meshRenderer;

    // Start is called before the first frame update
    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // This method will be called when the mouse clicks the cube
    void OnMouseDown()
    {
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            if (selector != null)
            {
                selector.closeOption();
                meshRenderer.enabled = true;
            }
            else meshRenderer.enabled = true;



            optionL1.SetActive(true);
            optionL2.SetActive(false);
            options.SetActive(true);

            tileInfo = GetComponent<TileInfo>();

            tileOption.loadStatus(tileInfo.getTileInfo());

            selector.tiletoclose(tileInfo);

            clickSfx.Play();
        }
    }
}
