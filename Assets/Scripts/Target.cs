using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    private void Update()
    {
        Vector3 mousePositionInWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePositionInWorld.z = 0f;

        transform.position = mousePositionInWorld;
    }
}
