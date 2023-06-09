using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandGunContainer : GunContainer
{
    

    [SerializeField]
    private GameObject BulletLine;
    protected float lastShootTime = 0;
    protected virtual void Fire(LayerMask layers,Vector3 endPoint){
        lastShootTime = Time.time;
        RaycastHit rHit = new RaycastHit();

        float maxDistance = 100;
        Vector3 startPoint =  transform.position+ transform.rotation* MyGun.FirePoint;
        Vector3 direction = (endPoint - startPoint).normalized+new Vector3(Random.value-0.5f,Random.value-0.5f,Random.value-0.5f)*2 *MyGun.Spread;
        Physics.Raycast(startPoint, direction, out rHit, maxDistance, layers);
        if(rHit.collider == null){
            rHit.point = startPoint + direction.normalized * maxDistance;
        }

        if(BulletLine!=null){
            Instantiate(BulletLine,startPoint,Quaternion.identity).GetComponent<LineRenderer>().SetPosition(1,rHit.point- startPoint);
        }

        if(rHit.collider == null) return;
        EntityBehaviour entity;
        if(rHit.collider.TryGetComponent<EntityBehaviour>(out entity))
            entity.GetDamage(MyGun.Damage);
    }

    public virtual bool TryFire(EntityBehaviour sender, Vector3 endPoint){
        if(Time.time<lastShootTime+MyGun.FireRate) return false;
        Fire(sender.FireLayers, endPoint);
        return true;
    }

}
