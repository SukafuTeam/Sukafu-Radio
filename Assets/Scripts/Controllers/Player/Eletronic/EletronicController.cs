using System;
using UnityEngine;
using System.Collections;

public class EletronicController : MonoBehaviour, IPlayer, ITargetable
{
    private bool __grounded;
    private bool _grounded
    {
        get
        {
            return __grounded;
        }
        set
        {
            if (value != __grounded)
            {
                if (value)
                {
                    HitGround();
                }
            }
            __grounded = value;
        }
    }
    private bool _walking;
    private bool _canDash;
    private bool _canDoubleJump;
    private bool _canWalk;
    private bool _canGravity;
    public  float VerticalVelocity;
    public bool LookRight;

    public float Gravity;
    public float MoveSpeed;
    public float JumpForce;

    public LayerMask GroundMask;
    public LayerMask SideMask;
    public LayerMask UpMask;
    public float GroundOffset;
    public Transform GroundCheck;
    public Transform SideCheck;
    public Transform UpCheck;

    public Animator Animator;
    private SpriteRenderer _renderer;

    public AudioClip DashSound;
    public AudioClip deathSound;

    public GameObject Pieces;

    // Use this for initialization
    void Start ()
    {
        Animator = GetComponent<Animator>();
        _renderer = GetComponent<SpriteRenderer>();
        _canDash = true;
        _canDoubleJump = true;
        _canWalk = true;
        _canGravity = true;
        LookRight = true;
    }

    // Update is called once per frame
    void Update ()
    {
        if (Time.deltaTime > 0.2f || GameController.Instance.GameOver)
        {
            return;
        }

        Move();



        if (!_grounded && _canGravity)
        {
            VerticalVelocity -= Gravity * Time.deltaTime;
        }
    }

    private void FixedUpdate()
    {
        _grounded = IsGrounded();
        Animator.SetBool("Grounded", _grounded);
    }

    #region Player Methods

    private void Move()
    {
        var pos = transform.position;

        if (_canGravity)
        {
            pos.y += VerticalVelocity * Time.deltaTime;
        }

        if (VerticalVelocity > 0)
        {
            var origin = transform.position;
            var distance = UpCheck.position.y - transform.position.y;
            var hit = Physics2D.Raycast(origin, new Vector2(0, 1), distance, UpMask);
            if (hit.collider != null)
            {
                VerticalVelocity = 0;
            }
        }

        if (_canWalk)
        {
            _walking = false;

            if (InputController.Horizontal >= 0.2f)
            {
                LookRight = true;
                _walking = true;
                if (IsFree(MoveSpeed, true) == 2)
                {
                    pos.x += InputController.Horizontal * MoveSpeed * Time.deltaTime;
                }
            }
            if (InputController.Horizontal <= -0.2f)
            {
                LookRight = false;
                _walking = true;
                if (IsFree(MoveSpeed, false) == 2)
                {
                    pos.x += InputController.Horizontal * MoveSpeed * Time.deltaTime;
                }
            }
        }

        _renderer.flipX = !LookRight;
        transform.position = pos;
        Animator.SetBool("Walking", _walking);
    }

    public int IsFree(float speed, bool right)
    {
        var origin = transform.position;
        var distance = SideCheck.position.x - transform.position.x + speed * Time.deltaTime;
        var direction = new Vector2(1, 0);
        RaycastHit2D hit;

        if (right)
        {
            hit = Physics2D.Raycast(origin, direction, distance, SideMask);
            Debug.DrawLine(origin, new Vector3(origin.x + distance, origin.y, origin.z), hit.collider == null? Color.green : Color.red);
            return hit.collider == null ? 2 : 1;
        }

        direction = new Vector2(-1, 0);
        hit = Physics2D.Raycast(origin, direction, distance, SideMask);
        Debug.DrawLine(origin, new Vector3(origin.x - distance, origin.y, origin.z), hit.collider == null? Color.green : Color.red);
        return hit.collider == null ? 2 : 1;
    }

    private bool IsGrounded()
    {
        var distance = transform.position.y - GroundCheck.position.y;
        var origin = transform.position;

        if(VerticalVelocity <= 0)
        {
            var free = false;

            //center
            RaycastHit2D hit = Physics2D.Raycast(transform.position, new Vector2(0,-1), distance, GroundMask);
            if(hit.collider != null) {
                transform.position = new Vector3(transform.position.x, hit.point.y + (distance - 0.01f), transform.position.z);
                VerticalVelocity = 0;
                Debug.DrawLine(transform.position, GroundCheck.position,	Color.red);
                free = true;
            }
            //right
            origin.x += SideCheck.position.x - transform.position.x;
            var destiny = GroundCheck.position;
            destiny.x += SideCheck.position.x - transform.position.x;
            hit = Physics2D.Raycast(origin, new Vector2(0,-1), distance, GroundMask);
            if(hit.collider != null) {
                transform.position = new Vector3(transform.position.x, hit.point.y + (distance - 0.01f), transform.position.z);
                VerticalVelocity = 0;
                Debug.DrawLine(origin, destiny, Color.red);
                free = true;
            }
            //left
            origin = transform.position;
            origin.x -= SideCheck.position.x - transform.position.x;
            destiny = GroundCheck.position;
            destiny.x -= SideCheck.position.x - transform.position.x;
            hit = Physics2D.Raycast(origin, new Vector2(0,-1), distance, GroundMask);
            if(hit.collider != null) {
                transform.position = new Vector3(transform.position.x, hit.point.y + (distance - 0.01f), transform.position.z);
                VerticalVelocity = 0;
                Debug.DrawLine(origin, destiny, Color.red);
                free = true;
            }

            if (free)
            {
                return true;
            }
        }

        Debug.DrawLine(transform.position, GroundCheck.position,	Color.green);
        return false;
    }
    #endregion

    #region IPlayer Methods

    public bool Grounded()
    {
        return _grounded;
    }

    public bool CanDash()
    {
        return _canDash;
    }

    public bool CanDoubleJump()
    {
        return _canDoubleJump;
    }

    public bool CanWalk()
    {
        return _canWalk;
    }

    public bool Walking()
    {
        return _walking;
    }

    public float GetJumpForce()
    {
        return JumpForce;
    }

    public bool CanGravity()
    {
        return _canGravity;
    }

    public int IsLookRight()
    {
        return LookRight ? 2 : 1;
    }

    public IEnumerable SetCanDash(bool canDash)
    {
        _canDash = canDash;
        yield return null;
    }

    public IEnumerable SetCanWalk(bool canWalk)
    {
        _canWalk = canWalk;
        yield return null;
    }

    public IEnumerable SetCangravity(bool canGravity)
    {
        _canGravity = canGravity;
        yield return null;
    }

    public IEnumerable SetCanDoubleJump(bool canDoubleJump)
    {
        _canDoubleJump = canDoubleJump;
        yield return null;
    }

    public IEnumerable Kill()
    {
        GameController.PlaySound(deathSound);
        GameController.Spawn(Pieces, transform.position);
        _renderer.enabled = false;
        GetComponent<BoxCollider2D>().enabled = false;
        GameController.Instance.StartFade();
        yield return null;
    }

    public void HitGround()
    {
        _canDoubleJump = true;
        _canDash = true;
    }

    public IEnumerable Jump()
    {
        if (!_grounded)
        {
            if (!_canDoubleJump)
            {
                yield return null;
            }

            _canDoubleJump = false;
            VerticalVelocity = JumpForce;
            Animator.SetTrigger("Jump");
            yield return null;
        }

        VerticalVelocity = JumpForce;
        yield return null;
    }

    public IEnumerable Skill()
    {
        if (_canDash && !GameController.Instance.GameOver)
        {
            Animator.SetTrigger("Dash");
            GameController.PlaySound(DashSound);
        }
        yield return null;
    }

    public IEnumerable StartSkill()
    {
        _canDash = false;
        _canGravity = false;
        _canWalk = false;
        GetComponent<BoxCollider2D>().enabled = false;
        yield return null;
    }

    public IEnumerable EndSkill()
    {
        _canGravity = true;
        _canWalk = true;
        VerticalVelocity = 0;
        GetComponent<BoxCollider2D>().enabled = true;
        if (_grounded)
        {
            _canDash = true;
        }
        yield return null;
    }

    public GameObject GetPlayer()
    {
        return gameObject;
    }

    #endregion

    public IEnumerable GetHit()
    {
        gameObject.Send<IPlayer>(_=>_.Kill()).Run();
        yield return null;
    }

    public IEnumerable SplashHit()
    {
        yield return null;
    }
}
