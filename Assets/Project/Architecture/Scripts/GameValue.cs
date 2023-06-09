using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameValue
{
    public enum events{
        onValueChanged,
        onValueMin,
        onValueMax
    }

    public SingletoneEventListener<events> eventListener = new SingletoneEventListener<events>();

    [SerializeField]
    private float value;
    public float Value{
        get {
            return value;
        }
    }
    public float maxValue{get; private set;}
    public float minValue{get; private set;}

    public GameValue(float value){
        this.value = value;
        maxValue = value;
        minValue = 0;
    }

    public void Init(){
        maxValue = value;
        minValue = 0;
    }
    
    public void Add(float value){ 
        this.value += value;
        if(this.value>=maxValue){
            this.value = maxValue;
            eventListener.Listener.Notify(events.onValueMax);
        }
        eventListener.Listener.Notify(events.onValueChanged);
    }
    public void Sub(float value){ 
        this.value -= value;
        if(this.value<=minValue){
            this.value = minValue;
            eventListener.Listener.Notify(events.onValueMin);
        }
        eventListener.Listener.Notify(events.onValueChanged);
    }
    public static GameValue operator+(GameValue a, float b){
        a.ChangeValue(b);
        return a;
    }
    public static GameValue operator-(GameValue a, float b){
        a.ChangeValue(-b);
        return a;
    }
    public void ChangeValue(float value){
        if(value>=0) Add(value);
        else Sub(-value);
    }
}
