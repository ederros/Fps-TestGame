using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public enum events{
        OnWin,
        OnLoose
    }
    static LevelManager instance;
    public static LevelManager Instance{
        get{
            return instance;
        }
    }
    public SingletoneEventListener<events> eventListener;
    bool isGameEnded = false;
    
    [SerializeField]
    Text winText;

    [SerializeField]
    Text looseText;
    int enemiesToKill = 0;
    public int EnemiesToKill{
        get {
            return enemiesToKill;
        }
        set{
            if(enemiesToKill-value == 1) PlayerController.Instance.BulletCount+=60; 
            enemiesToKill = value;
            if(enemiesToKill<=0) OnWin();
        }
    }

    IEnumerator PopupMessage(Text message){
        float iterations = 100;
        
        message.gameObject.SetActive(true);
        for(int i = 0;i<iterations;i++){
            message.rectTransform.anchoredPosition = Vector3.down*(message.rectTransform.rect.height*i/iterations - message.rectTransform.rect.height/2);  
            yield return new WaitForSeconds(2f/iterations);
        }
        
    }
    public void OnWin(){
        isGameEnded = true;
        Statistic.Instance.totalWins++;
        StartCoroutine(PopupMessage(winText));
        eventListener.Listener.Notify(events.OnWin);
    }
    public void OnLoose(){
        isGameEnded = true;
        Statistic.Instance.totalLooses++;
        StartCoroutine(PopupMessage(looseText));
        eventListener.Listener.Notify(events.OnLoose);
    }
    void Awake()
    {
        if(instance!= null&&instance!= this) Destroy(this.gameObject);
        else instance = this;
        Cursor.visible = false;
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)){
            if(!isGameEnded){
                Statistic.Instance.totalLooses++;   
                eventListener.Listener.Notify(events.OnLoose);
            }
            Statistic.Save();
            SceneManager.LoadScene("Main Menu");
        }
    }
    void Start()
    {
        
    }
}
