using System;
using UnityEngine;

public class WaterRippleAction : MonoBehaviour, ITutorialAction
{
    [SerializeField] private BeamTrigger beamTrigger;
    public event Action OnActionCompleted;
    private bool _isCompleted = false;

    private void Start()
    {
        beamTrigger.OnWaterDestroyed += SimulateRipple;
    }

    private void OnDisable()
    {
        beamTrigger.OnWaterDestroyed -= SimulateRipple;
    }

    public void StartAction()
    {
        _isCompleted = false;
    }

    public void SimulateRipple() // Вызывается при ряби воды
    {
        if (!_isCompleted)
        {
            _isCompleted = true;
            OnActionCompleted?.Invoke();

            beamTrigger.enabled = false;
        }
    }
}