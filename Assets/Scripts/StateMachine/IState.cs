using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IState
{
    void OnStateEnter(StateMachineController stateMachineController);
    void OnStateUpdate(StateMachineController stateMachineController);
    void OnStateExit(StateMachineController stateMachineController);
}
