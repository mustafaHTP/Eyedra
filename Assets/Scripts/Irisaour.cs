using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Irisaour : MonoBehaviour
{
    private TrackAreaStatus _trackAreaStatus;

    public TrackAreaStatus TrackAreaStatus 
    { 
        get => _trackAreaStatus; 
        private set => _trackAreaStatus = value;
    }

    private void Awake()
    {
        _trackAreaStatus = TrackAreaStatus.Exited;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Target target))
        {
            TrackAreaStatus = TrackAreaStatus.Entered;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Target target))
        {
            TrackAreaStatus = TrackAreaStatus.Stayed;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Target target))
        {
            TrackAreaStatus = TrackAreaStatus.Exited;
        }
    }
}
