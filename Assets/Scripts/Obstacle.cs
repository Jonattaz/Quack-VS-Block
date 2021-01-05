using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Obstacle : MonoBehaviour
{
    //Public Variables
    public Text amountText;
    public AudioClip impactSound;
    public int amount;

    //Private Variables
    private Color initialColor;
    private Player player;
    private SpriteRenderer spriteRenderer;
    private float nextTime;

    
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    //Calls the PlayerDamage Method
    private void Update()
    {
        if (player != null && nextTime < Time.time)
        {
            PlayerDamage();
        }
    }

    //Deals with the obstacles
    public void SetAmount()
    {
        gameObject.SetActive(true);
        amount = Random.Range(0, LevelController.instance.obstaclesAmount);
        if (amount <= 0)
        {
            gameObject.SetActive(false);
        }
        SetAmountText();
        SetColor();
    }

    public void SetAmountText()
    {
        amountText.text = amount.ToString();

    }

    //Gives to the obstacles color
    public void SetColor()
    {
        int playerLives = FindObjectOfType<Player>().transform.childCount;
        Color newColor;

        if (amount > playerLives)
        {
            newColor = LevelController.instance.hardColor;

        }
        else if (amount > playerLives / 2)
        {
            newColor = LevelController.instance.mediumColor;
        }
        else
        {
            newColor = LevelController.instance.easyColor;
        }

        spriteRenderer.color = newColor;
        initialColor = newColor;
    }

    //this method is in charge for the damage that the player takes
    void PlayerDamage()
    {
        if (LevelController.instance.gameOver)
        {
            return;
        }

        AudioSource.PlayClipAtPoint(impactSound, Camera.main.transform.position);

        nextTime = Time.time + LevelController.instance.damageTime;
        player.TakeDamage();
        amount--;
        SetAmountText();
        if (amount <= 0)
        {
            gameObject.SetActive(false);
            player = null;
        }
        else
        {
            StopAllCoroutines();
            StartCoroutine(DamageColor());
        }
    }

    //coroutine that links the color of the obstacle to the damage done
    IEnumerator DamageColor()
    {
        float timer = 0;
        float t = 0;

        spriteRenderer.color = initialColor;

        while (timer < LevelController.instance.damageTime)
        {
            spriteRenderer.color = Color.Lerp(initialColor, Color.white, t);
            timer += Time.deltaTime;
            t += Time.deltaTime / LevelController.instance.damageTime;
            yield return null;

        }

        spriteRenderer.color = initialColor;
    }


    private void OnCollisionEnter2D(Collision2D other)
    {
        Player otherPlayer = other.gameObject.GetComponent<Player>();
        if (otherPlayer != null)
        {
            player = otherPlayer;
        }

    }

    private void OnCollisionExit2D(Collision2D other)
    {
        Player otherPlayer = other.gameObject.GetComponent<Player>();
        if (otherPlayer != null)
        {
            player = null;
        }
    }
}


















