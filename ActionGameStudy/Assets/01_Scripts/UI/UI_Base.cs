using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public abstract class UI_Base : MonoBehaviour
{
    protected Dictionary<Type, UnityEngine.Object[]> _objects = new Dictionary<Type, UnityEngine.Object[]>();

    protected abstract void Init();

    protected void Bind<T>(Type type, bool searchInactive = false) where T : UnityEngine.Object
    {
        string[] names = Enum.GetNames(type);
        UnityEngine.Object[] objects = new UnityEngine.Object[names.Length];
        
        _objects.Add(typeof(T), objects);

        for (int i = 0; i < objects.Length; i++)
        {
            if (typeof(T) == typeof(GameObject))
                objects[i] = gameObject.FindChild(names[i], true, searchInactive);
            else
                objects[i] = gameObject.FindChild<T>(names[i], true, searchInactive);

            if (objects[i] == null)
                Debug.Log($"Faild : Not found this object {names[i]}");
        }
    }

    protected void BindObject(Type type, bool searchInactive = false) { Bind<GameObject>(type, searchInactive); }
    protected void BindSlider(Type type, bool searchInactive = false) { Bind<Slider>(type, searchInactive); }
    protected void BindImage(Type type, bool searchInactive = false) { Bind<Image>(type, searchInactive); }
    protected void BindText(Type type, bool searchInactive = false) { Bind<TMP_Text>(type, searchInactive); }
    protected void BindInputField(Type type, bool searchInactive = false) { Bind<TMP_InputField>(type, searchInactive); }
    protected void BindButton(Type type, bool searchInactive = false) { Bind<Button>(type, searchInactive); }
    protected void BindDropdown(Type type, bool searchInactive = false) {Bind<TMP_Dropdown>(type, searchInactive); }

    public T Get<T>(int idx) where T : UnityEngine.Object
    {
        UnityEngine.Object[] objects = null;

        if (_objects.TryGetValue(typeof(T), out objects) == false)
        {
            Debug.Log($"Faild get object {typeof(T)}");
            return null;
        }

        return objects[idx] as T;
    }

    public GameObject GetObject(int idx) { return Get<GameObject>(idx); }
    public Slider GetSlider(int idx) { return Get<Slider>(idx); }
    public Image GetImage(int idx) { return Get<Image>(idx); }
    public TMP_Text GetText(int idx) { return Get<TMP_Text>(idx); }
    public TMP_InputField GetInputFieldint(int idx) { return Get<TMP_InputField>(idx); }
    public Button GetButton(int idx) { return Get<Button>(idx); }
    protected TMP_Dropdown GetDropdown(int idx) { return Get<TMP_Dropdown>(idx); }
}
