using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct SingletoneEventListener<T> where T:System.Enum
{
    private Observer<T> listener;
    public Observer<T> Listener{
        get{
            if(listener == null) {
                listener = new Observer<T>();
                Debug.Log("new observer "+ listener);
            }
            return listener;
        }
    }
}

