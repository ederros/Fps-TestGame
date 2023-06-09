using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : EntityBehaviour
{
    public enum playerEvents{
        OnFire,
        OnBulletCountChange
    }

    private Observer<playerEvents> listener;
    public Observer<playerEvents> Listener{
        get{
            if(listener == null) {
                listener = new Observer<playerEvents>();
                Debug.Log("new observer "+ listener);
            }
            return listener;
        }
    }
    [SerializeField]
    float interactionDistance;
    [SerializeField]
    Transform foot;

    [SerializeField]
    float jumpForce = 10;
    
    [SerializeField]
    float runSpeed= 5;
    
    [SerializeField]
    Transform camJoint;
    private float camY = 0;

    [SerializeField]
    int bulletCount;
    public int BulletCount{
        get{
           return bulletCount;
        }
        set{
            bulletCount=value;
            listener.Notify(playerEvents.OnBulletCountChange);
        }
    }
    [SerializeField]
    private float maxAngleY = 60;

    static PlayerController instance;
    static public PlayerController Instance{
        get{
            return instance;
        }
    }

    protected override void Die()
    {
        Camera.main.transform.LookAt(transform.position-transform.forward);
        LevelManager.Instance.OnLoose();
    }
    protected override void Movement()
    {
        //Move
        Vector2 moveVec = new Vector2();
        moveVec.x = Input.GetAxisRaw("Horizontal");
        moveVec.y = Input.GetAxisRaw("Vertical");
        moveVec.Normalize();
        float curSpeed = moveSpeed;
        if (Input.GetKey(KeyCode.LeftShift)) 
            curSpeed = runSpeed;
        
        Vector3 velos = (transform.right * moveVec.x + transform.forward * moveVec.y) * curSpeed;
        rb.velocity = new Vector3(velos.x, rb.velocity.y, velos.z);

        //Jump
        if(Input.GetKeyDown(KeyCode.Space)){
            bool isGrounded = false;
            foreach(Collider c in Physics.OverlapSphere(foot.position,0.1f)){
                if(c.tag == "Floor") isGrounded = true;
            }
            if(isGrounded){
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            }
            
        }
            
        //Rotation
        Vector2 deltaMouse = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y")*2);
        transform.Rotate(new Vector3(0,deltaMouse.x,0)*rotateSpeed);

        camY+=deltaMouse.y;
        camY=Mathf.Clamp(camY, -maxAngleY, maxAngleY);
        camJoint.transform.rotation = Quaternion.Euler(new Vector3(-camY,camJoint.transform.rotation.eulerAngles.y,0));

        //Animator
        Vector2 animationPoint = Vector2.Lerp(new Vector2(myAnimator.GetFloat("SpeedX"), myAnimator.GetFloat("SpeedZ")),moveVec/runSpeed*curSpeed,Time.deltaTime*10);
        myAnimator.SetFloat("SpeedX",animationPoint.x);
        myAnimator.SetFloat("SpeedZ",animationPoint.y);
    }

    protected override void Attack()
    {
        if(Input.GetMouseButton(0)&&bulletCount>0){
            if(gunContainer.TryFire(this, lookTarget.position)){
                
                BulletCount--;
                Listener.Notify(playerEvents.OnFire);
            }
        }
    }

    protected override void LookAt()
    {
        RaycastHit rHit = new RaycastHit();
        float maxDistance = 100;
        Physics.Raycast(camJoint.position, camJoint.forward, out rHit, maxDistance, fireLayers);
        if(rHit.collider == null){
            rHit.point = camJoint.position + camJoint.forward * maxDistance;
        }else{
            if(Input.GetKeyDown(KeyCode.F)&&(rHit.point-transform.position).magnitude<interactionDistance){
                IInteractable interactable = rHit.collider.GetComponent<IInteractable>();
                if(interactable!=null){
                    interactable.Interact(this);
                }
            }
            
        }
        lookTarget.position = Vector3.Lerp(lookTarget.position, rHit.point, Time.deltaTime*10);
    }
    protected override void Awake()
    {
        base.Awake();
        rb = GetComponent<Rigidbody>();
        instance = this;
    }
}
