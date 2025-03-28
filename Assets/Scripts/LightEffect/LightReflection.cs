using UnityEngine;

public class LightReflection : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] GameObject volumetricLightBeam;
    [SerializeField] GameObject beamController;
    [SerializeField] float maxDistance = 100f;
    [SerializeField] float reflectedBeamLength = 50f;
    [SerializeField] float upwardAngleAdjustment = 30f;
    [SerializeField] LayerMask raycastLayerMask = ~0;

    private Light spotLight;
    private Light beamLight;
    private BeamTrigger beamTrigger;
    private bool isPlayerInTrigger = false;
    private Vector3 lastHitPoint;
    private Vector3 lastReflectedDirection;
    private CharacterController playerCharacterController;

    void Awake()
    {
        spotLight = GetComponent<Light>();
        beamLight = volumetricLightBeam.GetComponent<Light>();
        beamTrigger = beamController.GetComponent<BeamTrigger>();
        playerCharacterController = player.GetComponent<CharacterController>();
        Collider playerCollider = player.GetComponent<Collider>();
        volumetricLightBeam.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player.gameObject)
        {
            isPlayerInTrigger = true;
            if (!volumetricLightBeam.activeSelf)
            {
                volumetricLightBeam.SetActive(true);
            }

            ComputeReflection();
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject == player.gameObject)
        {
            ComputeReflection();
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player.gameObject)
        {
            isPlayerInTrigger = false;
            if (volumetricLightBeam.activeSelf)
            {
                volumetricLightBeam.SetActive(false);
                volumetricLightBeam.transform.position = Vector3.zero;
                volumetricLightBeam.transform.rotation = new Quaternion(0, 0, 0, 0);
            }
        }
    }

    private void ComputeReflection()
    {
        Vector3 playerTargetPosition = player.position;
        if (playerCharacterController != null)
        {
            playerTargetPosition = player.position + Vector3.up * playerCharacterController.height * 0.5f;
        }

        Vector3 directionToPlayer = (playerTargetPosition - transform.position).normalized;
        Ray ray = new Ray(transform.position, directionToPlayer);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, maxDistance, raycastLayerMask))
        {
            //Debug.DrawRay(transform.position, directionToPlayer, Color.red);
            Debug.Log(maxDistance);
            if (hit.collider.gameObject == player.gameObject)
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
                Debug.Log(maxDistance);
                lastHitPoint = hitPoint;
                lastReflectedDirection = reflectedDirection;

                volumetricLightBeam.transform.position = hitPoint;
                volumetricLightBeam.transform.rotation = Quaternion.LookRotation(reflectedDirection);

                Ray reflectedRay = new Ray(hitPoint, reflectedDirection);
                RaycastHit targetHit;

                if (Physics.Raycast(reflectedRay, out targetHit, reflectedBeamLength, raycastLayerMask))
                {
                    Debug.Log(maxDistance);
                    float beamLength = targetHit.distance;
                    if (beamLight != null)
                    {
                        beamLight.range = beamLength;
                    }

                    if (beamTrigger != null)
                    {
                        string targetTag = targetHit.collider.gameObject.tag;
                        Debug.Log(targetHit.collider.gameObject.name);

                        switch (targetTag)
                        {
                            case "Water":
                                beamTrigger.OnWaterHit(targetHit.collider.gameObject);
                                break;
                            case "Mechanism":
                                beamTrigger.OnMechanismHit(targetHit.collider.gameObject);
                                break;
                            case "Crystall":
                                beamTrigger.OnCrystallHit(targetHit.collider.gameObject);
                                break;
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
    }
}