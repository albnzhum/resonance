using UnityEngine;

public class FlammableObject : MonoBehaviour
{
    public GameObject fireEffect;
    private bool isBurning = false;

    public void Ignite()
    {
        if (!isBurning)
        {
            isBurning = true;
            Instantiate(fireEffect, transform.position, Quaternion.identity);
            Destroy(gameObject, 5f);
        }
    }
}
