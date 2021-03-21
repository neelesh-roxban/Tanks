using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSystem : MonoBehaviour
{
    public GameObject bullet;


    public LineRenderer lr;

    public GameObject bulletSpawn;
    Vector3 spawnPoint;

    float force = 0;
    float Degrees = 0;
    

    

    float angle = 0;
    Vector2 direction;
    private void Awake()
    {
        ClearTrajectory();
    }
    void Start()
    {
        ClearTrajectory();
        
       
        
    }
    
    public void draw()
    {
        
        angle = Degrees * Mathf.Deg2Rad;
        direction.x = Mathf.Cos(angle);
        direction.y = Mathf.Sin(angle);
        lr.positionCount = 50;
        Debug.Log(angle);
        for (int i = 0; i < lr.positionCount; i++)
        {
            lr.SetPosition(i, CalculatePoint(i*0.1f));
                

        }

    }
    public void ClearTrajectory()
    {
        for (int i = 0; i < lr.positionCount; i++)
        {
            lr.SetPosition(i, Vector3.zero);


        }
    }
    Vector2 CalculatePoint(float time)
    {
        Vector2 currentpoint = (Vector2)spawnPoint + (direction.normalized * force * time) + 0.5f * Physics2D.gravity * time * time;
       
        return currentpoint;
    }


    public void ChangeForce(float newForce)
    {
        force = newForce;
        
    }

    public void ChangeDegrees(float newDegree)
    {
        Degrees = newDegree;
        
    }

    private void Update()
    {
        spawnPoint = bulletSpawn.transform.position;
        draw();
       

    }


    public void Fire()
    {

        angle = Degrees * Mathf.Deg2Rad;
        direction.x = Mathf.Cos(angle);
        direction.y = Mathf.Sin(angle);
        draw();
       
        GameObject BulletGO = GameObject.FindGameObjectWithTag("Bullet");
        if (BulletGO == null)
        {
            GameObject bulletGO = Instantiate(bullet, spawnPoint, Quaternion.identity);
            Rigidbody2D bulletRB = bulletGO.GetComponent<Rigidbody2D>();
            bulletRB.velocity = direction.normalized * force;

        }


    }

   
    
    

}


