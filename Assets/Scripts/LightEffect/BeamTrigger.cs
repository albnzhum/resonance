using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamTrigger : MonoBehaviour
{
    [SerializeField] ParticleSystem waterSmokeParticles;
    [SerializeField] List<GameObject> waterOpenObj;
    private bool waterSmoke;

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
        while (waterMaterial.GetFloat("Vector1_b4651e1c4a334bef8c36a54b0b6c423c") > 0f)
        {
            waterMaterial.SetFloat("Vector1_b4651e1c4a334bef8c36a54b0b6c423c", waterMaterial.GetFloat("Vector1_b4651e1c4a334bef8c36a54b0b6c423c") - 0.1f);
            yield return new WaitForSeconds(0.5f);
        }

        Destroy(waterObject);
        waterSmoke = false;
        foreach (GameObject obj in waterOpenObj)
        {
            obj.SetActive(false);
        }
    }
}