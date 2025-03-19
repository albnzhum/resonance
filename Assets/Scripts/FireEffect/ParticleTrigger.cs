using System.Collections.Generic;
using UnityEngine;

public class ParticleTrigger : MonoBehaviour
{
    ParticleSystem ps;
    [SerializeField] GameObject waterTrigger;
    [SerializeField] GameObject treeTrigger;

    private Collider waterCollider;
    private Collider treeCollider;

    void Awake()
    {
        ps = GetComponent<ParticleSystem>();

        waterCollider = waterTrigger.GetComponent<Collider>();
        treeCollider = treeTrigger.GetComponent<Collider>();
    }

    void Update()
    {
        ParticleSystem.Particle[] particles = new ParticleSystem.Particle[ps.main.maxParticles];
        int numParticlesAlive = ps.GetParticles(particles);

        int newCount = 0;

        for (int i = 0; i < numParticlesAlive; i++)
        {
            Vector3 particlePos = particles[i].position;
            bool shouldDelete = false;

            if (waterCollider != null && waterCollider.bounds.Contains(particlePos))
            {
                shouldDelete = true;
            }
            else if (treeCollider != null && treeCollider.bounds.Contains(particlePos))
            {
                OnParticleHitTree(particlePos);
            }

            if (!shouldDelete)
            {
                particles[newCount] = particles[i];
                newCount++;
            }
        }

        if (newCount != numParticlesAlive)
        {
            ps.SetParticles(particles, newCount);
        }
    }

    void OnParticleHitTree(Vector3 particlePosition)
    {
        treeTrigger.gameObject.GetComponent<TreeOnFire>().ParticlesOnFire();
    }
}