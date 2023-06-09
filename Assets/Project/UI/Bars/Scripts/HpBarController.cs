using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class HpBarController : MonoBehaviour, ISubscribeable<GameValue.events>, ISubscribeable<EntityBehaviour.events>
{
    [SerializeField]
    EntityBehaviour myEntity;
    GameValue barValue;
    Slider mySlider;
    public void ReceiveEvent(GameValue.events ev)
    {
        if (ev == GameValue.events.onValueChanged){
            mySlider.value = barValue.Value;
        }
    }

    public void ReceiveEvent(EntityBehaviour.events ev)
    {
        if(ev == EntityBehaviour.events.onDie){
            Destroy(this.gameObject);
        }
        if(ev == EntityBehaviour.events.onGetDamage){
            for(int i = 0;i< transform.childCount;i++){
                transform.GetChild(i).gameObject.SetActive(true);
            }
            myEntity.eventListener.Listener.UnSubscribe(EntityBehaviour.events.onGetDamage,this);
        }
    }

    void Awake()
    {
        barValue = myEntity.Hp;
        mySlider = GetComponent<Slider>();
        barValue.eventListener.Listener.Subscribe(GameValue.events.onValueChanged, this);
        myEntity.eventListener.Listener.Subscribe(EntityBehaviour.events.onDie,this);
        myEntity.eventListener.Listener.Subscribe(EntityBehaviour.events.onGetDamage,this);
    }
    void Start()
    {
        mySlider.minValue = barValue.minValue;
        mySlider.maxValue = barValue.maxValue;
        mySlider.value = barValue.Value;
    }

}
