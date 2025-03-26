using System;


public interface ITutorialAction
{
    event Action OnActionCompleted;
    void StartAction();
}