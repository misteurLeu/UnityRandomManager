using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class RandomItem: System.Random
{
    private int? _value = null;

    public int Value
    {
        get {
            if (_value == null)
                Next();
            return (int)_value;
        }
    }

    public int Seed { get; private set; }
    public int CallNumber { get; private set;}

    public override int Next()
    {
        CallNumber += 1;
        _value = base.Next();
        return Value;
    }

    public RandomItem(int seed): base(seed)
    {
        // Init the class randomItem with a given seed
        Seed = seed;
        CallNumber = 0;
    }
    
    public RandomItem(int seed, int numberOfCalls): base(seed)
    {
        // Init the class randomItem with a given seed and set the random to a given position, usefull for save/load.
        Seed = seed;
        for(int i = 0; i < numberOfCalls; i++)
            Next();
    }

    public string Export()
    {
        return $"{Seed};{CallNumber}";
    }
}