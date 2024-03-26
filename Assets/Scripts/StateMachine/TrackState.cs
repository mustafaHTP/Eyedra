using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackState : BaseState
{
    [SerializeField] private float _irisMovementSpeed;

    private event Action OnStateTransitioning;
    private bool _isPerformingBehavior = false;
    private Coroutine _trackBehaviorCR = null;

    public override void OnStateEnter(StateMachineController stateMachineController) { }

    public override void OnStateExit(StateMachineController stateMachineController)
    {
        OnStateTransitioning?.Invoke();
    }

    public override void OnStateUpdate(StateMachineController stateMachineController)
    {
        if (stateMachineController.TrackAreaStatus == TrackAreaStatus.Exited)
        {
            stateMachineController.ChangeState(stateMachineController.ResetState);
            return;
        }

        PerformTrackBehavior(stateMachineController);
    }

    private void OnEnable()
    {
        OnStateTransitioning += TrackState_OnStateTransitioning;
    }

    private void OnDisable()
    {
        OnStateTransitioning -= TrackState_OnStateTransitioning;
    }

    private void PerformTrackBehavior(StateMachineController stateMachineController)
    {
        if (!_isPerformingBehavior)
        {
            _trackBehaviorCR = StartCoroutine(PerformTrackBehaviorCoroutine(stateMachineController));
        }

        //Vector3 basePosition = stateMachineController.SharedData.BasePosition;
        //Vector3 mousePositionInWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //mousePositionInWorld.z = 0f;

        //Vector3 directionToMouse = mousePositionInWorld - basePosition;
        //Vector3 newPosition = basePosition + Vector3.ClampMagnitude(
        //    directionToMouse,
        //    stateMachineController.SharedData.IrisMovementCircleRadius);

        //stateMachineController.SharedData.IrisTS.position = newPosition;
    }

    private IEnumerator PerformTrackBehaviorCoroutine(StateMachineController stateMachineController)
    {
        _isPerformingBehavior = true;

        Vector3 basePosition = stateMachineController.SharedData.BasePosition;
        Vector3 mousePositionInWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePositionInWorld.z = 0f;

        Vector3 directionToMouse = mousePositionInWorld - basePosition;
        Vector3 newPosition = basePosition + Vector3.ClampMagnitude(
            directionToMouse,
            stateMachineController.SharedData.IrisMovementCircleRadius);

        yield return MoveToPositionCoroutine(stateMachineController, newPosition, _irisMovementSpeed);

        _isPerformingBehavior = false;
    }

    private void TrackState_OnStateTransitioning()
    {
        if(_trackBehaviorCR != null)
        {
            StopCoroutine(_trackBehaviorCR);
        }

        _isPerformingBehavior = false;
    }
}
