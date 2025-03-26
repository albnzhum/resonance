using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAction : MonoBehaviour
{
    [SerializeField] private VoidEventChannelSO actionChannel;
    [SerializeField] private TutorialSystem tutorialSystem;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (tutorialSystem.CurrentStage == TutorialStages.Movement)
        {
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) ||
                Input.GetKey(KeyCode.D))
            {
                actionChannel.RaiseEvent();
            }
        }
    }
}
