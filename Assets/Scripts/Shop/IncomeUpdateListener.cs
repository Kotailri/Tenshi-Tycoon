using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IncomeUpdateListener : MonoBehaviour
{
    public virtual void Awake()
    {
        Subscribe();
    }

    public virtual void OnDisable()
    {
        Unsubscribe();
    }

    private void Subscribe()
    {
        Global.incomeListeners.Add(this);
    }

    private void Unsubscribe()
    {
        Global.incomeListeners.Remove(this);
    }

    public abstract void OnIncomeUpdate();
}
