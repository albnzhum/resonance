using System.Collections;
using UnityEngine;

public class BeamTrigger : MonoBehaviour
{
    [SerializeField] ParticleSystem waterSmokeParticles;

    private GameObject waterObj;
    private bool waterSmoke;

    public void OnWaterHit(GameObject waterObject)
    {
        if (!waterSmoke)
        {
            waterSmoke = true;
            waterSmokeParticles.Play();
            waterObj = waterObject;
            Material waterObjMaterial = waterObj.GetComponent<Renderer>().material;
            StartCoroutine(WaterDestroy(waterObjMaterial));
        }
    }

    public void OnMechanismHit(GameObject mechanism)
    {
        mechanism.GetComponent<MechanismTest>().StartAction();
    }

    private IEnumerator WaterDestroy(Material waterMaterial)
    {
        while (waterMaterial.GetFloat("Vector1_b4651e1c4a334bef8c36a54b0b6c423c") > 0f)
        {
            waterMaterial.SetFloat("Vector1_b4651e1c4a334bef8c36a54b0b6c423c", waterMaterial.GetFloat("Vector1_b4651e1c4a334bef8c36a54b0b6c423c") - 0.1f);
            waterObj.transform.position = new Vector3(waterObj.transform.position.x, waterObj.transform.position.y - 0.2f, waterObj.gameObject.transform.position.z);
            yield return new WaitForSeconds(0.5f);
        }

        Destroy(waterObj);
        waterSmoke = false;
        waterObj = null;
    }
}