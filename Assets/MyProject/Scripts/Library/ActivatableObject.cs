using UnityEngine;
using UnityEngine.Events;

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
            OnActive.Invoke();
        }
        else if(!activate && ActiveSelf)
        {
            ActiveSelf = activate;
            OnDeactive.Invoke();
        }
    }

    protected UnityEvent OnActive;
    protected UnityEvent OnDeactive;
}