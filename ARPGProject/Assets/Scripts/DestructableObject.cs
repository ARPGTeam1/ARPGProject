using UnityEngine;

[System.Serializable]
public class DestructableObject : MonoBehaviour, IDamagable
{
    private DropLoot _dropLoot;

    private void Start()
    {
        _dropLoot = GetComponent<DropLoot>();
    }

    public void TakeDamage()
    {
        /*var instance = Instantiate(myLoot.RandomDrop());
        instance.transform.Translate(Vector3.up * 5, Space.World);*/
        _dropLoot.Drop();
        Destroy(gameObject);    
    }
}