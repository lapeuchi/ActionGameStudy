using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : MonoBehaviour
{
    protected Define.Weapon weapon;
    public bool isPlay = false;
    public float originCoolTime;

    private void Awake()
    {
        Init();
    }

    protected virtual void Init()
    {
        isPlay = false;
        weapon = GetComponent<ISkill>().SetWeapon();
    }

    protected void Update()
    {
        
    }
}
