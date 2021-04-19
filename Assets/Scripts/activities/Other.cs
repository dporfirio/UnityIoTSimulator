using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class Other : Activity
{

    // Start is called before the first frame update
    public Other()
    {
        this.command = "other";
        this.description = "walking";
    }

    public override bool CheckActivityConditions() {
        return true;
    }

    public override void Act() {

    }

    public override void EndAct() {

    }
}
