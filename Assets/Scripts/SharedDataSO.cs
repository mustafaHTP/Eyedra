using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "New Shared Data", menuName = "State Machine/Shared Data SO")]
public class SharedDataSO : ScriptableObject
{
    private float _irisMovementCircleRadius;
    private Transform _irisTS;
    private Vector3 _basePosition;

    public Transform IrisTS
    {
        get => _irisTS;
        set => _irisTS = value;
    }
    public Vector3 BasePosition
    {
        get => _basePosition;
        set => _basePosition = value;
    }
    public float IrisMovementCircleRadius
    {
        get => _irisMovementCircleRadius;
        set => _irisMovementCircleRadius = value;
    }
}

public enum TrackAreaStatus
{
    Entered,
    Stayed,
    Exited
}