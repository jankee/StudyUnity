using UnityEngine;
using System.Collections;

public class UDEnemy : MonoBehaviour 
{
    public int Health = 100;

    public int magicPoint = 100;

    public float attackDuration = 2.0f;

    private float spinSpeed = 90.0f;

    public void Update()
    {
        ////this.transform.Rotate( 0, 90 * Time.deltaTime, 0);

        //transform.Translate(0, 0, 1 * 5 * Time.deltaTime);

        //print(transform.forward);
    }

    public IEnumerator Attack()
    {
        float elapsedTime = 0.0f;

        while (elapsedTime < attackDuration)
        {
            this.transform.Rotate(0, spinSpeed * Time.deltaTime, 0);

            elapsedTime += Time.deltaTime;

            yield return null;
        }

    }

    public void OnkeyEvent()
    {
        print("This is Object " + gameObject.name + ", and you pressed the E key"); 
    }

}
