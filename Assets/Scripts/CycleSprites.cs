using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CycleSprites : MonoBehaviour
{
    public Sprite[] sprites;
    private int currentSpriteIndex = 0;
    public Sprite Codec1;
    public Sprite Codec2;
    public Sprite Codec3;
    public Sprite Codec4;

 
    private SpriteRenderer spriteRenderer;

    void Start()

    {
        sprites = new Sprite[4];
        sprites[0] = Codec1;
        sprites[1] = Codec2;
        sprites[2] = Codec3;
        sprites[3] = Codec4;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            currentSpriteIndex++;
            if (currentSpriteIndex >= sprites.Length)
            {
                currentSpriteIndex = 3;
            }
            spriteRenderer.sprite = sprites[currentSpriteIndex];
        }
    }
}
