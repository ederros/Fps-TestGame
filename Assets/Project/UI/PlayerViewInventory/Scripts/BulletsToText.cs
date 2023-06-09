using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class BulletsToText : MonoBehaviour, ISubscribeable<PlayerController.playerEvents>
{
    [SerializeField]
    PlayerController player;
    Text myText;
    string origin;

    public void ReceiveEvent(PlayerController.playerEvents ev)
    {
        Debug.Log("event received");
        if(ev == PlayerController.playerEvents.OnBulletCountChange){
            myText.text = origin+PlayerController.Instance.BulletCount;
        }
    }

    void Awake()
    {
        
        myText = GetComponent<Text>();
        Debug.Log("text = "+myText);
        origin = myText.text;
        
    }
    void Start()
    {
        player.Listener.Subscribe(PlayerController.playerEvents.OnBulletCountChange,this);
        player.Listener.Notify(PlayerController.playerEvents.OnBulletCountChange);
    }
}
