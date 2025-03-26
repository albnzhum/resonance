using System.Collections.Generic;
using UnityEngine;

public class ParticleTrigger : MonoBehaviour
{
    [SerializeField] ParticleSystem ps;
    [SerializeField] GameObject leavesTrigger;

    private Collider leavesCollider;

    void Awake()
    {
        leavesCollider = leavesTrigger.GetComponent<Collider>();
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
            
            if (leavesCollider != null && leavesCollider.bounds.Contains(particlePos))
            {
                Debug.Log(1);
                OnParticleHitleaves(particlePos);
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

    void OnParticleHitleaves(Vector3 particlePosition)
    {
        leavesTrigger.gameObject.GetComponent<LeavesFire>().ParticlesOnFire();
    }
}