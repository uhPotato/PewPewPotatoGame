using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Destructable : MonoBehaviour
{
    public GameObject explosion;

    public bool canBeDestroyed = false;
    public int scoreValue = 100;

    public int hits = 1;

    public AdditionalExplosion[] MultiExplosion;



    // Start is called before the first frame update
    void Start()
    {
        Level.instance.AddDestructable();
       
    }

    // Update is called once per frame
    void Update()
    {
     

  
// float targetPosition = -2f;
// float tolerance = 0.2f;

// if (transform.position.x >= targetPosition - tolerance && transform.position.x <= targetPosition + tolerance)
 if(transform.position.x < -2)
 {
            
             Level.instance.SubScore(scoreValue);
            //  Level.instance.RemoveDestructable();
            DestroyDestructable();
        }


        if (transform.position.x < 17.0f && !canBeDestroyed)
        {
            canBeDestroyed = true; 
            Gun[] guns = transform.GetComponentsInChildren<Gun>();
            foreach (Gun gun in guns)
            {
                gun.Shoot();
                gun.isActive = true;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!canBeDestroyed)
        {
            return;
        }
        Bullet bullet = collision.GetComponent<Bullet>();
        if (bullet != null)
        {
            if (!bullet.isEnemy) {
                hits--;
                Destroy(bullet.gameObject);
           
                if (hits == 0)
                {
              
                Level.instance.AddScore(scoreValue);
                DestroyDestructable();
               
                }
              

            }
        }
    }


    public void DestroyDestructable()
    {


        AudioManager.Instance.Play("Explosion");
        Instantiate(explosion, transform.position, Quaternion.identity);
       
        for(int i=0 ; i < MultiExplosion.Length; i++) {
          
            // Invoke("CreateAdditionalExplosion");
            CreateAdditionalExplosion(i);
        }

      
        Level.instance.RemoveDestructable();
        Destroy(gameObject);
    }

    
    public void CreateAdditionalExplosion(int ExplosionIndex) {

         AudioManager.Instance.Play("bigExplosion");
        Instantiate(MultiExplosion[ExplosionIndex].Explosions, transform.position +(Vector3) MultiExplosion[ExplosionIndex].offset, Quaternion.identity);
        // ExplosionIndex++;
     
    }
}

[Serializable] 
public class AdditionalExplosion {
    public GameObject Explosions;

    public Vector2 offset;

    public float loadDelay;


}