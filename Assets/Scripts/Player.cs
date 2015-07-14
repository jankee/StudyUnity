using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Player : MoveObject 
{
    public int wallDamage = 1;
    public int pointPerFood = 10;
    public int pointPerSoda = 20;
    public float restartLevelDelay = 1f;
    public Text foodText;

    public AudioClip moveSound1;
    public AudioClip moveSound2;
    public AudioClip eatSound1;
    public AudioClip eatSound2;
    public AudioClip drinkSound1;
    public AudioClip drinkSound2;
    public AudioClip gameOverSound;

    private Animator animator;
    private int food;

    private Vector2 touchOrigin = -Vector2.one;

    private Wall hitComponent;

	// Use this for initialization
	protected override void Start () 
    {
        animator = GetComponent<Animator>();
        food = GameManager.instance.playerFoodPoint;

        foodText.text = "Food: " + food;

        base.Start();
	}
	
    private void OnDisable()
    {
        GameManager.instance.playerFoodPoint = food;
    }

	// Update is called once per frame
	void Update () 
    {
        if (!GameManager.instance.playersTurn)
        {
            return;
        }

        int horizontal = 0;
        int vertical = 0;

#if UNITY_EDITOR || UNITY_STANDALONE || WUNITY_WEBPLAYER
        


        horizontal = (int)Input.GetAxisRaw("Horizontal");
        vertical = (int)Input.GetAxisRaw("Vertical");

        
        //print("hirizontal : " + horizontal);
        //print("vertical : " + vertical);

        if (horizontal != 0)
        {
            vertical = 0;
        }
#else

        if (Input.touchCount > 0)
        {
            Touch myTouch = Input.touches[0];
            if (myTouch.phase == TouchPhase.Began)
            {
                touchOrigin = myTouch.position;
            }
            else if (myTouch.phase == TouchPhase.Ended && touchOrigin.x >= 0)
            {
                Vector2 touchEnd = myTouch.position;
                float x = touchEnd.x - touchOrigin.x;
                float y = touchEnd.y - touchOrigin.y;
                touchOrigin.x = -1;

                if (Mathf.Abs(x) > Mathf.Abs(y))
                {
                    horizontal = x > 0 ? 1 : -1;
                }
                else
                {
                    vertical = y > 0 ? 1 : -1;
                }
            }
        }  
#endif

        if (horizontal != 0 || vertical != 0)
        {
            AttempMove<Wall>(horizontal, vertical);
        }
	}

    protected override void AttempMove<T>(int xDir, int yDir)
    {
        food--;
        foodText.text = "Food: " + food;

        base.AttempMove<T>(xDir, yDir);

        RaycastHit2D hit;
        if (Move(xDir, yDir, out hit))
        {
            SoundManager.instance.RandomizeSfx(moveSound1, moveSound2);
        }
        

        CheckIfGameOver();
        GameManager.instance.playersTurn = false;
    }


    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Exit")
        {
            //GameManager.instance.level++;
            Invoke("Restart", restartLevelDelay);
            enabled = false;
        }
        else if (collision.tag == "Food")
        {
            food += pointPerFood;
            foodText.text = "+" + pointPerFood + " Food: " + food;
            SoundManager.instance.RandomizeSfx(eatSound1, eatSound2);
            collision.gameObject.SetActive(false);
        }
        else if (collision.tag == "Soda")
        {
            food += pointPerSoda;
            collision.gameObject.SetActive(false);
            SoundManager.instance.RandomizeSfx(drinkSound1, drinkSound2);
            collision.gameObject.SetActive(false);
        }
    }

    protected override void OnCantMove<T>(T component)
    {
        //스크립트가 Wall스크립트면 HitWall에 저장
        Wall hitWall = component as Wall;
        hitWall.DamageWall(wallDamage);
        //플레이어 애니를 실행
        animator.SetTrigger("PlayerChop");
    }

    private void Restart()
    {
        Application.LoadLevel(Application.loadedLevel);
    }

    public void LoseFood(int loss)
    {
        animator.SetTrigger("PlayerHit");
        food -= loss;
        foodText.text = "-" + loss + " Food: " + food;
        CheckIfGameOver();
    }

    private void CheckIfGameOver()
    {
        if (food <= 0)
        {
            SoundManager.instance.PlaySingle(gameOverSound);
            SoundManager.instance.musicSource.Stop();
            GameManager.instance.GameOver();
        }
    }
}
