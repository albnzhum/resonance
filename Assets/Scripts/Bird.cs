using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Bird : MonoBehaviour
{
    [SerializeField] List<FlightDestination> flights;

    [SerializeField] BirdAnimationController _animationController;

    [SerializeField] float flySpeed = 5f;
    [SerializeField] float takeOffHeight = 0.2f;
    [SerializeField] float takeOffSpeed = 1.0f;
    [SerializeField] float takeOffDelay = 1.0f;
    [SerializeField] float landSpeed = 1.0f;
    [SerializeField] float heightOffset = 0.2f;
    [SerializeField] Transform pawsPoint;
    [SerializeField] LeverMechanism lever;

    public void TakeOff()
    {
        _animationController.onTakeOffEnd += StartFly;
        _animationController.TakeOff();
    }

    public void StartFly()
    {
        _animationController.onTakeOffEnd -= StartFly;
        StartCoroutine(FlyToDestinationsCoroutine(flights));
        _animationController.Fly();
    }

    private IEnumerator FlyToDestinationsCoroutine(List<FlightDestination> destinations)
    {
        yield return MoveToTarget(pawsPoint.position + Vector3.up * takeOffHeight, takeOffSpeed);
        yield return new WaitForSecondsRealtime(takeOffDelay);

        // Полет по точкам
        foreach (var destination in destinations)
        {
            yield return MoveToTarget(destination.Target.position + Vector3.up * heightOffset, destination.Speed);
            if (destination.DelayOnTarget > 0)
                yield return new WaitForSecondsRealtime(destination.DelayOnTarget);
        }
        _animationController.onLand += Land;
        _animationController.Land();
    }

    private IEnumerator MoveToTarget(Vector3 targetPosition, float speed)
    {
        // Разница между положением центра вороны и её лап
        Vector3 offset = transform.position - pawsPoint.position;

        // Смещаем цель, чтобы лапы приземлялись точно на targetPosition
        Vector3 adjustedTarget = targetPosition + offset;

        while (Vector3.Distance(pawsPoint.position, targetPosition) >= 0.01f)
        {
            transform.position = Vector3.MoveTowards(transform.position, adjustedTarget, speed * Time.deltaTime);
            yield return null;
        }
    }

    private void Land()
    {
        StopAllCoroutines();
        _animationController.onLand -= Land;
        StartCoroutine(MoveToTarget(flights.Last().Target.position, landSpeed));

        if (lever != null)
            _animationController.onLandEnd += LandOnLever;
    }
    private void LandOnLever()
    {
        //transform.SetParent(lever.pivot); // Привязываем ворону к рычагу
        //lever.ActivateLever(transform); // Активируем рычаг
        //_animationController.onLandEnd -= LandOnLever;
    }

}
