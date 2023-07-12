using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ILoader<T>
{
    List<T> MakeList();
}

public interface ILoader<TKey, TValue>
{
    Dictionary<TKey, TValue> MakeDict();
}

public class DataManager
{

    public void Init()
    {
    }
}
