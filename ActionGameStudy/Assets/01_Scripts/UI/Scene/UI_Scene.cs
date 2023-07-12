using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Scene : UI_Base
{
    private void Awake()
    {
        Init();
    }

    protected override void Init()
    {
        Managers.UI.SetCanvas(gameObject, true);
    }
}