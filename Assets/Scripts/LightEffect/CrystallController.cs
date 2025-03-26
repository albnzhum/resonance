using System.Collections;
using UnityEngine;

public class CrystallController : MonoBehaviour
{
    [Header("Light")]
    [SerializeField] Light crystallLight;
    [SerializeField] Light crystallSpotLight;
    [SerializeField] ParticleSystem crystallParticles;

    [Header("Audio")]
    [SerializeField] AudioSource crystallSound;

    [Header("Other scripts")]
    [SerializeField] WallCrystallController wallCrystallController;

    [Header("Light Settings")]
    [SerializeField] float maxCrystallLightIntensity = 25f;
    [SerializeField] float maxSpotLightIntensity = 1f;
    [SerializeField] float lightIncreaseRate = 1.25f;
    [SerializeField] float spotLightIncreaseRate = 0.1f;
    [SerializeField] float lightDecreaseRate = 0.125f;
    [SerializeField] float spotLightDecreaseRate = 0.1f;
    [SerializeField] float lightDetectionTimeout = 0.2f;

    public bool IsActivated() => isActivated;

    private bool isLightOn = false;
    private bool isActivated = false;
    private Coroutine lightCoroutine;
    private float lastLightDetectionTime;

    void Start()
    {
        crystallLight.intensity = 0f;
        crystallSpotLight.intensity = 0f;

        if (crystallSound.isPlaying)
        {
            crystallSound.Stop();
        }
    }

    public void StartAction()
    {
        lastLightDetectionTime = Time.time;

        if (!isLightOn)
        {
            isLightOn = true;

            if (lightCoroutine != null)
            {
                StopCoroutine(lightCoroutine);
            }

            lightCoroutine = StartCoroutine(ManageLight());
        }
    }

    private IEnumerator ManageLight()
    {
        while (isLightOn)
        {
            if (Time.time - lastLightDetectionTime > lightDetectionTimeout)
            {
                isLightOn = false;
                break;
            }

            if (crystallLight.intensity < maxCrystallLightIntensity)
            {
                crystallLight.intensity += lightIncreaseRate;
            }

            if (crystallSpotLight.intensity < maxSpotLightIntensity)
            {
                crystallSpotLight.intensity += spotLightIncreaseRate;

                if (crystallSpotLight.intensity >= maxSpotLightIntensity && !isActivated)
                {
                    isActivated = true;
                    crystallParticles.Play();
                    if (!crystallSound.isPlaying)
                    {
                        crystallSound.Play();
                    }
                }
            }

            yield return new WaitForSeconds(0.1f);
        }

        while (crystallLight.intensity > 0 || crystallSpotLight.intensity > 0)
        {
            if (crystallLight.intensity > 0)
            {
                crystallLight.intensity -= lightDecreaseRate;
                if (crystallLight.intensity < 0)
                {
                    crystallLight.intensity = 0;
                }
            }

            if (crystallSpotLight.intensity > 0)
            {
                crystallSpotLight.intensity -= spotLightDecreaseRate;
                if (crystallSpotLight.intensity < 0)
                {
                    crystallSpotLight.intensity = 0;
                }

                if (crystallSpotLight.intensity <= 0 && isActivated)
                {
                    isActivated = false;
                    crystallParticles.Stop();
                    if (crystallSound.isPlaying)
                    {
                        crystallSound.Stop();
                    }
                }
            }

            yield return new WaitForSeconds(0.1f);
        }

        lightCoroutine = null;
    }
}