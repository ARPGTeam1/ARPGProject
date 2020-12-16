using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeeThroughWalls : MonoBehaviour
{


    [SerializeField] Transform target;
    private Transform lastObject;
    private Camera mainCam;
    
    private LineRenderer line;
    
    // Start is called before the first frame update
    void Start()
    {
        mainCam = Camera.main;
        line = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {

        RaycastHit[] hits;
        hits = Physics.RaycastAll(transform.position, target.position, (target.transform.position - this.transform.position).magnitude,LayerMask.GetMask("HideIfBehind"));

        for (int i = 0; i < hits.Length; i++)
        {
            RaycastHit hit = hits[i];
            MeshRenderer rend = hit.transform.GetComponent<MeshRenderer>();
            
            if (rend)
            {
                //rend.enabled = false;
                rend.material.color = new Color(1f, 1f, 1f, 0.2f);
            }
        }
        //Drawline(true);
    }
    
    private void Drawline(bool reachable)
    {
        
        line.SetPosition(0, transform.position);
        if(reachable) line.SetPosition(1, target.position);
        line.startWidth = reachable ? 0.1f : 0f;
        line.endWidth = reachable ? 0.1f : 0f;
        line.startColor= Color.yellow;
        line.endColor= Color.red;
    }
    
}
