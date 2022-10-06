using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum GameMode1
{
    idle,
    playing,
    LevelEnd
}

public class PrototypeManager : MonoBehaviour
{
    static private PrototypeManager S;

    [Header("Set in Inspector")]
    public Text uitLevel;
    public Text uitShots;
    public Text uitButton;
    public Vector3 castlePos;
    public GameObject[] castles;

    [Header("Set Dynamically")]
    public int level;
    public int levelMax;
    public int shotsTaken;
    public GameObject castle;
    public GameMode1 mode1 = GameMode1.idle;
    public string showing = "Show Slingshot";
    // Start is called before the first frame update
    void Start()
    {
        S = this;

        level = 0;
        levelMax = castles.Length;
        StartLevel();
    }

    void StartLevel()
    {
        //get rid of old castle if one exists
        if (castle != null)
        {
            Destroy(castle);
        }

        //Destroy old projectiles if they exist
        GameObject[] gos = GameObject.FindGameObjectsWithTag("Projectile");
        foreach (GameObject pTemp in gos)
        {
            Destroy(pTemp);
        }

        //Instantiate the new castle
        castle = Instantiate<GameObject>(castles[level]);
        castle.transform.position = castlePos;
        shotsTaken = 0;

        //reset camera
        SwitchView1("Show Both");
        ProjectileLine.S.Clear();

        //reset goal
        Goal.goalMet = false;

        UpdateGUI();

        mode1 = GameMode1.playing;
    }

    private void UpdateGUI()
    {
        uitLevel.text = "Level: " + (level + 1) + "of " + levelMax;
        uitShots.text = "Shots Taken: " + shotsTaken;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateGUI();

        //check for level end

        if ( (mode1 == GameMode1.playing) && Goal.goalMet)
        {
            //change mode to stop checking for level end
            mode1 = GameMode1.LevelEnd;

            //Zoom Out
            SwitchView1("Show Both");

            //Start the next level in 2 seconds

            Invoke("NextLevel", 2f);


        }
    }

    void NextLevel()
    {
        level++;

        if (level == levelMax)
        {
            level = 0;
        }
        StartLevel();
    }

    public void SwitchView1(string eView = "")
    {
        if (eView == "")
        {
            eView = uitButton.text;
        }
        showing = eView;
        switch (showing)
        {
            case "Show Slingshot":
                FollowCam.POI = null;
                uitButton.text = "Show Hoop";
                break;

            case "Show Hoop":
                FollowCam.POI = S.castle;
                uitButton.text = "Show Both";
                break;

            case "Show Both":
                FollowCam.POI = GameObject.Find("ViewBoth");
                uitButton.text = "Show Slingshot";
                break;
        }
    }

    //static method that allows code anywhere to increment shotsTaken
    public static void ShotFired()
    {
        S.shotsTaken++;
    }
}
