//======= Copyright (c) Valve Corporation, All rights reserved. ===============
using UnityEngine;
using System.Collections;
using Valve.VR.Extras;


public class LaserPointer : SteamVR_LaserPointer
{
    private bool _ignoreInmediateInteraction = false;
    
    public void Activate()
    {
        pointer.SetActive(true);

    }
    
    public void Deactivate()
    {
        if (pointer == null)
            return;
        pointer.SetActive(false);
        _ignoreInmediateInteraction = true;
    }

    public override void OnPointerClick(PointerEventArgs e)
    {
        if (_ignoreInmediateInteraction)
        {
            _ignoreInmediateInteraction = false;
            return; 
        }
        base.OnPointerClick(e);
    }

    public override void OnPointerOut(PointerEventArgs e)
    {

        base.OnPointerOut(e);
    }
}
