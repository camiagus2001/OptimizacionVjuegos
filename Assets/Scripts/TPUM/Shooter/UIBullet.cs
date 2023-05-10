using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIBullet : CustomUpdater
{
    public Player player;  
    public TextMeshProUGUI cantidadProjectilesText;
    public TextMeshProUGUI cantidadTotalProjectilesText;
    
    void Start()
    {
        UpdateManagerUI.Instance.Add(this);
    }

    public override void Tick()
    {
        cantidadProjectilesText.text = player.cantProyectiles.ToString();
        cantidadTotalProjectilesText.text = player.cantidadTotalProyectiles.ToString();
    }
}
