using UnityEngine;

public class LightReflection : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] GameObject volumetricLightBeam;
    [SerializeField] float maxDistance = 100f;
    [SerializeField] float reflectedBeamLength = 50f;
    [SerializeField] float upwardAngleAdjustment = 30f;

    private Light spotLight;
    private bool isPlayerInBeam = false;
    private Vector3 lastPlayerPosition;
    private bool hasMoved = false;
    private Light beamLight;
    private BeamTrigger beamTrigger;

    void Awake()
    {
        spotLight = GetComponent<Light>();
        beamLight = volumetricLightBeam.GetComponent<Light>();
        beamTrigger = volumetricLightBeam.GetComponent<BeamTrigger>();
        volumetricLightBeam.SetActive(false);
        lastPlayerPosition = player.position;
    }

    void Update()
    {
        Vector3 directionToPlayer = (player.position - transform.position).normalized;
        float angleToPlayer = Vector3.Angle(transform.forward, directionToPlayer);
        float spotAngle = spotLight.spotAngle * 0.5f;

        if (angleToPlayer <= spotAngle)
        {
            if (Vector3.Distance(player.position, lastPlayerPosition) > 0.01f)
            {
                hasMoved = true;
                if (!volumetricLightBeam.activeSelf)
                {
                    volumetricLightBeam.SetActive(true);
                }

                Ray ray = new Ray(transform.position, directionToPlayer);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit, maxDistance) && hit.collider.gameObject == player.gameObject)
                {
                    isPlayerInBeam = true;
                    ReflectLight(hit);
                }
                else
                {
                    if (volumetricLightBeam.activeSelf)
                    {
                        volumetricLightBeam.SetActive(false);
                    }
                }

                lastPlayerPosition = player.position;
            }
        }
        else
        {
            isPlayerInBeam = false;
            hasMoved = false;
            if (volumetricLightBeam.activeSelf)
            {
                volumetricLightBeam.SetActive(false);
            }
        }
    }

    void ReflectLight(RaycastHit hit)
    {
        Vector3 hitPoint = hit.point;
        Vector3 incomingDirection = (hitPoint - transform.position).normalized;

        Vector3 normal = Vector3.ProjectOnPlane(player.forward, Vector3.up).normalized;
        if (normal.magnitude < 0.01f)
        {
            normal = Vector3.up;
        }

        Vector3 reflectedDirection = Vector3.Reflect(incomingDirection, normal);
        Vector3 rotationAxis = Vector3.Cross(reflectedDirection, Vector3.up).normalized;
        reflectedDirection = Quaternion.AngleAxis(upwardAngleAdjustment, rotationAxis) * reflectedDirection;

        volumetricLightBeam.transform.position = hitPoint;
        volumetricLightBeam.transform.rotation = Quaternion.LookRotation(reflectedDirection);

        Ray reflectedRay = new Ray(hitPoint, reflectedDirection);
        RaycastHit targetHit;

        if (Physics.Raycast(reflectedRay, out targetHit, reflectedBeamLength))
        {
            float beamLength = targetHit.distance;
            if (beamLight != null)
            {
                beamLight.range = beamLength;
            }

            if (beamTrigger != null)
            {
                if (targetHit.collider.gameObject.CompareTag("Water"))
                {
                    beamTrigger.OnWaterHit(targetHit.collider.gameObject);
                }
                else if (targetHit.collider.gameObject.CompareTag("Mechanism"))
                {
                    beamTrigger.OnMechanismHit(targetHit.collider.gameObject);
                }
            }
        }
        else
        {
            if (beamLight != null)
            {
                beamLight.range = reflectedBeamLength;
            }
        }
    }
}