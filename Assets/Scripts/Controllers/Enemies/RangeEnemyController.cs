using UnityEngine;
using System.Collections;

public class RangeEnemyController : MonoBehaviour, IEnemy
{
    private bool _pursuing;
    private bool _canAttack;
    private bool _lookRight;
    private SpriteRenderer _renderer;

    public GameObject Projectile;
    public Transform offset;
    public float CoolDownTime;
    private float _coolDownTime;

    public int Pontos;

    public GameObject Pieces;

    public AudioClip shootSound;
    public AudioClip DeathSound;

	// Use this for initialization
	void Start ()
	{
	    _renderer = GetComponent<SpriteRenderer>();
	    _lookRight = true;
	}
	
	// Update is called once per frame
	void Update ()
	{
	    _coolDownTime -= Time.deltaTime;
	    if (_coolDownTime <= 0)
	    {
	        _canAttack = true;
	    }

	    _lookRight = GameController.Instance.Player.transform.position.x > transform.position.x;
	    _renderer.flipX = !_lookRight;

	}

    private void OnTriggerEnter2D(Collider2D other)
    {
        other.gameObject.Send<IPlayer>(_=>_.Kill()).Run();
    }

    public int CanAttacK()
    {
        return _canAttack ? 2 : 1;
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
        _pursuing = pursuing;
        yield return null;
    }

    public IEnumerable Attack()
    {
        _canAttack = false;
        _coolDownTime = CoolDownTime;
        var origin = offset.position;

        GameController.PlaySound(shootSound);

        if (!_lookRight)
        {
            var distance = origin.x - transform.position.x;
            origin.x -= distance * 2;
        }

        var clone = GameController.Spawn(Projectile, origin);
        clone.Send<IProjectile>(_ => _.SetData(_lookRight)).Run();
        yield return null;
    }
}
