using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Idle : State
{

    public Chase chaseState;
    public override State RunCurrentState()
    {
        return chaseState;
    }
}
