using System.Collections;
using UnityEngine;
public class FlickerLight : MonoBehaviour
{
    public Light childLight;
    
    private void Start()
    {
        if (childLight == null)
        {
            childLight = GetComponentInChildren<Light>();
        }
        StartCoroutine(Flicker());
    }
    private IEnumerator Flicker()
    {
        while (true)
        {
            childLight.enabled = !childLight.enabled;
            yield return new WaitForSeconds(1);
        }
    }
}