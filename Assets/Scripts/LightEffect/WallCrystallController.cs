using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallCrystallController : MonoBehaviour
{
    [Header("Light data")]
    [SerializeField] Light crystallPointLight;
    [SerializeField] Light crystallSpotLight;
    [SerializeField] List<CrystallController> crystals;

    [Header("Actions")]
    [SerializeField] BeamTrigger beamTrigger;
    [SerializeField] GameObject waterObj;

    private Coroutine chargeLightCoroutine;
    private bool isCharging = false;

    void Start()
    {
        crystallPointLight.intensity = 0f;
        crystallSpotLight.intensity = 0f;
    }

    void Update()
    {
        int activeCrystals = 0;
        foreach (CrystallController crystal in crystals)
        {
            if (crystal.IsActivated())
            {
                activeCrystals++;
            }
        }

        if (activeCrystals >= 2 && !isCharging)
        {
            isCharging = true;
            if (chargeLightCoroutine != null)
            {
                StopCoroutine(chargeLightCoroutine);
            }
            chargeLightCoroutine = StartCoroutine(ChargeLight());

        } else if (activeCrystals < 2 && isCharging)
        {
            isCharging = false;
            if (chargeLightCoroutine != null)
            {
                StopCoroutine(chargeLightCoroutine);
            }
            crystallPointLight.intensity = 0f;
            crystallSpotLight.intensity = 0f;

        }
    }

    IEnumerator ChargeLight()
    {
        while (crystallPointLight.intensity < 10)
        {
            crystallPointLight.intensity += 1f;
            yield return new WaitForSeconds(0.2f);
        }

        while (crystallSpotLight.intensity < 25)
        {
            crystallSpotLight.intensity += 1.25f;
            yield return new WaitForSeconds(0.2f);
        }

        beamTrigger.OnWaterHit(waterObj);
    }
}