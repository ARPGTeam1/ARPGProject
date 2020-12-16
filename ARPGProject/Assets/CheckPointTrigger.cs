using Characters.Player;
using UnityEngine;

public class CheckPointTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        FindObjectOfType<Revive>().checkPoint = transform.position;
    }
}
