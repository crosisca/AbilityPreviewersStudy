using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

//public class PositionerConfig
//{
//    [SerializeField]
//    public AbilityPreviewPositionMode PositionMode;

//    [SerializeField]
//    public PreviewPositioner positioner;
//}

public class PreviewConfig<T>
{
    [SerializeField]
    public PreviewPositioner positioner;

    [SerializeField]
    public PreviewScaler scaler;

    [SerializeField]
    public Material Material { get; set; }

    //[SerializeField]
    //public string VariableName { get; set; }

    //[SerializeField, /*, ReadOnly*/]
    //public T Value { get; set; }

    Dictionary<string, T> _variables;
    [SerializeField/*, ReadOnly*/]
    public Dictionary<string, T> Variables { get; set; }
    //{
    //    get
    //    {
    //        if(_variables == null)
    //            _variables = new Dictionary<string, T>();
    //        return _variables;
    //    }
    //    set { _variables = value; }
    //}
}