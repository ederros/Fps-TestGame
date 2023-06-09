using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InvCellController : MonoBehaviour
{
    [SerializeField]
    Image foreground;

    [SerializeField]
    Image background;

    public void SetForeground(Sprite newForeground){
        foreground.sprite = newForeground;
        if(foreground.sprite==null){
            foreground.color = new Color(0,0,0,0);
        }else
        {
            foreground.color = Color.white;
        }
    }
    public void OnSelect(){
        GetComponent<RectTransform>().localScale=Vector3.one* 1.1f;
    }
    public void OnUnSelect(){
        GetComponent<RectTransform>().localScale=Vector3.one* 1f;
    }

}
