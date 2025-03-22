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
        while (waterMaterial.GetFloat("_DepthFade") > 0f)
        {
            waterMaterial.SetFloat("_DepthFade", waterMaterial.GetFloat("_DepthFade") - 0.1f);
            waterObj.transform.position = new Vector3(waterObj.transform.position.x, waterObj.transform.position.y - 0.2f, waterObj.gameObject.transform.position.z);
            yield return new WaitForSeconds(0.5f);
        }

        Destroy(waterObj);
        waterSmoke = false;
        waterObj = null;
    }
}