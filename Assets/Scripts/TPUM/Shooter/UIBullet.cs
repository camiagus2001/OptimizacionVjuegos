using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIBullet : CustomUpdater
{
    private float count;
    public Gun gun;
    public int cantProyectiles;
    public int cantidadTotalProyectiles;
    public TextMeshProUGUI cantidadProjectilesText;
    public TextMeshProUGUI cantidadTotalProjectilesText;
    
    void Start()
    {
        UpdateManagerUI.Instance.Add(this);
    }

    public override void Tick()
    {
        count++;
        Debug.Log("La ui se actualizó:" + count + "veces");

        cantidadProjectilesText.text = gun.cantProyectiles.ToString();
        cantidadTotalProjectilesText.text = gun.cantidadTotalProyectiles.ToString();
    }
}
