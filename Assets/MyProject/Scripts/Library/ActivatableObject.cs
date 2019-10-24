using System;

using UnityEngine;

public interface IActivatableObject
{
    bool ActiveSelf { get; }

    void SetActive(bool activate);
}

public class ActivatableObject : MonoBehaviour, IActivatableObject
{
    public bool ActiveSelf { get; private set; }

    public void SetActive(bool activate)
    {
        if(activate && !ActiveSelf)
        {
            ActiveSelf = activate;
            OnActive?.Invoke();
        }
        else if(!activate && ActiveSelf)
        {
            ActiveSelf = activate;
            OnDeactive?.Invoke();
        }
    }

    protected event Action OnActive;
    protected event Action OnDeactive;
}