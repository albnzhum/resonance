using UnityEngine;

public class ChangeParticleColor : MonoBehaviour
{
    private ParticleSystem ps;
    private ParticleSystem.Particle[] particles;
    public string triggerTag = "Water"; // ��� ��� ��������
    public float checkRadius = 0.1f; // ������ ��� �������� ���������
    private int removedParticlesCount = 0; // ������� �������� ������

    void Start()
    {
        ps = GetComponent<ParticleSystem>();
        particles = new ParticleSystem.Particle[ps.main.maxParticles];
    }

    void Update()
    {
        int numParticlesAlive = ps.GetParticles(particles);
        int newCount = 0;

        for (int i = 0; i < numParticlesAlive; i++)
        {
            // �������� ��������� ������� � �������
            if (IsParticleInTrigger(particles[i]))
            {
                // ������� ��� ������� ��� ��������
                removedParticlesCount++;
                continue; // ���������� ��� �������, ��� ����� "�������"
            }

            // ��������� �������, ���� ��� �� ���� �������
            particles[newCount] = particles[i];
            newCount++;
        }

        // ��������� ������� ������ � ����������� ���������
        ps.SetParticles(particles, newCount);

        // ������� ���������� �������� ������ � �������
        Debug.Log("������� ������: " + removedParticlesCount);
    }

    // ������� ��� �������� ��������� ������� � �������
    bool IsParticleInTrigger(ParticleSystem.Particle particle)
    {
        // ���������, ���� �� ���������� ����� � �������� �������
        Collider[] hitColliders = Physics.OverlapSphere(particle.position, checkRadius); // ������ ����� �����������������
        foreach (var collider in hitColliders)
        {
            // ��������, ���� �� � ���������� ������ ���
            if (collider.CompareTag(triggerTag))
            {
                return true; // ������� ��������� � �������� � ������ �����
            }
        }
        return false;
    }
}
