using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PotatoPlayer : MonoBehaviour
{
    Vector2 initialPosition;

    Gun[] guns;

    float moveSpeed = 3;
    float speedMultiplier = 1;

    int hits = 3;

    public TextMeshProUGUI hitsText;
    bool invincible = false;
    float invincibleTimer = 0;
    float invincibleTime = 2;

    bool moveUp;
    bool moveDown;
    bool moveLeft;
    bool moveRight;
    bool speedUp;

    bool shoot;

    SpriteRenderer spriteRenderer;
    SpriteRenderer gunRender;

    public Sprite Gun1;
    public Sprite Gun2;
    public Sprite Gun3;

    GameObject shield;
    int powerUpGunLevel = 0;


    private void Awake()
    {
        initialPosition = transform.position;
        spriteRenderer = transform.Find("SpritePotato").GetComponent<SpriteRenderer>();
        hitsText = GameObject.Find("HitsText").GetComponent<TextMeshProUGUI>();
    
    }

    // Start is called before the first frame update
    void Start()
    {
        hitsText.text = hits.ToString();
        shield = transform.Find("Shield").gameObject;
        DeactivateShield();
        guns = transform.GetComponentsInChildren<Gun>();
        foreach(Gun gun in guns)
        {
            // gun.Shoot();
            gun.isActive = true;
            if (gun.powerUpLevelRequirement != 0)
            {
                gun.gameObject.SetActive(false);
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        moveUp = Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W);
        moveDown = Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S);
        moveLeft = Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A);
        moveRight = Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D);
        speedUp = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);

        shoot = Input.GetKeyDown(KeyCode.Space);
        if (shoot)
        {
            shoot = false;
            foreach(Gun gun in guns)
            {
                if (gun.gameObject.activeSelf)
                {
                    gun.Shoot();
                    AudioManager.Instance.Play("PewPew");

                }
            }
        }


        if (invincible)
        {

            if (invincibleTimer >= invincibleTime)
            {
                invincibleTimer = 0;
                invincible = false;
                spriteRenderer.enabled = true;
            }
            else
            {
                invincibleTimer += Time.deltaTime;
                spriteRenderer.enabled = !spriteRenderer.enabled;
            }
        }
    }

    private void FixedUpdate()
    {
        Vector2 pos = transform.position;

        float moveAmount = moveSpeed * speedMultiplier * Time.fixedDeltaTime;
        if (speedUp)
        {
            moveAmount *= 2;
        }
        Vector2 move = Vector2.zero;

        if (moveUp)
        {
            move.y += moveAmount;
        }

        if (moveDown)
        {
            move.y -= moveAmount;
        }

        if (moveLeft)
        {
            move.x -= moveAmount;
        }

        if (moveRight)
        {
            move.x += moveAmount;
        }

        float moveMagnitude = Mathf.Sqrt(move.x * move.x + move.y * move.y);
        if (moveMagnitude > moveAmount)
        {
            float ratio = moveAmount / moveMagnitude;
            move *= ratio;
        }

        pos += move;
        if (pos.x <= 1.5f)
        {
            pos.x = 1.5f;
        }
        if (pos.x >= 16f)
        {
            pos.x = 16;
        }
        if (pos.y <= 1)
        {
            pos.y = 1;
        }
        if (pos.y >= 9)
        {
            pos.y = 9;
        }

        transform.position = pos;
    }



    void ActivateShield()
    {
        shield.SetActive(true);
    }

    void DeactivateShield()
    {
        shield.SetActive(false);
    }

    bool HasShield()
    {
        return shield.activeSelf;
    }


    void AddGuns()
    {
       
       
        if(powerUpGunLevel<0){
            powerUpGunLevel=0;
        }
        if(powerUpGunLevel > 3 ){
            powerUpGunLevel=3;
        }
        foreach(Gun gun in guns)
        {
            if (gun.powerUpLevelRequirement <= powerUpGunLevel)
            {
              
                gun.gameObject.SetActive(true);

                // if(powerUpGunLevel==3){
                    
                // }
           
                // GameObject gunObject = GameObject.Find("Gun");    
                
            }
        
            else
            {
                gun.gameObject.SetActive(false);
            }
        }
               
            // //  
            if (guns[0] != null)
            {  
                    SetGunImage();
            }
        

          
    }

    void SetSpeedMultiplier(float mult)
    {
        speedMultiplier = mult;
    }

    void addHit()
    {
        hits++;
        hitsText.text = hits.ToString();
    }
 
    void SetGunImage()
    {

                gunRender = guns[0].gameObject.transform.Find("gunSprite").GetComponent<SpriteRenderer>();
                
                
                switch (powerUpGunLevel)
                    {
                        case 0:
                            gunRender.sprite = Gun1;
                            break;
                        case 1:
                            gunRender.sprite = Gun1;
                            break;
                        case 2:
                            gunRender.sprite = Gun2;
                            break;
                        case 3:
                            gunRender.sprite = Gun3;
                            break;
                    
                        default:
                            // handle unknown gun level
                            break;
                    }

}


    void ResetShip()
    {
        // transform.position = initialPosition;
        // DeactivateShield();
        // powerUpGunLevel = 0;
        // SetGunImage();
        // AddGuns();
        SetSpeedMultiplier(1);
        hits = 0;
        hitsText.text = hits.ToString();

        // Level.instance.AddScore();
       
        Level.instance.GameOver();
    }


    void Hit(GameObject gameObjectHit)
    {
        
        if (HasShield())
        {
           
          

            if(gameObjectHit.name.Contains("Bullet")) 
            {
                Destroy(gameObjectHit);
                
                DeactivateShield();
                invincible = true;
                // hits++;
            }
            else {
                
                if (!gameObjectHit.name.Contains("Destoyer") && !gameObjectHit.name.Contains("Dreadnot") && !gameObjectHit.name.Contains("MotherShip")) {
                Destroy(gameObjectHit);
                Level.instance.RemoveDestructable();
                }
                DeactivateShield();
            }
            
           
        }
        else
        {
            if (!invincible)
            {
                hits--;
                if (hits == 0)
                {
                    ResetShip();
                }
                else
                {
               
                    powerUpGunLevel--;

                    AddGuns();
                    invincible = true;

                }
                //  Level.instance.RemoveDestructable();
                // if(hits==2){
                //     Level.instance.RemoveDestructable();
                // } else {
                if(gameObjectHit.name.Contains("Bullet")){
                    Destroy(gameObjectHit);
                    hitsText.text = hits.ToString();
                }
                else {
           
               if (!gameObjectHit.name.Contains("Destoyer") && !gameObjectHit.name.Contains("Dreadnot") && !gameObjectHit.name.Contains("MotherShip")) {
                Destroy(gameObjectHit);
                Level.instance.RemoveDestructable();
                }
                // Destructable.DestroyDestructable();
                hitsText.text = hits.ToString();
                }
            }
        
        }
    
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        Bullet bullet = collision.GetComponent<Bullet>();
        if (bullet != null)
        {
            // string json = JsonUtility.ToJson(bullet);
           
            if (bullet.isEnemy)
            {
                Hit(bullet.gameObject);
          
            }
        }
    
        Destructable destructable = collision.GetComponent<Destructable>();
        
        
        if (destructable != null)
        {

            Hit(destructable.gameObject);
            //  Level.instance.RemoveDestructable();
           
                
        }
    
        PowerUp powerUp = collision.GetComponent<PowerUp>();
        if (powerUp)
        {
            if (powerUp.activateShield)
            {
                ActivateShield();
            }
            if (powerUp.addGuns)
            {
                powerUpGunLevel++;
                AddGuns();
            }
            if (powerUp.increaseSpeed)
            {
                SetSpeedMultiplier(speedMultiplier + 1);
           
            }
            if (powerUp.health) {
                addHit();
            }
            Level.instance.AddScore(powerUp.pointValue);
            Destroy(powerUp.gameObject);
        }
    }
}
