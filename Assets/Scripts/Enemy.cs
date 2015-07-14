using UnityEngine;
using System.Collections;

public class Enemy : MoveObject 
{
    public int playerDamage;

    private Animator animator;
    private Transform target;
    private bool skipMove;

    public AudioClip enemyAttack1;
    public AudioClip enemyAttack2;

	// Use this for initialization
	protected override void Start () 
    {
        GameManager.instance.AddEnemyToList(this);
        animator = GetComponent<Animator>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
        base.Start();
	}
	
	// Update is called once per frame
	void Update () 
    {
	
	}

    protected override void AttempMove<T>(int xDir, int yDir)
    {
        if (skipMove)
        {
            skipMove = false;
            return;
        }
        base.AttempMove<T>(xDir, yDir);

        skipMove = true;
    }

    public void MoveEnemy()
    {
        int xDir = 0;
        int yDir = 0;

        if (Mathf.Abs(target.position.x - transform.position.x) < float.Epsilon)
        {
            yDir = target.position.y > transform.position.y ? 1 : -1;
        }
        else
        {
            xDir = target.position.x > transform.position.x ? 1 : -1;
        }
        //플레이어 AttempMove에 xDir, yDir을 넣어 준다.
        AttempMove<Player>(xDir, yDir);
    }

    protected override void OnCantMove<T>(T component)
    {
        //component로 플레이어를 찾는다.
        Player hitPlayer = component as Player;

        animator.SetTrigger("EnemyAttack");

        hitPlayer.LoseFood(playerDamage);

        SoundManager.instance.RandomizeSfx(enemyAttack1, enemyAttack2);
    }
}
