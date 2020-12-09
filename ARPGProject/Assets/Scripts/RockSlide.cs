using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class RockSlide : MonoBehaviour
{
    [SerializeField] private GameObject rockSlidePrefab;
    
    [Header("RockSlide Settings")]
    [SerializeField] private float delayBeforeStart = 0f;
    [SerializeField] private float repeatRate = 0.3f;
    [SerializeField] private float rocksLifetime = 5f;
    [Space]
    [SerializeField] private bool neverEndingRockSlide;
    [SerializeField] private float periodBetweenSlides = 7;
    [Space]
    [Header("Rock Settings")]
    [SerializeField] private int rocks = 4;
    [SerializeField] private float minSize = 0.2f, maxSize = 1.5f;
    [SerializeField] private float torque ;
    
    private int _counter;
    private bool _alreadyTriggered = false;

    private void SpawnRock()
    {
        
        var random = Random.Range(minSize, maxSize);
        var instance = Instantiate(rockSlidePrefab, transform.position, Quaternion.identity, transform);
        instance.transform.localScale = new Vector3(random, random, random);
        random = Random.Range(minSize, maxSize);
        instance.GetComponent<Rigidbody>().AddTorque(Vector3.forward * torque);
        Destroy(instance, rocksLifetime);
        _counter++;
        if(_counter >= rocks) Stop();
    }

    private void Start()
    {
        if (neverEndingRockSlide)
        {
            StartCoroutine(Periodic(periodBetweenSlides));
        }
    }

    public void Begin()
    {
        _counter = 0;
        InvokeRepeating(nameof(SpawnRock), delayBeforeStart, repeatRate);
    }

    public void Stop() => CancelInvoke("SpawnRock");

    public IEnumerator Periodic(float intervalBetween)
    {
        while(neverEndingRockSlide)
        {
            Begin();
            yield return new WaitForSeconds(intervalBetween);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("Player") || _alreadyTriggered) return;
        
        Begin();
        _alreadyTriggered = true;
    }
}
