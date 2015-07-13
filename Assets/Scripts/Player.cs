using UnityEngine;
using System.Collections;

public class Player : MoveObject 
{
    public int wallDamage = 1;
    public int pointPerFood = 10;
    public int pointPerSoda = 20;
    public float restartLevelDelay = 1f;

    private Animator animator;
    private int food;

    private Wall hitComponent;

	// Use this for initialization
	protected override void Start () 
    {
        animator = GetComponent<Animator>();
        food = GameManager.instance.playerFoodPoint;
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

        horizontal = (int)Input.GetAxisRaw("Horizontal");
        vertical = (int)Input.GetAxisRaw("Vertical");

        
        //print("hirizontal : " + horizontal);
        //print("vertical : " + vertical);

        if (horizontal != 0)
        {
            vertical = 0;
        }

        if (horizontal != 0 || vertical != 0)
        {
            AttempMove<Wall>(horizontal, vertical);
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            print("Space");
            animator.SetTrigger("PlayerChop");
        }
	}

    protected override void AttempMove<T>(int xDir, int yDir)
    {
        food--;
        
        RaycastHit2D hit;

        base.AttempMove<T>(xDir, yDir);

        CheckIfGameOver();
        GameManager.instance.playersTurn = false;
    }


    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Exit")
        {
            Invoke("Restart", restartLevelDelay);
            enabled = false;
        }
        else if (collision.tag == "Food")
        {
            food += pointPerFood;
            collision.gameObject.SetActive(false);
        }
        else if (collision.tag == "Soda")
        {
            food += pointPerSoda;
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
        CheckIfGameOver();
    }

    private void CheckIfGameOver()
    {
        if (food <= 0)
        {
            GameManager.instance.GameOver();
        }
    }
}
