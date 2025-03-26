using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bonfire : MonoBehaviour, IInteractableTouching
{
    [SerializeField] List<BurningObject> burningObjects;

    public void Interact(Player player)
    {
        Debug.Log(0);
        LitBurningObjects();
    }

    private void LitBurningObjects()
    {
        foreach (var obj in burningObjects)
        {
            obj.Burn();
        }
    }


}
