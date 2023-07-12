using System;
using UnityEngine;
using UnityEngine.EventSystems;

public static class Util
{
    public static T GetOrAddComponent<T>(this GameObject go) where T : Component
    {
        T component = go.GetComponent<T>();
    
        if (component == null)
            component = go.AddComponent<T>();
    
        return component;
    }

    public static T FindChild<T>(this GameObject go, string name = null, bool recursive = false, bool searchInactive = false) where T : UnityEngine.Object
    {
        if (go == null)
            return null;

        if(recursive)
        {
            foreach(T component in go.GetComponentsInChildren<T>(searchInactive))
            {
                if(string.IsNullOrEmpty(name) || component.name == name)
                {
                    if (component != null)
                        return component;
                }
            }
        }
        else
        {
            for (int i = 0; i < go.transform.childCount; i++)
            {
                Transform transform = go.transform.GetChild(i);
                
                T component = transform.GetComponent<T>();
                if(string.IsNullOrEmpty(name) || component.name == name)
                {
                    if (component != null)
                        return component;
                }
            }
        }

        return null;
    }

    public static GameObject FindChild(this GameObject go, string name = null, bool recursive = false, bool searchInactive = false)
    {
        Transform transform = FindChild<Transform>(go, name, recursive, searchInactive);

        if (transform == null)
            return null;

        return transform.gameObject;
    }


    // 헥사값 컬러 반환( 코드 순서 : RGBA )
    public static Color HexColor(string hexCode)
    {
        Color color;
        if ( ColorUtility.TryParseHtmlString(hexCode, out color))
        {
            return color;
        }       
        Debug.LogError( "[Util::HexColor]invalid hex code - " + hexCode );
        return Color.white;
    }
}
