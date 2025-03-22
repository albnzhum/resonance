using UnityEditor;
using UnityEngine;

public class LightReflection : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] GameObject volumetricLightBeam;
    [SerializeField] GameObject beamController;
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
        beamTrigger = beamController.GetComponent<BeamTrigger>();
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
        // Центр отражения — точка попадания луча
        Vector3 hitPoint = hit.point;

        // Вычисляем направление падающего луча
        Vector3 incomingDirection = (hitPoint - transform.position).normalized;

        // Вычисляем нормаль к поверхности
        Vector3 normal = hit.normal;

        // Отражаем свет относительно нормали
        Vector3 reflectedDirection = Vector3.Reflect(incomingDirection, normal);

        // --- Смещаем источник света немного назад ---
        Vector3 lightPosition = hitPoint - reflectedDirection * 2f; // Сдвиг на 0.5 метра назад

        // --- Наклоняем луч на 30 градусов вниз ---
        reflectedDirection = Quaternion.AngleAxis(30f, Vector3.Cross(reflectedDirection, Vector3.up)) * reflectedDirection;

        // Устанавливаем позицию и направление света
        volumetricLightBeam.transform.position = lightPosition;
        volumetricLightBeam.transform.rotation = Quaternion.LookRotation(reflectedDirection);

        // Создаём новый луч
        Ray reflectedRay = new Ray(lightPosition, reflectedDirection);
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