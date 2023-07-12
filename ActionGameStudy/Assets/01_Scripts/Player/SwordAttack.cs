using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordAttack : Skill, ISkill
{
    float atk;
    
    public Define.Weapon SetWeapon() { return Define.Weapon.Sword; }
    public float SetOriginCoolTime() { return 3; }
    
    protected override void Init()
    {
        base.Init();
    }

    public void Play()
    {

    }


}
