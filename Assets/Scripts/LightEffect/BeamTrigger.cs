using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamTrigger : MonoBehaviour
{
    [SerializeField] ParticleSystem waterSmokeParticles;
    [SerializeField] List<GameObject> waterOpenObj;
    private bool waterSmoke;

    public Action OnWaterDestroyed;

    public void OnWaterHit(GameObject waterObject)
    {
        if (!waterSmoke)
        {
            waterSmoke = true;
            waterSmokeParticles.Play();
            Material waterObjMaterial = waterObject.GetComponent<Renderer>().material;
            StartCoroutine(WaterDestroy(waterObjMaterial, waterObject));
        }
    }

    public void OnMechanismHit(GameObject mechanism)
    {
        mechanism.GetComponent<MechanismTest>().StartAction();
    }

    public void OnCrystallHit(GameObject crystall)
    {
        crystall.GetComponent<CrystallController>().StartAction();
    }

    private IEnumerator WaterDestroy(Material waterMaterial, GameObject waterObject)
    {
        while (waterMaterial.GetFloat("_DepthFade") > 0f)
        {
            waterMaterial.SetFloat("_DepthFade", waterMaterial.GetFloat("_DepthFade") - 0.1f);
            yield return new WaitForSeconds(0.5f);
        }

        Destroy(waterObject);
        waterSmoke = false;
        foreach (GameObject obj in waterOpenObj)
        {
            obj.SetActive(false);
        }
        
        OnWaterDestroyed?.Invoke();
    }
}