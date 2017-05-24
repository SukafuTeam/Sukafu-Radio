using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class GameController : MonoBehaviour
{
    public static GameController Instance;
    public GameObject Player;

    public bool IsEletronic;
    public GameObject Eletronic;
    public GameObject Metal;
    private MetalController _mc;
    private EletronicController _ec;

    public float CooldownChange;
    private float _cooldownChange;
    public bool CanChange;

    public Image CoolDownImage;
    public Animator CoolDownAnimator;

    public Sprite EletronicSprite;
    public Sprite EletronicBlackSprite;
    public Sprite MetalSprite;
    public Sprite MetalBlackSprite;

    public Image OtherImage;
    public Image OtherLoadImage;
    public Image ActualImage;

    public Text perda;

    public bool GameOver;

    public int Pontos;
    public int Multiplicador = 1;
    public float _timeLeftCombo;

    public Text TxtPontos;
    public Text TxtCombo;

    public CameraFilterPack_Color_GrayScale gray;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;

            _mc = Metal.GetComponent<MetalController>();
            _ec = Eletronic.GetComponent<EletronicController>();

            Metal.SetActive(false);

            Multiplicador = 0;
            Pontos = PlayerPrefs.GetInt("pontos");

        }
    }

	// Use this for initialization
	void Start ()
	{
	    IsEletronic = true;
	}

    public void AddMultiplicador()
    {
    }

	// Update is called once per frame
	void Update ()
	{
	    if (Input.GetKeyDown(KeyCode.Escape))
	    {
	        LoadLevel("cena_menu");
	    }

	    _timeLeftCombo -= Time.deltaTime;
	    if (_timeLeftCombo <= 0)
	    {
	        Multiplicador = 1;
	    }

	    if (Multiplicador > 2)
	    {
	        TxtCombo.text = "x" + Multiplicador;
	    }
	    else
	    {
	        TxtCombo.text = "";
	    }

	    TxtPontos.text = "" + Pontos;

	    if (GameOver)
	    {
	        if (InputController.Jump)
	        {

	            if (Pontos <= 0)
	            {
	                SoundData.SetGameplay(1);
	                SoundData.Stop(2);
                    GameController.LoadLevel("cena_menu");
	                return;
	            }

	            PlayerPrefs.SetInt("pontos", Pontos);
	            PlayerPrefs.Save();

	            SoundData.SetGameplay(1);
	            Application.LoadLevel(Application.loadedLevel);
	        }
	    }

	    _cooldownChange -= Time.deltaTime;
	    if (_cooldownChange <= 0 && !CanChange)
	    {
	        CanChange = true;
	        CoolDownAnimator.SetTrigger("Ready");
	        _cooldownChange = 0;
	    }

	    CoolDownImage.fillAmount = Mathf.InverseLerp(0, CooldownChange, _cooldownChange);
	    if (!CanChange)
	    {
	        return;
	    }

	    if (IsEletronic)
	    {
	        if (InputController.Metal)
	        {
	            Metal.transform.position = Eletronic.transform.position;
	            Eletronic.SetActive(false);
	            Metal.SetActive(true);
	            Player = Metal;
	            IsEletronic = false;
	            _mc.LookRight = _ec.LookRight;
	            _mc.VerticalVelocity = _ec.VerticalVelocity;
	            _mc.Animator.Rebind();
	            _mc.Animator.SetTrigger("Begin");
	            _cooldownChange = CooldownChange;
	            CanChange = false;

	            ActualImage.sprite = MetalSprite;
	            OtherImage.sprite = EletronicSprite;
	            OtherLoadImage.sprite = EletronicBlackSprite;

	            SoundData.SetGameplay(2);
	        }
	    }
	    else
	    {
	        if (InputController.Eletronic)
	        {
	            Eletronic.transform.position = Metal.transform.position;
	            Eletronic.SetActive(true);
	            Metal.SetActive(false);
	            Player = Eletronic;
	            IsEletronic = true;
	            _ec.LookRight = _mc.LookRight;
	            _ec.VerticalVelocity = _mc.VerticalVelocity;
	            _ec.Animator.Rebind();
	            _ec.SetCanDoubleJump(false);
	            _ec.SetCanWalk(true);
	            _ec.SetCangravity(true);
	            _ec.EndSkill();
	            _ec.HitGround();
	            _ec.Animator.SetTrigger("Begin");
	            _cooldownChange = CooldownChange;
	            CanChange = false;

	            ActualImage.sprite = EletronicSprite;
	            OtherImage.sprite = MetalSprite;
	            OtherLoadImage.sprite = MetalBlackSprite;

	            SoundData.SetGameplay(1);
	        }
	    }
	}

    public void StartFade()
    {
        GameOver = true;
        perda.enabled = true;
        Pontos -= 300;
        Metal.GetComponent<Animator>().enabled = false;
        Eletronic.GetComponent<Animator>().enabled = false;
        if (Pontos <= 0)
        {
            Pontos = 0;
        }
        StartCoroutine(Fade());
    }

    private IEnumerator Fade()
    {
        while (gray._Fade < 1)
        {
            gray._Fade += 0.02f;
            yield return null;
        }
    }

    static public GameObject Spawn(GameObject gameObject, Vector3 position)
    {
#if UNITY_EDITOR
        var res = PrefabUtility.InstantiatePrefab(gameObject) as GameObject;
        res.transform.position = position;
        return res;
#else
        var res = Instantiate(gameObject, position, Quaternion.identity) as GameObject;
        return res;
 #endif
    }

    public static void PlaySound(AudioClip sound)
    {
        if (sound == null)
        {
            return;
        }

        AudioSource.PlayClipAtPoint(sound, Camera.main.transform.position, 0.4f);
    }

    [ContextMenu("reset fields")]
    public void ResetFields()
    {
        PlayerPrefs.SetInt("fase", 1);
        PlayerPrefs.SetInt("pontos", 0);
        PlayerPrefs.Save();
    }

    public static void LoadLevel(string level)
    {
        PlayerPrefs.SetString("scene", level);
        PlayerPrefs.Save();
        Application.LoadLevel("cena_load");
    }
}
