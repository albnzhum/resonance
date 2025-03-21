using UnityEngine;

public class LightReflection : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] GameObject reflectedBeamObject;
    [SerializeField] float maxDistance = 100f;
    [SerializeField] float reflectedBeamLength = 50f;

    private Light spotLight;
    private bool isPlayerInBeam = false;
    private LineRenderer reflectedBeam;
    private BoxCollider beamCollider;

    void Start()
    {
        spotLight = GetComponent<Light>();
        reflectedBeam = reflectedBeamObject.GetComponent<LineRenderer>();
        beamCollider = reflectedBeamObject.GetComponent<BoxCollider>();
    }

    void Update()
    {
        Vector3 directionToPlayer = (player.position - transform.position).normalized;
        float angleToPlayer = Vector3.Angle(transform.forward, directionToPlayer);
        float spotAngle = spotLight.spotAngle * 0.5f;

        if (angleToPlayer <= spotAngle)
        {
            if (!reflectedBeamObject.activeSelf) reflectedBeamObject.SetActive(true);
            Ray ray = new Ray(transform.position, directionToPlayer);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, maxDistance))
            {
                if (hit.collider.gameObject == player.gameObject)
                {
                    isPlayerInBeam = true;
                    ReflectLight(hit);
                }
            }
        } else
        {
            if (reflectedBeamObject.activeSelf) reflectedBeamObject.SetActive(false);
        }
    }

    void ReflectLight(RaycastHit hit)
    {
        if (reflectedBeamObject == null || reflectedBeam == null || beamCollider == null) return;

        Vector3 hitPoint = hit.point;
        Vector3 incomingDirection = (hitPoint - transform.position).normalized;

        Vector3 normal = hit.normal;
        Vector3 reflectedDirection = Vector3.Reflect(incomingDirection, normal);

        Ray reflectedRay = new Ray(hitPoint, reflectedDirection);
        RaycastHit targetHit;

        float beamLength = reflectedBeamLength;
        Vector3 endPoint;

        if (Physics.Raycast(reflectedRay, out targetHit, reflectedBeamLength))
        {
            beamLength = targetHit.distance;
            endPoint = targetHit.point;
        }
        else
        {
            endPoint = hitPoint + reflectedDirection * beamLength;
        }

        reflectedBeam.positionCount = 2;
        reflectedBeam.SetPosition(0, hitPoint);
        reflectedBeam.SetPosition(1, hitPoint + reflectedDirection * beamLength);

        Vector3 beamStart = hitPoint;
        Vector3 beamEnd = hitPoint + reflectedDirection * beamLength;
        Vector3 beamCenter = (beamStart + beamEnd) / 2f;
        reflectedBeamObject.transform.position = beamCenter;
        reflectedBeamObject.transform.rotation = Quaternion.LookRotation(reflectedDirection);

        beamCollider.size = new Vector3(beamCollider.size.x, beamCollider.size.y, beamLength);
        beamCollider.center = new Vector3(0, 0, beamLength / 2f);
    }
}