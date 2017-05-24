using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Runtime.InteropServices;
using Ez.Msg;
using UnityEngine.EventSystems;

public interface UIIntro : IEventSystemHandler
{
    IEnumerable StartMusic();
    IEnumerable ShowLogo();
    IEnumerable ShowGgj();
    IEnumerable HideLogo();
    IEnumerable HideGgj();
    IEnumerable AnimRobo();
    IEnumerable AnimLogo();
    IEnumerable ChangeScene();
}

public class IntroController : MonoBehaviour, UIIntro
{

    public float InitialDelay;
    public Image Logo;
    public Image Ggj;
    public float FadeAmount;
    public float TimeShowing;

    public bool MayJump = false;

    public GameObject LogoJogo;
    public GameObject Metal;

    public AudioClip sukafu;

    public bool shouldContinue;

	// Use this for initialization
	void Start ()
	{
	    EzMsg.Wait(InitialDelay)
	        .Send<UIIntro>(gameObject, _ => _.ShowLogo())
	        .Send<UIIntro>(gameObject, _ => _.HideLogo())
	        .Send<UIIntro>(gameObject, _ => _.StartMusic())
	        .Send<UIIntro>(gameObject, _ => _.ShowGgj())
	        .Send<UIIntro>(gameObject, _ => _.HideGgj())
	        .Send<UIIntro>(gameObject, _ => _.AnimRobo())
	        .Send<UIIntro>(gameObject, _ => _.AnimLogo())
	        .Send<UIIntro>(gameObject, _ => _.ChangeScene())
	        .Run();
	}
	
	// Update is called once per frame
	void Update () {
	    if (MayJump && InputController.Jump)
	    {
	        Application.LoadLevel("cena_menu");
	    }
	}

    public IEnumerable StartMusic()
    {
        SoundData.Play(1);
        yield return null;
    }

    public IEnumerable ShowLogo()
    {
        var color = Logo.color;
        AudioSource.PlayClipAtPoint(sukafu, Camera.main.transform.position);
        while (color.a <= 1)
        {
            color.a += FadeAmount;
            Logo.color = color;
            yield return null;
        }
        yield return new WaitForSeconds(TimeShowing);
    }

    public IEnumerable ShowGgj()
    {
        var color = Ggj.color;
        while (color.a <= 1)
        {
            color.a += FadeAmount;
            Ggj.color = color;
            yield return null;
        }
        yield return new WaitForSeconds(TimeShowing);
    }

    public IEnumerable HideLogo()
    {
        var color = Logo.color;
        while (color.a > 0)
        {
            color.a -= FadeAmount;
            Logo.color = color;
            yield return null;
        }
        MayJump = true;
        yield return null;
    }

    public IEnumerable HideGgj()
    {
        var color = Ggj.color;
        while (color.a > 0)
        {
            color.a -= FadeAmount;
            Ggj.color = color;
            yield return null;
        }
        MayJump = true;
        yield return null;
    }


    public IEnumerable AnimRobo()
    {
        Metal.GetComponent<Animator>().SetTrigger("Start");
        shouldContinue = false;
        while (!shouldContinue)
        {
            yield return null;
        }

        yield return null;
    }

    public IEnumerable AnimLogo()
    {
        LogoJogo.GetComponent<Animator>().SetTrigger("Start");
        shouldContinue = false;
        while (!shouldContinue)
        {
            yield return null;
        }
        yield return null;
    }

    public IEnumerable ChangeScene()
    {
        Application.LoadLevel("cena_menu");
        yield return null;
    }
}
