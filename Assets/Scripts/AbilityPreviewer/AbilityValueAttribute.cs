using System;

/// <summary>
/// If variable is not on exact type, variable must be protected
/// </summary>
[AttributeUsage(AttributeTargets.Field)]
public class AbilityDatabaseValueAttribute : Attribute { }