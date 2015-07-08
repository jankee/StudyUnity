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
            AttempMove(horizontal, vertical);
        }
	}

    protected override void AttempMove(int xDir, int yDir)
    {
        food--;
        print(food);

        base.AttempMove(xDir, yDir);
        CheckIfGameOver();
        GameManager.instance.playersTurn = false;
    }

    //protected override void AttempMove<T>(int xDir, int yDir)
    //{
    //    food--;
    //    base.AttempMove<T>(xDir, yDir);

    //    RaycastHit2D hit;
    //    CheckIfGameOver();
    //    GameManager.instance.playersTurn = false;
    //    print("Player : " + GameManager.instance.playersTurn);
    //}

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

    //protected override void OnCantMove<T>(T component)
    //{
    //    Wall hitWall = component as Wall;
    //    hitWall.DamageWall(wallDamage);
    //    animator.SetTrigger("PlayerChop");
    //}

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
