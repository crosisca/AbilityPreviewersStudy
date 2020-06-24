using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Sirenix.OdinInspector;
using UnityEngine;

public class PreviewConfig
{

    [Space]
    [Title("Positioner", titleAlignment: TitleAlignments.Centered)]
    [SerializeField, Required, HideLabel, InlineProperty]
    public PreviewPositioner positioner;

    [Title("Scaler", titleAlignment:TitleAlignments.Centered)]
    [SerializeField, Required, HideLabel, InlineProperty]
    public PreviewScaler scaler;
    
    [SerializeField, ShowIf("@UnityEngine.Application.isPlaying")]
    Dictionary<string, object> cachedAbilitysValues;
    public Dictionary<string, object> CachedAbilitysValues => cachedAbilitysValues ?? (cachedAbilitysValues = new Dictionary<string, object>());

    AbilityPreviewer previewer;

    //odinhelper
    //public string InspectorName => $"Positioner:{positioner}\nScaler     :{scaler}";
    public string InspectorName => $"{positioner}\n{scaler}";

    public void Setup(AbilityPreviewer previewer)
    {
        this.previewer = previewer;
        CacheUsedVariables();
        
        positioner.Setup(previewer, this);
        scaler.Setup(previewer, this);
    }
    
    public void CacheUsedVariables ()
    {
        CachedAbilitysValues.Clear();
        
        AddAbilityDatabaseVariableNameToDictionary(positioner);
        AddAbilityDatabaseVariableNameToDictionary(scaler);
        FillDictionaryWithAbilityValues();
    }

    void AddAbilityDatabaseVariableNameToDictionary (object objToLookForAttribute)
    {
        foreach (FieldInfo fieldInfo in objToLookForAttribute.GetType().GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.FlattenHierarchy))
        {
            AbilityDatabaseValueAttribute abilityDbValue = Attribute.GetCustomAttribute(fieldInfo, typeof(AbilityDatabaseValueAttribute)) as AbilityDatabaseValueAttribute;

            //Debug.Log($"{fieldInfo.Name} is atribute {abilityDbValue != null}");

            if (abilityDbValue != null)
            {
                string key = fieldInfo.GetValue(objToLookForAttribute) as string;

                if (!string.IsNullOrEmpty(key))
                {
                    if(!CachedAbilitysValues.ContainsKey(key))
                        CachedAbilitysValues.Add(key, null);
                }
            }
        }
    }

    void FillDictionaryWithAbilityValues()
    {
        //foreach (FieldInfo fieldInfo in previewer.Ability.GetType().GetFields(BindingFlags.Public | BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy))
        //{
        //    if (CachedAbilitysValues.ContainsKey(fieldInfo.Name))
        //        CachedAbilitysValues[fieldInfo.Name] = fieldInfo.GetValue(previewer.Ability);
        //}

        //foreach (KeyValuePair<string, object> kvp in CachedAbilitysValues)
        //{
        //    FieldInfo fieldInfo = previewer.Ability.GetType().GetField(kvp.Key);
        //    if (fieldInfo != null)
        //        CachedAbilitysValues[kvp.Key] = fieldInfo.GetValue(previewer.Ability);
        //    else
        //    {
        //        PropertyInfo propertyInfo = previewer.Ability.GetType().GetProperty(kvp.Key);
        //        if (propertyInfo != null)
        //            CachedAbilitysValues[kvp.Key] = propertyInfo.GetValue(previewer.Ability);
        //    }
        //}

        foreach (string variableName in CachedAbilitysValues.Keys.ToArray())
        {
            FieldInfo fieldInfo = previewer.Ability.GetType().GetField(variableName);
            if (fieldInfo != null)
                CachedAbilitysValues[variableName] = fieldInfo.GetValue(previewer.Ability);
            else
            {
                PropertyInfo propertyInfo = previewer.Ability.GetType().GetProperty(variableName);
                if (propertyInfo != null)
                    CachedAbilitysValues[variableName] = propertyInfo.GetValue(previewer.Ability);
            }
        }
    }

    void CacheAbilityValues()
    {
        float range = (float)previewer.Ability.GetType().GetProperty("Range", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.FlattenHierarchy).GetValue(previewer.Ability);
        Debug.Log(range);
    }
    
    

    public T GetValue<T> (string variableName)
    {
        return (T)CachedAbilitysValues[variableName];
    }

    public float GetFloat (string variableName, VariableType varType)
    {
        float distance;
        switch (varType)
        {
            case VariableType.FLOAT:
                distance = GetValue<float>(variableName);
                break;
            case VariableType.INT:
                distance = GetValue<int>(variableName);
                break;
            case VariableType.VECTOR3:
                distance = GetValue<Vector3>(variableName).z;
                break;
            case VariableType.VECTOR2:
                distance = GetValue<Vector2>(variableName).y;
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

        return distance;
    }
}