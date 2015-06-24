using UnityEngine;
using System.Collections;

public class SpellCasting : MonoBehaviour {

    public UDEnemy[] enemies;

	// Use this for initialization
	void Start () 
    {
        enemies = Object.FindObjectsOfType(typeof(UDEnemy)) as UDEnemy[];
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (Input.GetKey(KeyCode.S))
        {
            CastSpell(0);
        }

        if (Input.GetKey(KeyCode.Space))
        {
            StartCoroutine("enemiesAttack");
        }
	}

    void CastSpell(int SpellID = 0, int MinimumPoints = 50)
    {
        UDEnemy enemyComp = GetComponent<UDEnemy>();

        if (enemyComp.magicPoint > MinimumPoints)
        {
            enemyComp.magicPoint -= MinimumPoints;

            print("cast a Spell!");
        }
    }

    public IEnumerator enemiesAttack()
    {
        foreach (UDEnemy en in enemies)
        {
            StartCoroutine(en.Attack());
            yield return new WaitForSeconds(en.attackDuration);
        }
        
    }
}
       