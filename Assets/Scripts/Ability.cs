using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability
{
    public bool Previewable = true;


    public bool IsPreviewPositionValid(Vector3 position)
    {
        return !Input.GetKey(KeyCode.LeftShift);
    }
}
