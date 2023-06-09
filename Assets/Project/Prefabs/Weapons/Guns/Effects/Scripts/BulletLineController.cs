using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class BulletLineController : MonoBehaviour
{
    [SerializeField]
    float lifetime;
    float curLifeTime;
    float width;
    LineRenderer lineRenderer;
    void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        width = lineRenderer.widthMultiplier;
        curLifeTime = lifetime;
    }
    // Update is called once per frame
    void Update()
    {
        curLifeTime-=Time.deltaTime;
        curLifeTime = Mathf.Clamp(curLifeTime,0,lifetime);
        lineRenderer.widthMultiplier= curLifeTime/ lifetime*width;
        
        if(curLifeTime == 0){
            Destroy(this.gameObject);
        }
    }

}
