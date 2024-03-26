using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseState : MonoBehaviour, IState
{
    public abstract void OnStateEnter(StateMachineController stateMachineController);
    public abstract void OnStateExit(StateMachineController stateMachineController);
    public abstract void OnStateUpdate(StateMachineController stateMachineController);

    protected IEnumerator MoveToPositionCoroutine(StateMachineController stateMachineController, Vector3 targetPosition, float irisMovementSpeed)
    {
        Vector3 startPosition = stateMachineController.SharedData.IrisTS.position;
        float deltaPosition = irisMovementSpeed * Time.deltaTime;
        float lerpFactor = 0f;
        while (lerpFactor <= 1f)
        {
            lerpFactor += deltaPosition;
            stateMachineController.SharedData.IrisTS.position = Vector3.Lerp(startPosition, targetPosition, lerpFactor);
            yield return null;
        }
    }
}
