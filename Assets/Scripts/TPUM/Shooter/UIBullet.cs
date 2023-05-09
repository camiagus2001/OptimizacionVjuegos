using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIBullet : CustomUpdater
{
    private float count;
    public TankMovement player;  
    public TextMeshProUGUI cantidadProjectilesText;
    public TextMeshProUGUI cantidadTotalProjectilesText;
    
    void Start()
    {
        UpdateManagerUI.Instance.Add(this);
    }

    public override void Tick()
    {
        count++;
        Debug.Log("La ui se actualiz�:" + count + "veces");

        cantidadProjectilesText.text = player.cantProyectiles.ToString();
        cantidadTotalProjectilesText.text = player.cantidadTotalProyectiles.ToString();
    }
}
