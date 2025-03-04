using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System;

[Serializable]
public class ForbiddenKeyException : Exception
{
    public ForbiddenKeyException() : base() { }
    public ForbiddenKeyException(string message) : base(message) { }
    public ForbiddenKeyException(string message, Exception inner) : base(message, inner) { }
}

public class RandomManager: MonoBehaviour
{
    private string _mainKey = "_";

    private static RandomManager _instance;

    public static RandomManager Instance { get => _instance; }

    public int MainSeed
    {
        get { return _items[_mainKey].Seed;}
    }

    private Dictionary<string, RandomItem> _items;

    public Dictionary<string, RandomItem> Items {
        get {
            return _items.Where(kvp => kvp.Key != _mainKey).ToDictionary(i => i.Key, i => i.Value);
        }
    }

    private void Awake()
    {
        if (_instance != null && _instance != this)
            DestroyImmediate(_instance);
        _instance = this;

        int seed = (int)DateTimeOffset.Now.ToUnixTimeSeconds();

        _items = new Dictionary<string, RandomItem>
        {
            [_mainKey] = new RandomItem(seed)
        };
    }

    public void Init(List<string> keys, int? seed = null, bool clear = true)
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

    public void AddKey(string key, bool erase=false)
    {
        if (key == _mainKey)
            throw new ForbiddenKeyException($"The key {_mainKey} is reserved for internal use only");
        if (!isKeyAvaible(key) && !erase)
            throw new ForbiddenKeyException($"The key {key} is already in use, use erase=true to replace it");
        int seed = _items[_mainKey].Next();
        _items[key] = new RandomItem(seed);
    }

    public bool isKeyAvaible(string key)
    {
        return !string.IsNullOrEmpty(key) && !string.IsNullOrWhiteSpace(key) && !_items.ContainsKey(key);
    }

    public void Reset()
    {
        int seed = (int)DateTimeOffset.Now.ToUnixTimeSeconds();

        _items.Clear();
        _items[_mainKey] = new RandomItem(seed);
    }

    public void Reset(int seed)
    {
        _items.Clear();
        _items[_mainKey] = new RandomItem(seed);
    }

    public int NextKey(string key)
    {
        if (key == _mainKey)
            throw new ForbiddenKeyException($"The key {_mainKey} is reserved for internal use only");
        return _items[key].Next();
    }

    public int getValue(string key)
    {
        if (key == _mainKey)
            throw new ForbiddenKeyException($"The key {_mainKey} is reserved for internal use only");
        return _items[key].Value;
    }

    public List<string> Export()
    {
        List<string> toExport = new List<string>();

        foreach (KeyValuePair<string, RandomItem> kvp in _items)
            toExport.Add($"{kvp.Key};{kvp.Value.Export()}");

        return toExport;
    }

    public void Import(List<string> toImport, bool clear = true)
    {
        if (clear)
            _items.Clear();

        foreach (string item in toImport)
        {
            string[] vals = item.Split(';');
            _items.Add(vals[0], new RandomItem(int.Parse(vals[1]), int.Parse(vals[2])));
        }
    }
}