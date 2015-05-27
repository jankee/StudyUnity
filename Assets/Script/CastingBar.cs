using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CastingBar : MonoBehaviour 
{
    private Vector3 startPos;
    private Vector3 endPos;

    private Image castImage;
    private RectTransform castTransform;

    public Text SpellName;
    public Text CastTime;
    public CanvasGroup canvasGroup;

    private float fadeSpeed = 2.0f;


    private bool casting;

    //하드코딩 스펠
    private Spell fireBall = new Spell("FireBall", 2f, Color.red);
    private Spell frostBolt = new Spell("FrostBolt", 1.5f, Color.blue);
    private Spell heal = new Spell("Heal", 1f, Color.green);

	// Use this for initialization
	void Start () 
    {
        casting = false;
        castTransform = GetComponent<RectTransform>();
        castImage = GetComponent<Image>();
        //canvasGroup = GetComponent<CanvasGroup>();
        endPos = castTransform.position;
        startPos = new Vector3(castTransform.position.x - castTransform.rect.width, castTransform.position.y, castTransform.position.z);
        print("StarPos" + startPos);
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            StartCoroutine(CastSpell(fireBall));
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            StartCoroutine(CastSpell(frostBolt));
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            StartCoroutine(CastSpell(heal));
        }
	}

    private IEnumerator FadeIn()
    {
        while (canvasGroup.alpha < 1.0f)
        {
            
            float newValue = fadeSpeed * Time.deltaTime;

            if ((canvasGroup.alpha + newValue) < 1.0f)
            {
                canvasGroup.alpha += newValue;
            }
            else
            {
                canvasGroup.alpha = 1f;
            }

            yield return null;
        }
    }

    private IEnumerator FadeOut()
    {
        while (canvasGroup.alpha > 0.0f)
        {

            float newValue = fadeSpeed * Time.deltaTime;

            if ((canvasGroup.alpha - newValue) > 0.0f)
            {
                canvasGroup.alpha -= newValue;
            }
            else
            {
                canvasGroup.alpha = 0f;
            }

            yield return null;
        }
    }

    private IEnumerator CastSpell(Spell spell)
    {
        if (!casting)
        {
        StartCoroutine(FadeIn());
            //canvasGroup.alpha = 1.0f;

            casting = true;
            castImage.color = spell.SpellColor;
            castTransform.position = startPos;
            float timeLeft = Time.deltaTime;
            float rate = 1.0f / spell.CastTime;
            float progress = 0.0f;

            SpellName.text = spell.Name;

            while (progress <= 1.0f)
            {
                castTransform.position = Vector3.Lerp(startPos, endPos, progress);

                progress += rate * Time.deltaTime;

                timeLeft += Time.deltaTime;

                CastTime.text = timeLeft.ToString("F2") + " / " + spell.CastTime.ToString("F2");

                yield return null;
            }

            castTransform.position = endPos;

            CastTime.text = spell.CastTime.ToString("F2") + " / " + spell.CastTime.ToString("F2");

            StartCoroutine(FadeOut());
            casting = false;
        }
        

    }
}
