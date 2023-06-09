using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Gun", menuName = "ScriptableObjects/CreateGun", order = 1)]
public class GunScriptableObject : ScriptableObject
{

    [SerializeField]
    Vector3 firePoint;

    public Vector3 FirePoint{
        get{
            return firePoint;
        }
    }

    [SerializeField]
    float spread;// fire spread in tan
    public float Spread{
        get{
            return spread;
        }
    }

    [SerializeField] 
    GameObject prefab;
    public GameObject Prefab{
        get{
            return prefab;
        }
    }

    [SerializeField] 
    Sprite icon;
    public Sprite Icon{
        get{
            return icon;
        }
    }

    [SerializeField]
    protected float damage;
    public float Damage{
        get{
            return damage;
        }
    }

    [SerializeField]
    float fireRate; // delay between shoots
    public float FireRate{
        get{
            return fireRate;
        }
    }
    // [SerializeField]
    // bool isAutoFire = true;
    // public bool IsAutoFire{
    //     get{
    //         return isAutoFire;
    //     }
    // }
}
