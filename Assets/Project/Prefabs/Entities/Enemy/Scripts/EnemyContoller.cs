using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyContoller : EntityBehaviour
{
    [SerializeField]
    private float sightRadius = 10;

    [SerializeField]
    private float crookedness = 0.1f;

    [SerializeField]
    private float forgetRadius = 50;

    [SerializeField]
    private float attackApproachDist = 8;

    [SerializeField]
    private float targetCheckTick = 1;

    [SerializeField]
    private EntityBehaviour target;
    public EntityBehaviour Target{
        get{
            return target;   
        }
        set {
            target = value;
        }
    }

    private float distanceToTarget = 0;

    private void DropLoot(){
        GameObject container = gunContainer.gameObject;

        FloorGunContainer floorContainer = container.AddComponent<FloorGunContainer>();
        floorContainer.MyGun = gunContainer.MyGun;
        GunContainer.MyGun = null;
        Destroy(container.GetComponent<HandGunContainer>());
    }
    protected override void Die()
    {
        LevelManager.Instance.EnemiesToKill--;
        DropLoot();
    }

    protected override void Attack()
    {
        if(target == null) return;
        gunContainer.TryFire(this, (lookTarget.position+targetLastPos)/2+new Vector3(Random.value-0.5f,Random.value-0.5f,Random.value-0.5f)*distanceToTarget*crookedness);
        
    }
    private Vector3 targetLastPos;
    protected override void LookAt()
    {
        if(target == null) return;
        Vector3 vec = (Vector3)(lookTarget.position - transform.position);
        transform.forward = new Vector3(vec.x, 0 , vec.z);
        lookTarget.position = target.transform.position;
        
    }
    IEnumerator CheckForTarget(){
        while(!isDied){
            if(target == null){
                foreach(Collider c in Physics.OverlapSphere(transform.position, sightRadius, FireLayers)){
                    if(c.gameObject.layer == 6){//6 is player layer
                        target = c.GetComponent<EntityBehaviour>();
                        if(target!=null){
                            distanceToTarget = (target.transform.position - transform.position).magnitude;
                            break;
                        }
                    }
                }
            }
            else if (distanceToTarget>forgetRadius){
                target = null;
            } else{
                distanceToTarget = (target.transform.position - transform.position).magnitude;
                targetLastPos = target.transform.position;
                
            }
            yield return new WaitForSeconds(targetCheckTick);
        }
    }

    public override void GetDamage(float value)
    {
        base.GetDamage(value);
        target = PlayerController.Instance;
    }
    protected override void Movement()
    {
        if(target == null) return;
        if(distanceToTarget<attackApproachDist) {
            myAnimator.SetFloat("SpeedZ",0);
            return;
        }

        Vector3 moveVec = (target.transform.position - transform.position).normalized*moveSpeed;
        rb.velocity = new Vector3(moveVec.x,rb.velocity.y,moveVec.z);
        //Vector2 animationPoint = Vector2.Lerp(new Vector2(myAnimator.GetFloat("SpeedX"), myAnimator.GetFloat("SpeedZ")),moveVec/runSpeed*curSpeed,Time.deltaTime*10);
        myAnimator.SetFloat("SpeedZ",0.2f*moveSpeed);
    }
    private void Start() {
        StartCoroutine(CheckForTarget());
        LevelManager.Instance.EnemiesToKill++;
    }

    protected override void Awake()
    {
        base.Awake();
        rb = GetComponent<Rigidbody>();
        
    }
}
