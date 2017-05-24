using UnityEngine;
using System.Collections;

public class MeleeEnemyController : MonoBehaviour, IEnemy
{
    public bool RightLook;
    private bool _canAttack;

    public float CoolDown;
    private float _coolDown;

    public LayerMask PlayerMask;

    private SpriteRenderer _renderer;

    public GameObject Pieces;

    public int Pontos;

    public float DistanceHit;
    public float RadiusHit;

    public AudioClip DeathSound;

    // Use this for initialization
    void Start ()
    {
        _renderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update ()
    {
        _coolDown -= Time.deltaTime;
        if (_coolDown <= 0)
        {
            _canAttack = true;
        }

        RightLook = GameController.Instance.Player.transform.position.x > transform.position.x;

        _renderer.flipX = !RightLook;
    }

    public int CanAttacK()
    {
        return _canAttack ? 2 : 1;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        other.gameObject.Send<IPlayer>(_=>_.Kill()).Run();
    }

    public IEnumerable ApplyDamage()
    {
        GameController.PlaySound(DeathSound);

        GameController.Instance.Pontos += Pontos * GameController.Instance.Multiplicador;
        GameController.Instance.AddMultiplicador();

        GameController.Spawn(Pieces, transform.position);
        transform.position = new Vector3(-1000, -1000, 0);
        Destroy(gameObject, 2);
        yield return null;
    }

    public IEnumerable SetPursuing(bool pursuing)
    {
        yield return null;
    }

    public void ExecuteAttack()
    {
        StartCoroutine(Attack().GetEnumerator());
    }

    public IEnumerable Attack()
    {
        Debug.Log("Katiau");
        var origin = transform.position;
        origin.x += RightLook ? DistanceHit : -DistanceHit;
        var radius = RadiusHit;

        var res = Physics2D.OverlapCircleAll(origin, radius, PlayerMask);
        foreach (var col in res)
        {
            col.gameObject.Send<IPlayer>(_=>_.Kill()).Run();
        }

        _canAttack = false;
        _coolDown = CoolDown;

        yield return null;
    }
}
