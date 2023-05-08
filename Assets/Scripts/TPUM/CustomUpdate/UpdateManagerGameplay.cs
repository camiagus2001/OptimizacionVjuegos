using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateManagerGameplay : MonoBehaviour
{
    private List<CustomUpdater> gameplayUpdater;

    public static UpdateManagerGameplay Instance { get; private set; }

    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
            gameplayUpdater = new List<CustomUpdater>();
        }
    }

    private void Update()
    {
        var count = gameplayUpdater.Count;

        for (int i = 0; i < count; i++)
        {
            gameplayUpdater[i].Tick();
        }
    }

    public void Add(CustomUpdater entity)
    {
        gameplayUpdater.Add(entity);
    }

    public void Remove(CustomUpdater entity)
    {
        gameplayUpdater.Remove(entity);
    }
}
