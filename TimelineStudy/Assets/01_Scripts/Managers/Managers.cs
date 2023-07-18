using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Managers : MonoBehaviour
{
    static Managers Instance { get { Init(); return s_instance; } }
    static Managers s_instance;
    
    DataManager _data = new DataManager();
    GameManager _game = new GameManager();
    ResourceManager _resource = new ResourceManager();
    SceneManagerEx _scene = new SceneManagerEx();
    SoundManager _sound = new SoundManager();
    UIManager _ui = new UIManager();
    
    public static DataManager Data { get { return Instance._data; } }
    public static GameManager Game { get { return Instance._game; } }
    public static ResourceManager Resource { get { return Instance._resource; } }
    public static SceneManagerEx Scene { get { return Instance._scene; } }
    public static SoundManager Sound { get { return Instance._sound; } }
    public static UIManager UI { get { return Instance._ui; } }

    static GameObject _arCamera;

    public static void SetActiveToCamera(bool active)
    {
        if (_arCamera == null)
            _arCamera = GameObject.Find("ARCamera");

        _arCamera.SetActive(active);
    }

    void Start()
    {
        Init();    
    }

    static void Init()
    {
        if(s_instance == null)
        {
            GameObject go = GameObject.Find("@Managers");

            if(go == null)
            {
                go = new GameObject { name = "@Managers" };
                go.AddComponent<Managers>();
            }

            s_instance = go.GetComponent<Managers>();
            DontDestroyOnLoad(go);
            
            s_instance._data.Init();
            s_instance._game.Init();
            s_instance._sound.Init();
        }
    }

    public static void Clear()
    {
        UI.Clear();
        Scene.Clear();
        Sound.Clear();
    }
}