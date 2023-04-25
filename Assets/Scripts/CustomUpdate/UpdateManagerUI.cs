using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateManagerUI : MonoBehaviour
{
    private List<CustomUpdater> uiUpdater;

    public static UpdateManagerUI Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
            uiUpdater = new List<CustomUpdater>();
        }
    }

    private void Update()
    {
        var count = uiUpdater.Count;

        for (int i = 0; i < count; i++)
        {
            uiUpdater[i].Tick();
        }
    }

    public void Add(CustomUpdater entity)
    {
        uiUpdater.Add(entity);
    }

    public void Remove(CustomUpdater entity)
    {
        uiUpdater.Remove(entity);
    }
}
