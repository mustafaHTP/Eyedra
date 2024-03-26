using System;
using System.Collections;
using UnityEditor;
using UnityEngine;


public class StateMachineController : MonoBehaviour
{
    [Header("Shared Data")]
    [Space(5)]
    [SerializeField] private float _irisMovementCircleRadius;

    //states
    private IState _currentState;
    private IdleState _idleState;
    private ResetState _resetState;
    private TrackState _trackState;

    private Eyedra _eyedra;
    private SharedDataSO _sharedData;

    public SharedDataSO SharedData { get => _sharedData; private set => _sharedData = value; }
    public TrackAreaStatus TrackAreaStatus { get => _eyedra.TrackAreaStatus; }
    public IdleState IdleState => _idleState;
    public TrackState TrackState => _trackState;
    public ResetState ResetState => _resetState;

    public void ChangeState(IState nextState)
    {
        _currentState.OnStateExit(this);
        _currentState = nextState;
        _currentState.OnStateEnter(this);
    }

    private void Awake()
    {
        _eyedra = GetComponentInParent<Eyedra>();

        InitSharedData();
        InitStates();
    }

    private void Update()
    {
        _currentState.OnStateUpdate(this);
        print($"Current State: {_currentState.GetType().Name}");
    }

    private void InitSharedData()
    {
        SharedDataSO sharedData = ScriptableObject.CreateInstance<SharedDataSO>();

        sharedData.IrisTS = FindIrisTS();
        sharedData.BasePosition = transform.position;
        sharedData.IrisMovementCircleRadius = _irisMovementCircleRadius;

        string sharedDataName = $"{gameObject.name}_SharedData.asset";
        string path = $"Assets/Data/{sharedDataName}";

        AssetDatabase.CreateAsset(sharedData, path);

        SharedData = sharedData;
    }

    private void InitStates()
    {
        _idleState = GetComponent<IdleState>();
        _trackState = GetComponent<TrackState>();
        _resetState = GetComponent<ResetState>();

        _currentState = IdleState;
    }

    private Transform FindIrisTS()
    {
        Transform irisTS = null;
        foreach (Transform child in transform)
        {
            if (child.CompareTag("Iris")) irisTS = child;
        }

        return irisTS;
    }
}