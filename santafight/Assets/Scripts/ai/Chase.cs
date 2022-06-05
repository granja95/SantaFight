using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chase : State
{
    public Attack attackState;
    public bool isInAttackRange;

    public override State RunCurrentState()
    {
        if(isInAttackRange)
        {
            return attackState;
        } else
        {
            return this;
        }
    }
}
