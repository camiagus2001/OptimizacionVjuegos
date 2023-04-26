using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiTest : CustomUpdater
{
    private int count = 0;
    private void Start()
    {
        UpdateManagerUI.Instance.Add(this);
    }

    public override void Tick()
    {
        count++;
        Debug.Log("La ui se actualizó:" + count + "veces");
    }
}
