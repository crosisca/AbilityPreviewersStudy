using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability
{
    public bool Previewable = true;

    public float areaRadiusFloat = 3.5f;

    public int areaRadiusInt = 2;

    public Vector3 dashArea = Vector3.one * 3 + Vector3.forward * 5;

    public Vector3 areaSizeOfOne = Vector3.one;

    public float maxDistanceFromOrigin = 10;

    public bool IsPreviewPositionValid(Vector3 position)
    {
        return !Input.GetKey(KeyCode.LeftShift);
    }
}
