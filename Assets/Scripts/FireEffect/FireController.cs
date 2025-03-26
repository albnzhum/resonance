using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireController : MonoBehaviour
{
    [SerializeField] GameObject fireObj;
    [SerializeField] float timeToMax = 5f;

    private ParticleSystem ps;
    private AudioSource fireSound;
    private Vector3 startScale;
    private Vector3 endScale = new Vector3(0.7f, 0.7f, 0.7f);

    private void Awake()
    {
        ps = fireObj.GetComponent<ParticleSystem>();
        fireSound = fireObj.GetComponent<AudioSource>();
        startScale = fireObj.transform.localScale;
    }

    public void StartBurn()
    {
        ps.Play();
        fireSound.Play();
        StartCoroutine(FireScale());
    }

    IEnumerator FireScale()
    {
        float elapsedTime = 0.0f;

        while ((elapsedTime += Time.deltaTime) <= timeToMax)
        {
            fireObj.transform.localScale = Vector3.Lerp(startScale, endScale, elapsedTime / timeToMax);
            yield return null;
        }

        yield return new WaitForSeconds(5f);

        Destroy(fireObj.transform.parent.gameObject);
    }
}
