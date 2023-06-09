using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EntityBehaviour : MonoBehaviour, ISubscribeable<GameValue.events>
{
    public enum events{
        onDie,
        onGetDamage
    }
    [SerializeField]
    protected HandGunContainer gunContainer;
    public HandGunContainer GunContainer{
        get{
            return gunContainer;
        }
    }
    public SingletoneEventListener<events> eventListener = new SingletoneEventListener<events>();
    [SerializeField]
    protected GameValue hp;
    public GameValue Hp{
        get{
            return hp;
        }
    }
    [SerializeField]
    protected LayerMask fireLayers;
    public LayerMask FireLayers{
        get{
            return fireLayers;
        }
    }
    [SerializeField]
    protected float moveSpeed;

    [SerializeField]
    protected float rotateSpeed;

    [SerializeField] 
    protected Animator myAnimator;

    [SerializeField]
    InventoryManager<GunScriptableObject> myInventory;
    public InventoryManager<GunScriptableObject> MyInventory{
        get{
            return myInventory;
        }
    }

    [SerializeField]
    protected Transform lookTarget;
    protected bool isDied = false;
    protected Rigidbody rb;
    protected abstract void Die();
    protected abstract void Movement();
    protected abstract void LookAt();
    protected abstract void Attack();
    public virtual void GetDamage(float value){
        eventListener.Listener.Notify(events.onGetDamage);

        hp-=value;
    }

    protected virtual void Awake() {
        hp.Init();
        hp.eventListener.Listener.Subscribe(GameValue.events.onValueMin,this);
        
    }

    public virtual void ReceiveEvent(GameValue.events ev)
    {
        if(ev == GameValue.events.onValueMin && !isDied) {
            Die();
            isDied = true;
            myAnimator.SetTrigger("IsDied");
            eventListener.Listener.Notify(events.onDie);
            Destroy(GetComponent<Rigidbody>());
            foreach(Collider c in GetComponents<Collider>()){
                Destroy(c);
            }
            Destroy(GetComponent<EntityBehaviour>());
        }
    }

    protected virtual void Update()
    {
        if(isDied) return;
        Movement();
        LookAt();
        Attack();
    }
}
