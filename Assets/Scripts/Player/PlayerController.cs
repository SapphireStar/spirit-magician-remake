using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Animator Anim;
    public CharacterController Ctrl;
    private bool isMove;
    public Transform ViewManager;
    private float currentBlend=0;//用来记录当前的Blend值
    private float targetBlend=0;//用来记录目标Blend值
    private bool isGrounded;
    public GameObject CheckIsGrounded;
    public float radius;
    private float NextAttack;
    public float AttackCD=1;
    public bool isAttacking;
    private float downVelocity;//下落速度

    private Vector3 viewManagerOffset = new Vector3(0,0,0.1f);//用来记录ViewManager物体的初始位置
    private Vector3 dir = Vector3.zero;
    private Vector3 Dir
    {
        get
        {
            return dir;
        }
        set
        {
            if(value != Vector3.zero)
            {
                isMove = true;
                dir = value;
                targetBlend = 1;
            }
            else
            {
                isMove = false;
                dir = Vector3.zero;
                targetBlend = 0;
            }
        }
    }
    void Start()
    {

    }


    void Update()
    {

       if( Physics.OverlapSphere(CheckIsGrounded.transform.position, radius).Length<=1)//判断是否落地
        {
            isGrounded = false;
        }
        if (!isGrounded)
        {
            downVelocity +=Time.deltaTime * Constants.Gravity;
            Ctrl.Move(Vector3.down * downVelocity * Time.deltaTime);
        }
        EventAttack();
        setView();
        
        float h = Input.GetAxisRaw("Horizontal");//这里使用Raw是因为当不使用Raw时，若在攻击即将结束的时候进行移动操作，会将输入传入脚本（因为停止输入时，Axis值会慢慢归0）
        float v = Input.GetAxisRaw("Vertical");
        Vector2 _dir = new Vector2(h, v);
        setAnimBlend();

        if (_dir != Vector2.zero)
        {
            Dir = _dir;
        }
        else Dir = Vector3.zero;

        if (!isAttacking)
        {



            if (isMove)
            {
                setViewDir();//逻辑：只有当角色在移动时，才能使用鼠标来控制角色的前进方向，因此将通过鼠标控制方向的脚本放在isMove的判定之后，这样当角色停下后，角色会面朝停止移动时的方向，否则，角色会面朝当前摄像机所朝向的方向
                setDir();
                setMove();
            }
        }
        if (currentBlend != targetBlend)
            {
                setAnimBlend();
            }

    }

    private void setDir()//设置人物朝向
    {

        float angle = Vector2.SignedAngle(Dir, new Vector2(0, 1));
        Vector3 eulerAngles = new Vector3(0,transform.localEulerAngles.y+angle, 0);//在当前视角的基础上，加上键盘输入的角度
        transform.localEulerAngles = eulerAngles;
        

    }
    private void setMove()
    {
        Ctrl.Move(transform.forward*Constants.PlayerSpeed*Time.deltaTime);
    }
    private void setAnimBlend()
    {
        if (Mathf.Abs(currentBlend - targetBlend) < Constants.AccelerSpeed * Time.deltaTime)
        {
            currentBlend = targetBlend;
        }
        else if (currentBlend > targetBlend)
        {
            currentBlend -= Constants.AccelerSpeed * Time.deltaTime;
        }
        else currentBlend += Constants.AccelerSpeed * Time.deltaTime;
        Anim.SetFloat("Speed", currentBlend);
    }
    private void setView()//设置视角，摄像机位置，以及人物朝向
    {
        float X = Input.GetAxis("Mouse X");
        float Y = Input.GetAxis("Mouse Y");
        if (ViewManager.eulerAngles.x < 340 && ViewManager.eulerAngles.x>100 &&Y > 0)//限制视角的Y轴方向范围
        {
            Y = 0;
        }
        if (ViewManager.eulerAngles.x >80 && ViewManager.eulerAngles.x < 100 && Y < 0)
        {
            Y = 0;
        }
        ViewManager.eulerAngles += new Vector3( -Y, X, 0);
        ViewManager.position = transform.position+viewManagerOffset;


    }
    private void setViewDir()//根据相机视角，设置玩家的朝向，从而实现使用鼠标变换视角来控制玩家的移动方向
    {
        transform.localEulerAngles = new Vector3(0, ViewManager.eulerAngles.y, 0);
    }
    public void EventAttack()
    {
        if (Time.time > NextAttack&&Input.GetKeyDown(KeyCode.Mouse0))
        {
            isAttacking = true;
            Anim.SetTrigger("Attack");
            NextAttack = Time.time + AttackCD;
            //TODO在这里调用武器的碰撞体，当攻击时接触到敌人后，调用敌人的GetHit接口
        }
    }
    public void stopAttacking()
    {
        isAttacking = false;
    }

    public void OnControllerColliderHit(ControllerColliderHit hit)
    {

        if (hit.collider.tag == "Ground")
        {
            isGrounded = true;
            downVelocity = 0;
        }
        else isGrounded = false;

    }
    public void OnDrawGizmos()
    {
        Gizmos.DrawSphere(CheckIsGrounded.transform.position, radius);
    }

}
