using UnityEngine;

public class Collector : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {
        other.gameObject.GetComponent<IDamagable>()?.TakeDamage(1);
    }
}
