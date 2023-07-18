using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class BaseScene : MonoBehaviour
{
    public Define.Scene SceneType { get; protected set; } = Define.Scene.Unknow;

    private void Awake()
    {
        Init();   
    }

    protected virtual void Init()
    {
        EventSystem evtSystem = FindObjectOfType<EventSystem>();
        
        if (evtSystem == null)
            Managers.Resource.Instantiate("UI/EventSystem").name = "@EventSystem";
        else
            evtSystem.name = "@EventSystem";
    }

    public virtual void Clear()
    {

    }
}