using UnityEngine;

public class FireSource : MonoBehaviour
{
    [SerializeField] ParticleSystem sparks;
    [SerializeField] float sparkLifetime = 3f;
    [SerializeField] float sparkSpreadRadius = 2f;

    public void Ignite()
    {
        sparks.Play();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Flammable"))
        {
            other.GetComponent<FlammableObject>().Ignite();
        }
    }
}
