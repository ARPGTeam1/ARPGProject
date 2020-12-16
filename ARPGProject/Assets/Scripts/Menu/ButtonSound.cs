using UnityEngine;
using UnityEngine.EventSystems;

namespace Menu
{
    public class ButtonSound : MonoBehaviour, IPointerClickHandler
    {
        [FMODUnity.EventRef] [SerializeField] private string onClickSound;
    
        public void OnPointerClick(PointerEventData eventData)
        {
            FMODUnity.RuntimeManager.PlayOneShot(onClickSound);
        }
    }
}
