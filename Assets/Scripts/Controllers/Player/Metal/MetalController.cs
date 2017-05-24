using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MetalController : MonoBehaviour, IPlayer, ITargetable
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
    public bool _canJumpPunch;
    private bool _canWalk;
    private bool _canGravity;
    public float VerticalVelocity;
    public bool LookRight;

    public float Gravity;
    public float MoveSpeed;
    public float JumpForce;

    public LayerMask GroundMask;
    public LayerMask SideMask;
    public LayerMask EnemyMask;
    public LayerMask UpMask;
    public float GroundOffset;
    public Transform GroundCheck;
    public Transform SideCheck;
    public Transform UpCheck;
    public GameObject Hadouken;

    public GameObject PunchParticles;
    public GameObject Pieces;

    public Animator Animator;
    private SpriteRenderer _renderer;

    public AudioClip PunchSound;
    public AudioClip DeathSound;

    // Use this for initialization
    void Awake ()
    {
        Animator = GetComponent<Animator>();
        _renderer = GetComponent<SpriteRenderer>();
        _canJumpPunch = true;
        _canWalk = true;
        _canGravity = true;
        LookRight = true;
    }

    // Update is called once per frame
    void Update ()
    {
        if (GameController.Instance.GameOver)
        {
            return;
        }

        Move();

        _grounded = IsGrounded();
        Animator.SetBool("Grounded", _grounded);
        if (!_grounded && _canGravity)
        {
            VerticalVelocity -= Gravity * Time.deltaTime;
        }
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
                    var amountToAdd = InputController.Horizontal * MoveSpeed * Time.deltaTime;
                    if (!_canGravity)
                    {
                        amountToAdd /= 4;
                    }
                    pos.x += amountToAdd;
                }
            }
            if (InputController.Horizontal <= -0.2f)
            {
                LookRight = false;
                _walking = true;
                if (IsFree(MoveSpeed, false) == 2)
                {
                    var amountToAdd = InputController.Horizontal * MoveSpeed * Time.deltaTime;
                    if (!_canGravity)
                    {
                        amountToAdd /= 4;
                    }
                    pos.x += amountToAdd;
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

        if(VerticalVelocity <= 0) {
            //center
            RaycastHit2D hit = Physics2D.Raycast(transform.position, new Vector2(0,-1), distance, GroundMask);
            if(hit.collider != null) {
                transform.position = new Vector3(transform.position.x, hit.point.y + (distance - 0.01f), transform.position.z);
                VerticalVelocity = 0;
                Debug.DrawLine(transform.position, GroundCheck.position,	Color.red);
                return true;
            }
            //right
            origin.x += SideCheck.position.x - transform.position.x;
            hit = Physics2D.Raycast(origin, new Vector2(0,-1), distance, GroundMask);
            if(hit.collider != null) {
                transform.position = new Vector3(transform.position.x, hit.point.y + (distance - 0.01f), transform.position.z);
                VerticalVelocity = 0;
                Debug.DrawLine(origin, GroundCheck.position, Color.red);
                return true;
            }
            //left
            origin = transform.position;
            origin.x -= SideCheck.position.x - transform.position.x;
            hit = Physics2D.Raycast(origin, new Vector2(0,-1), distance, GroundMask);
            if(hit.collider != null) {
                transform.position = new Vector3(transform.position.x, hit.point.y + (distance - 0.01f), transform.position.z);
                VerticalVelocity = 0;
                Debug.DrawLine(origin, GroundCheck.position, Color.red);
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
        return false;
    }

    public bool CanDoubleJump()
    {
        return _canJumpPunch;
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
        _canJumpPunch = canDoubleJump;
        yield return null;
    }

    public IEnumerable Kill()
    {
        GameController.PlaySound(DeathSound);
        GameController.Spawn(Pieces, transform.position);
        _renderer.enabled = false;
        GetComponent<BoxCollider2D>().enabled = false;
        GameController.Instance.StartFade();
        yield return null;
    }

    public void HitGround()
    {
        _canJumpPunch = true;
    }

    public IEnumerable Jump()
    {
        if (_grounded)
        {
            VerticalVelocity = JumpForce;
        }

        yield return null;
    }

    public IEnumerable Skill()
    {
        if(!GameController.Instance.GameOver)
        {
            Animator.SetTrigger("Punch");
        }
        yield return null;
    }

    public IEnumerable StartSkill()
    {
//        _canGravity = false;
//        _canWalk = true;
        yield return null;
    }

    public IEnumerable EndSkill()
    {
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

    public void Attack()
    {
        var origin = transform.position;
        origin.x += LookRight ? 1.5f : -1.5f;
        var radius = 1.2f;

        var _origin = transform.position;
        _origin.x += LookRight ? 1 : -1;
        var clone = GameController.Spawn(Hadouken, _origin);
        clone.GetComponent<HadoukeScript>().SetSide(LookRight);

        GameController.PlaySound(PunchSound);
        clone = GameController.Spawn(PunchParticles, transform.position);
        if (!LookRight)
        {
            var euler = clone.transform.rotation.eulerAngles;
            euler.z = 180;
            clone.transform.rotation = Quaternion.Euler(euler);
        }

        Destroy(clone.gameObject, 1f);

        var colls = Physics2D.OverlapCircleAll(origin, radius, EnemyMask);
        foreach (var col in colls)
        {
            col.gameObject.Send<IEnemy>(_=>_.ApplyDamage()).Run();
        }
    }
}
