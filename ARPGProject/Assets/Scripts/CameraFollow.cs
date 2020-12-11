using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject player;

    public float followSpeed;

    [SerializeField] private Vector3 offset;

    private void LateUpdate()
    {
        this.transform.position = Vector3.Lerp(this.transform.position, this.player.transform.position + this.offset,
            Time.deltaTime * this.followSpeed);
    }

    private void Update()
    {
        #if UNITY_EDITOR
            offset += new Vector3(0, -Input.mouseScrollDelta.y, Input.mouseScrollDelta.y);
        #endif
        
    }
}
