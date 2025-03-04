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

    /*
     return a random number between 0 and max
     max is exclusive
     */
    public int Range(int max)
    {
        if (max <= 0)
            throw new ArgumentOutOfRangeException($"max should be strictly superior to 0, {max} given");
        return Range(0, max);
    }

    /*
     return a random number between min and max
     min is inclusive
     max is exclusive
     */
    public int Range(int min, int max)
    {
        if (max <= 0)
            throw new ArgumentOutOfRangeException($"max should be strictly superior to min, max = {max}, min = {min}");
        return Next() % (max - min) + min;
    }

    // return a random float between 0 and 1
    public float FRand()
    {
        return Next() / (float)int.MaxValue;
    }
    /*
     return a random number between 0 and max
     */
    public float Range(float max)
    {
        if (max <= 0)
            throw new ArgumentOutOfRangeException($"max should be strictly superior to 0, {max} given");

        return FRand() * max;
    }

    /*
     return a random number between min and max
     min is inclusive
     max is exclusive
     */
    public float Range(float min, float max)
    {
        if (max <= 0)
            throw new ArgumentOutOfRangeException($"max should be strictly superior to min, max = {max}, min = {min}");
        return FRand() * (max - min) + min;
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