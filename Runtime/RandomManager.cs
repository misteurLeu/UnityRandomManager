using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class ForbiddenKeyException : Exception
{
    public ForbiddenKeyException() : base() { }
    public ForbiddenKeyException(string message) : base(message) { }
    public ForbiddenKeyException(string message, Exception inner) : base(message, inner) { }
}

public partial class RandomManager: MonoBehaviour
{
    private string _mainKey = "_";

    private static RandomManager _instance;
    private Dictionary<string, RandomItem> items;
    private void Awake()
    {
        if (_instance != null)
            DestroyImmediate(_instance);
        _instance = this;

        int seed = (int)DateTimeOffset.Now.ToUnixTimeSeconds();

        items = new Dictionary<string, RandomItem>
        {
            [Instance._mainKey] = new RandomItem(seed)
        };
    }

    public static RandomManager Instance 
    {
        get
        {
            return _instance;
        }
    }

    public static void Init(List<string> keys, int? seed = null, bool clear = true)
    {
        if (clear)
        {
            if (seed != null)
                Reset((int)seed);
            else
                Reset();
        }
        foreach(string key in keys)
            AddKey(key);
    }

    public static void AddKey(string key, bool erase=false)
    {
        if (key == Instance._mainKey)
            throw new ForbiddenKeyException($"The key {Instance._mainKey} is reserved for internal use only");
        if (!RandomManager.isKeyAvaible(key) && !erase)
            throw new ForbiddenKeyException($"The key {key} is already in use, use erase=true to replace it");
        int seed = Instance.items[Instance._mainKey].Next();
        Instance.items[key] = new RandomItem(seed);
    }

    public static bool isKeyAvaible(string key)
    {
        return !string.IsNullOrEmpty(key) && !string.IsNullOrWhiteSpace(key) && !Instance.items.ContainsKey(key);
    }

    public static void Reset()
    {
        int seed = (int)DateTimeOffset.Now.ToUnixTimeSeconds();

        Instance.items.Clear();
        Instance.items[Instance._mainKey] = new RandomItem(seed);
    }

    public static void Reset(int seed)
    {
        Instance.items.Clear();
        Instance.items[Instance._mainKey] = new RandomItem(seed);
    }

    public static int NextKey(string key)
    {
        if (key == Instance._mainKey)
            throw new ForbiddenKeyException($"The key {Instance._mainKey} is reserved for internal use only");
        return Instance.items[key].Next();
    }
}