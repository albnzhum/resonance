using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLightChecker : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] float maxDistance = 100f;

    private Light spotLight;

    void Start()
    {
        spotLight = GetComponent<Light>();
    }

    void Update()
    {
        if (IsPlayerInLightCone())
        {
            Debug.Log("Игрок в конусе света.");
        }
        else
        {
            Debug.Log("Игрок не в конусе света.");
        }
    }

    bool IsPlayerInLightCone()
    {
        Vector3 directionToPlayer = player.position - transform.position;
        float distanceToPlayer = directionToPlayer.magnitude;

        if (distanceToPlayer > maxDistance) return false;

        directionToPlayer.Normalize();

        float angle = Vector3.Angle(transform.forward, directionToPlayer);

        if (angle < spotLight.spotAngle / 2f)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, directionToPlayer, out hit, maxDistance))
            {
                if (hit.collider.gameObject == player.gameObject)
                {
                    return true;
                }
            }
        }

        return false;
    }
}
