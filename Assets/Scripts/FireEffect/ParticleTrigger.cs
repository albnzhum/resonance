using System.Collections.Generic;
using UnityEngine;

public class ParticleTrigger : MonoBehaviour
{
    ParticleSystem ps;
    List<ParticleSystem.Particle> inside = new List<ParticleSystem.Particle>();

    void Start()
    {
        ps = GetComponent<ParticleSystem>();
    }

    void OnParticleTrigger()
    {
        int numInside = ps.GetTriggerParticles(ParticleSystemTriggerEventType.Inside, inside);

        if (numInside > 0)
        {
            var triggerModule = ps.trigger;
            int colliderCount = triggerModule.colliderCount;

            for (int i = 0; i < numInside; i++)
            {
                ParticleSystem.Particle particle = inside[i];
                Collider trigger = GetTriggerForParticle(particle, triggerModule, colliderCount);
                if (trigger != null)
                {
                    Debug.Log($"Частица на позиции {particle.position} находится в триггере: {trigger.name}");
                }
            }
        }
    }

    Collider GetTriggerForParticle(ParticleSystem.Particle particle, ParticleSystem.TriggerModule triggerModule, int colliderCount)
    {
        for (int j = 0; j < colliderCount; j++)
        {
            var triggerObj = triggerModule.GetCollider(j);
            Collider trigger = triggerObj.gameObject.GetComponent<Collider>();
            if (trigger != null && trigger.bounds.Contains(particle.position))
            {
                return trigger;
            }
        }
        return null;
    }
}