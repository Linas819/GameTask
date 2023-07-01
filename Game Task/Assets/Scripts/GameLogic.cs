using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameLogic : MonoBehaviour
{
    Levels gameLevels;
    public int levelProgression;
    public GameObject pointObject, canvas, menu;
    public List<GameObject> points;
    // Start is called before the first frame update
    void Start()
    {
        ReadData(); //reads data from Json file
    }

    void ReadData()
    {
        string DataLocation = "C:/Users/Linas/GameTaskRepo/GameTask/Game Task/Assets/GameAssets/level_data.json"; // the location of the json file in build
        string jsonString = File.ReadAllText(DataLocation); // reading the json file
        gameLevels = JsonUtility.FromJson<Levels>(jsonString); //attaching the Json file's data to a Levels object vriable
    }

    public void PlayGame(int BTN_Pressed) //menu
    {
        levelProgression = 0; //sets the progression to 0 at the start of every level
        //script.positions = new List<Transform>(); //creates a new list for Transform, in the LineRenderer's Line script, at the start of every level
        points = new List<GameObject>(); //creates a new list for the points GameObject at the start of every level
        switch (BTN_Pressed)
        {
            //cases choose the level
            case 0:
            case 1:
                CreateLevel(BTN_Pressed);
                break;
            default:
                Application.Quit();
                break;
        }
        menu.SetActive(false); //once the level starts, the menus UI is deactivated
    }

    public void CreateLevel(int currentLevel)
    {
        int currentPoint = 0; //variable used to track the current point ebing added in a level
        for (int i = 0; i < gameLevels.levels[currentLevel].levelData.Length; i = i + 2)
        {
            points.Add(Instantiate(pointObject, new Vector2(float.Parse(gameLevels.levels[currentLevel].levelData[i]), float.Parse(gameLevels.levels[currentLevel].levelData[i + 1]) * -1), Quaternion.identity));
            points[currentPoint].GetComponentInChildren<TextMeshProUGUI>().text = (currentPoint + 1).ToString(); //seting the numeric value of the boind to a text gameobject
            points[currentPoint].transform.SetParent(canvas.transform, false); //transforming the current point game object, to be the child of the canvas game object.
            currentPoint++;//setting up the new point's index 
        }
    }

    public void SetLevelProgression(int i) // adds to how many points are clicked and draws the line
    {
        levelProgression++;
    }

    public void EndLevel() //destroys the cirrent points and reactivates the menu UI
    {
        for (int i = 0; i < points.Count; i++)
        {
            Destroy(points[i]);
        }
        menu.SetActive(true);
    }

    [System.Serializable]
    public class LevelData
    {
        public string[] levelData;
    }

    [System.Serializable]
    public class Levels
    {
        public LevelData[] levels;
    }
}
