
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorchFireToLeaves : MonoBehaviour
{
    [SerializeField] LeavesFire leavesFire;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            OnTorchFire();
        }
    }

    private void OnTorchFire()
    {
        if (leavesFire != null)
        {
            leavesFire.ParticlesOnFire();
        }
    }
}
