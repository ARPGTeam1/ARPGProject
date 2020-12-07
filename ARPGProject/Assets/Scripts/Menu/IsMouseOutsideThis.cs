using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class IsMouseOutsideThis : MonoBehaviour
{
    

    
    private GraphicRaycaster raycast;
    public bool IsPointerOutside()
    {
 
            PointerEventData pointerData = new PointerEventData (EventSystem.current)
            {
                pointerId = -1,
            };
         
            pointerData.position = Input.mousePosition;
 
            List<RaycastResult> results = new List<RaycastResult>();
            EventSystem.current.IsPointerOverGameObject();

            Debug.Log( results.Count );
        
        
            
        if (EventSystem.current.currentSelectedGameObject != this.gameObject)
        {
            
            return true;
        }
        return false;
    }
    
}


