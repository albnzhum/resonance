using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestDustySurface : MonoBehaviour, IDustySurface
{
    public void BecomeDusty()
    {
        transform.localScale *= 2;
    }
}
