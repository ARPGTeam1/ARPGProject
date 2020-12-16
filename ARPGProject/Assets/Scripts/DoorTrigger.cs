using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    [SerializeField] GameObject cellarDoor;

    [SerializeField] private bool isOpened = false;
    


    private void OnTriggerEnter(Collider col)
    {            
        if(isOpened == false)
        {
            cellarDoor.transform.position += new Vector3(0, 10, 0);
            isOpened = true;

        }

        
    }





}
 