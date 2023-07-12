using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Dictionary<Define.Skill, ISkill> skillDict = new Dictionary<Define.Skill, ISkill>();

    private void Awake()
    {
        Init();
    }

    protected virtual void Init()
    {

    }

    private void Update()
    {
        
    }
}
