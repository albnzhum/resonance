using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamTrigger : MonoBehaviour
{
    [SerializeField] ParticleSystem waterSmokeParticles;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Water"))
        {
            Debug.Log(1);
            OnWaterHit(other.gameObject);
        } else if (other.CompareTag("Mechanism"))
        {
            OnMechanismHit(other.gameObject);
        }
    }

    private void OnWaterHit(GameObject waterObject)
    {
        waterSmokeParticles.Play();
        Material waterObjMaterial = waterObject.GetComponent<Renderer>().material;
        StartCoroutine(WaterDestroy(waterObjMaterial, waterObject));
    }

    IEnumerator WaterDestroy(Material waterMaterial, GameObject waterObject)
    {
        while (waterMaterial.GetFloat("Vector1_b4651e1c4a334bef8c36a54b0b6c423c") > 0f)
        {
            waterMaterial.SetFloat("Vector1_b4651e1c4a334bef8c36a54b0b6c423c", waterMaterial.GetFloat("Vector1_b4651e1c4a334bef8c36a54b0b6c423c") - 0.1f);
            yield return new WaitForSeconds(0.5f);
        }

        Destroy(waterObject);
    }

    private void OnMechanismHit(GameObject mechanism)
    {
        mechanism.GetComponent<MechanismTest>().StartAction();
    }
}