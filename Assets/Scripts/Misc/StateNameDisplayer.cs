using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StateNameDisplayer : MonoBehaviour
{
    [SerializeField] private StateMachineController _stateMachineController;
    [SerializeField] private TextMeshProUGUI _stateNameTMP;

    private void Update()
    {
        _stateNameTMP.SetText(_stateMachineController.CurrentStateName);
    }
}
