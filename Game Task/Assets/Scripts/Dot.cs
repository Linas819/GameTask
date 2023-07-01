using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Dot : MonoBehaviour
{
    public Sprite clickedSprite;
    private bool clicked = false;
    public TextMeshProUGUI numberText;
    GameLogic gameLogicScript;
    private void Start()
    {
        gameLogicScript = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameLogic>();
    }
    public void ChangeImage()
    {
        if (clicked == false) //Checking if the point has not been clicked
        {
            if (gameLogicScript.levelProgression == float.Parse(numberText.text) - 1) //Checking if the correct point in the order is pressed
            {
                clicked = true;
                this.GetComponent<Image>().overrideSprite = clickedSprite; //Changing the image of the point game object
                StartCoroutine(FadeOut(2f, numberText)); //starting the fadeout animation with a set time
                gameLogicScript.SetLevelProgression(1); //Adds to the level progression
                Destroy(numberText, 3); //Destroying the no longer nesecery Text object in the Point prefab
            }
        }
    }
    public IEnumerator FadeOut(float t, TextMeshProUGUI i)
    {
        while (i.color.a > 0.0f) //while the opacity is stil visible
        {
            i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a - (Time.deltaTime / t)); //sets a new collor with a lower opacity
            yield return null;
        }
    }
}
