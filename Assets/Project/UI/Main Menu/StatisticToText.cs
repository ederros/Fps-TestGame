using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class StatisticToText : MonoBehaviour, ISubscribeable<Statistic.events>
{
    Text textUI;

    public void ReceiveEvent(Statistic.events ev)
    {
        if(ev == Statistic.events.OnValueChanged&&textUI!=null){
            textUI.text =  "Total looses " + Statistic.Instance.totalLooses + "\n";
            textUI.text += "Total wins " + Statistic.Instance.totalWins;
        }
    }

    void Awake()
    {
        textUI = GetComponent<Text>();
        Statistic.Instance.eventListener.Listener.Subscribe(Statistic.events.OnValueChanged, this);
        Statistic.Instance.eventListener.Listener.Notify(Statistic.events.OnValueChanged);
    }
}
