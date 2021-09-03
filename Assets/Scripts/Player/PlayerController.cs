using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Animator Anim;
    public CharacterController Ctrl;
    private bool isMove;
    public Transform ViewManager;
    private float currentBlend=0;//������¼��ǰ��Blendֵ
    private float targetBlend=0;//������¼Ŀ��Blendֵ
    private bool isGrounded;
    public GameObject CheckIsGrounded;
    public float radius;
    private float NextAttack;
    public float AttackCD=1;
    public bool isAttacking;
    private float downVelocity;//�����ٶ�

    private Vector3 viewManagerOffset = new Vector3(0,0,0.1f);//������¼ViewManager����ĳ�ʼλ��
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

       if( Physics.OverlapSphere(CheckIsGrounded.transform.position, radius).Length<=1)//�ж��Ƿ����
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
        
        float h = Input.GetAxisRaw("Horizontal");//����ʹ��Raw����Ϊ����ʹ��Rawʱ�����ڹ�������������ʱ������ƶ��������Ὣ���봫��ű�����Ϊֹͣ����ʱ��Axisֵ��������0��
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
                setViewDir();//�߼���ֻ�е���ɫ���ƶ�ʱ������ʹ����������ƽ�ɫ��ǰ��������˽�ͨ�������Ʒ���Ľű�����isMove���ж�֮����������ɫͣ�º󣬽�ɫ���泯ֹͣ�ƶ�ʱ�ķ��򣬷��򣬽�ɫ���泯��ǰ�����������ķ���
                setDir();
                setMove();
            }
        }
        if (currentBlend != targetBlend)
            {
                setAnimBlend();
            }

    }

    private void setDir()//�������ﳯ��
    {

        float angle = Vector2.SignedAngle(Dir, new Vector2(0, 1));
        Vector3 eulerAngles = new Vector3(0,transform.localEulerAngles.y+angle, 0);//�ڵ�ǰ�ӽǵĻ����ϣ����ϼ�������ĽǶ�
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
    private void setView()//�����ӽǣ������λ�ã��Լ����ﳯ��
    {
        float X = Input.GetAxis("Mouse X");
        float Y = Input.GetAxis("Mouse Y");
        if (ViewManager.eulerAngles.x < 340 && ViewManager.eulerAngles.x>100 &&Y > 0)//�����ӽǵ�Y�᷽��Χ
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
    private void setViewDir()//��������ӽǣ�������ҵĳ��򣬴Ӷ�ʵ��ʹ�����任�ӽ���������ҵ��ƶ�����
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
            //TODO�����������������ײ�壬������ʱ�Ӵ������˺󣬵��õ��˵�GetHit�ӿ�
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
