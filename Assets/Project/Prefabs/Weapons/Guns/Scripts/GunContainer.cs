using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunContainer : MonoBehaviour
{
    [SerializeField]
    protected GameObject prefabInstance;

    [SerializeField]
    private GunScriptableObject myGun;
    public GunScriptableObject MyGun{
        get{
            return myGun;
        }
        set{
            Destroy(prefabInstance);
            myGun = value;
            if(myGun==null) return;
            prefabInstance = Instantiate(myGun.Prefab,transform.position,transform.rotation,transform);

        }
    }
}
