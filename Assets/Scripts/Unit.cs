using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
       

    public float maxHP;
    public float currentHP;

    void TakeDamage(float dmg)
    {
        currentHP -= dmg;

        

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Bullet"))
        {
            GameObject bullet = collision.gameObject;
            
            TakeDamage(bullet.GetComponent<Bullet>().dmg);
            GameController.instance.ChangeState();
            Destroy(bullet);
        }
    }

}
