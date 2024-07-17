using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using UnityEditor.Rendering.HighDefinition;
using UnityEngine;

public class GetMapToM : MonoBehaviour
{
    public GameObject Map;
    private bool open;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            if (open == false)
            {
                Map.SetActive(true);
                open = true;
            }
            else
            {
                Map.SetActive(false);
                open = false;
            }
        }
    }
}
