using UnityEngine;
using System.Collections;

public class Player : MovingObject 
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
        if (!GameManager.instance.playerTurn)
        {
            return;
        }

        int horizotal = 0;
        int vertical = 0;

        horizotal = (int)Input.GetAxisRaw("Horizontal");
        vertical = (int)Input.GetAxisRaw("Vertical");

        if (horizotal != 0)
        {
            vertical = 0;
        }

        if (horizotal != 0 || vertical != 0)
        {
            AttemptMove<Wall>(horizotal, vertical);
        }
	}

    protected override void AttemptMove<T>(int xDir, int yDir)
    {
        food--;
        base.AttemptMove<T>(xDir, yDir);
        RaycastHit2D hit;
        CheckIfGameOver();
        GameManager.instance.playerTurn = false;
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Exit")
        {
            Invoke("Restart", restartLevelDelay);
            enabled = false;
        }
        else if (other.tag == "Food")
        {
            food += pointPerFood;
            other.gameObject.SetActive(false);
        }
        else if (other.tag == "Soda")
        {
            food += pointPerSoda;
            other.gameObject.SetActive(false);
        }
    }



    protected override void OnCantMove<T>(T component)
    {
        Wall hitWall = component as Wall;
        hitWall.DamageWall(wallDamage);
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
