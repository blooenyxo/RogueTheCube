using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// base stat class. used to get a value (GetValue()) using the modifiers to add on top of the base value.
/// </summary>
[System.Serializable]
public class Stat
{
    [SerializeField]
    private int baseValue = 0;

    private List<int> modifiers = new List<int>();

    /// <summary>
    /// returns the value of a stat after adding all the modifiers together
    /// </summary>
    /// <returns>finalValue</returns>
    public int GetValue()
    {
        int finalValue = baseValue;
        modifiers.ForEach(x => finalValue += x);
        return finalValue;
    }
    /// <summary>
    /// use to add additional values to the base one for each stat.
    /// </summary>
    /// <param name="modifier">value to be added to the modifiers list</param>
    /// 
    public void AddModifier(int modifier)
    {
        if (modifier != 0)
            modifiers.Add(modifier);
    }
    /// <summary>
    /// same but for removing
    /// </summary>
    /// <param name="modifier">value to be removed</param>
    public void RemoveModifier(int modifier)
    {
        if (modifier != 0)
            modifiers.Remove(modifier);
    }
}
