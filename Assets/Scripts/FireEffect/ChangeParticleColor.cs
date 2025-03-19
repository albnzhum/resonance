using UnityEngine;

public class ChangeParticleColor : MonoBehaviour
{
    private ParticleSystem ps;
    private ParticleSystem.Particle[] particles;
    public string triggerTag = "Water"; // Тег для триггера
    public float checkRadius = 0.1f; // Радиус для проверки попадания
    private int removedParticlesCount = 0; // Счётчик удалённых частиц

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
            // Проверка попадания частицы в триггер
            if (IsParticleInTrigger(particles[i]))
            {
                // Считаем эту частицу как удалённую
                removedParticlesCount++;
                continue; // Пропускаем эту частицу, она будет "удалена"
            }

            // Сохраняем частицу, если она не была удалена
            particles[newCount] = particles[i];
            newCount++;
        }

        // Обновляем систему частиц с оставшимися частицами
        ps.SetParticles(particles, newCount);

        // Выводим количество удалённых частиц в консоль
        Debug.Log("Удалено частиц: " + removedParticlesCount);
    }

    // Функция для проверки попадания частицы в триггер
    bool IsParticleInTrigger(ParticleSystem.Particle particle)
    {
        // Проверяем, есть ли коллайдеры рядом с позицией частицы
        Collider[] hitColliders = Physics.OverlapSphere(particle.position, checkRadius); // Радиус можно подкорректировать
        foreach (var collider in hitColliders)
        {
            // Проверка, есть ли у коллайдера нужный тег
            if (collider.CompareTag(triggerTag))
            {
                return true; // Частица находится в триггере с нужным тегом
            }
        }
        return false;
    }
}
