using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoverEffect : MonoBehaviour
{
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

    void OnMouseOver()
    {
        meshRenderer.enabled = true;
    }

    void OnMouseExit()
    {
        meshRenderer.enabled = false;
    }
}
