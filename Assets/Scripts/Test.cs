using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : CustomUpdater
{
    private int count = 0;
    private void Start()
    {
        UpdateManagerGameplay.Instance.Add(this);
    }

    public override void Tick() 
    {
        count++;
        Debug.Log("El gameplay se actualizó:" + count + "veces");
    } 
}
