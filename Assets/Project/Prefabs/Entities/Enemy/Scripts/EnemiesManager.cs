using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesManager : MonoBehaviour
{
    [SerializeField]
    int enemiesCount;

    [SerializeField]
    GameObject enemyPrefab;
    static EnemiesManager instance;
    public static EnemiesManager Instance{
        get{
            return instance;
        }
    }
    [SerializeField]
    List<EnemyContoller> enemies;

    [SerializeField]
    List<GunScriptableObject> guns;
    void SpawnEnemies(){
        for(int i = 0; i<enemiesCount; i++){
            RaycastHit rHit = new RaycastHit();
            Physics.Raycast(new Vector3(Random.Range(-50f,50),20,Random.Range(-50f,50)),Vector3.down,out rHit);
            enemies.Add(Instantiate(
                enemyPrefab,
                rHit.point+Vector3.up*2,
                Quaternion.Euler(0,Random.value*360,0),
                transform
            ).GetComponent<EnemyContoller>());
        }
    }
    void SetRandomWeapon(){
        for(int i = 0; i<enemies.Count; i++){
            enemies[i].GunContainer.MyGun = guns[Random.Range(0,guns.Count)];
        }
    }

    void Start()
    {
        SpawnEnemies();
        SetRandomWeapon();
    }
}
