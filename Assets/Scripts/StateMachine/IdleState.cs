using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : BaseState
{
    [SerializeField] private float _irisMovementSpeed;
    [SerializeField] private float _irisMovementDelay;

    private event Action OnStateTransitioning;
    private bool _isPerformingBehavior = false;
    private Coroutine _idleBehaviorCR = null;

    public override void OnStateEnter(StateMachineController stateMachineController) { }

    public override void OnStateExit(StateMachineController stateMachineController)
    {
        OnStateTransitioning?.Invoke();
    }

    public override void OnStateUpdate(StateMachineController stateMachineController)
    {
        if(stateMachineController.TrackAreaStatus == TrackAreaStatus.Entered 
            || stateMachineController.TrackAreaStatus == TrackAreaStatus.Stayed)
        {
            stateMachineController.ChangeState(stateMachineController.TrackState);
            return;
        }

        PerformIdleBehavior(stateMachineController);
    }

    private void OnEnable()
    {
        OnStateTransitioning += IdleState_OnStateTransitioning;
    }

    private void OnDisable()
    {
        OnStateTransitioning -= IdleState_OnStateTransitioning;
    }

    private void PerformIdleBehavior(StateMachineController stateMachineController)
    {
        if (!_isPerformingBehavior)
        {
            _idleBehaviorCR = StartCoroutine(PerformIdleBehaviorCoroutine(stateMachineController));
        }
    }

    private IEnumerator PerformIdleBehaviorCoroutine(StateMachineController stateMachineController)
    {
        _isPerformingBehavior = true;

        Vector3 newPosition = stateMachineController.SharedData.BasePosition 
            + (Vector3)UnityEngine.Random.insideUnitCircle * stateMachineController.SharedData.IrisMovementCircleRadius;

        yield return MoveToPositionCoroutine(stateMachineController, newPosition, _irisMovementSpeed);

        yield return new WaitForSeconds(_irisMovementDelay);

        _isPerformingBehavior = false;
    }

    private void IdleState_OnStateTransitioning()
    {
        if (_idleBehaviorCR != null)
        {
            StopCoroutine(_idleBehaviorCR);
        }

        _isPerformingBehavior = false;
    }
}
