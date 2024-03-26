using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetState : BaseState
{
    [SerializeField] private float _stateChangeDelay;
    [SerializeField] private float _irisMovementSpeed;

    private bool _isPerformingBehavior = false;
    private bool _hasPerformedBehavior = false;

    public override void OnStateEnter(StateMachineController stateMachineController) { }

    public override void OnStateExit(StateMachineController stateMachineController)
    {
        _hasPerformedBehavior = false;
        _isPerformingBehavior = false;
    }

    public override void OnStateUpdate(StateMachineController stateMachineController)
    {
        if (_hasPerformedBehavior)
        {
            stateMachineController.ChangeState(stateMachineController.IdleState);
            return;
        }

        PerformResetBehavior(stateMachineController);
    }

    private void PerformResetBehavior(StateMachineController stateMachineController)
    {
        if (!_isPerformingBehavior)
        {
            StartCoroutine(PerformResetBehaviorCoroutine(stateMachineController));
        }
    }

    private IEnumerator PerformResetBehaviorCoroutine(StateMachineController stateMachineController)
    {
        _isPerformingBehavior = true;

        yield return MoveToPositionCoroutine(
            stateMachineController, 
            stateMachineController.SharedData.BasePosition,
            _irisMovementSpeed);

        yield return new WaitForSeconds(_stateChangeDelay);

        _isPerformingBehavior = false;
        _hasPerformedBehavior = true;
    }
}
