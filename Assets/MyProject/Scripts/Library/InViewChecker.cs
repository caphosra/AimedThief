using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class InViewChecker : MonoBehaviour
{
    [SerializeField]
    private List<ActivatableObject> components;

    private bool inViewFlag = false;

    void Update()
    {
        bool isInView = Inview(transform.position);
        if(!inViewFlag && isInView)
        {
            inViewFlag = true;
            SetAllComponentsActive(true);
        }
        else if(inViewFlag && !isInView)
        {
            inViewFlag = false;
            SetAllComponentsActive(false);
        }
    }

    private bool Inview(Vector2 pos)
    {
        return -9.5f <= pos.x && pos.x <= 9.5f && -5f <= pos.y && pos.y <= 5f;
    }

    private void SetAllComponentsActive(bool active)
    {
        foreach(var component in components)
        {
            component.SetActive(active);
        }
    }
}
