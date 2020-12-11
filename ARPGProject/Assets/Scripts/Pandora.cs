using UnityEngine;

public class Pandora : MonoBehaviour
{
    public Light hopeLight;
    
    [SerializeField] private float minLight;
    [SerializeField] private float maxLight;
    [Space]
    [SerializeField] private bool alsoBuffLightRadius;
    [SerializeField] private float minLightRadius;
    [SerializeField] private float maxLightRadius;
    
    public void ChangeLight(float lightIntensityBuffAmount = 0f, float lightRadiusBuffAmount = 0f)
    {
        hopeLight.intensity = Mathf.Clamp(hopeLight.intensity + lightIntensityBuffAmount, minLight, maxLight);
        if(alsoBuffLightRadius)
            hopeLight.range = Mathf.Clamp(hopeLight.range + lightRadiusBuffAmount, minLightRadius, maxLightRadius);

        Debug.Log(ToString());
    }

    public override string ToString()
    {
        return $"Pandora's Light : {hopeLight.intensity}, : Radius : {hopeLight.range}";
    }
}
