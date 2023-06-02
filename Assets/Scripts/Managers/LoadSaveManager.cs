using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

public class LoadSaveManager : MonoBehaviour
{

    // Save game data

    public class GameStateData
    {
        public struct DataTransform
        {

            public float posX;
            public float posY;
            public float posZ;
            public float rotX;
            public float rotY;  
            public float rotZ;
            public float scaleX;
            public float scaleY;
            public float scaleZ;







        }

        // Data for enemy
        public class DataEnemy
        {
            //Enemy Transform Data
            public DataTransform posRotScale;

            //Enemy ID
            public int EnemyID;

            //Health
            public int health;

        }

        // Data for player
        public class DataPlayer
        {
            //Transform Data
            public DataTransform posRotScale;

            //Collected combo power up?
            public bool collectedPowerUp;

            //Has Collected sword ?
            public bool collectedSword;

            //Health
            public int health;
        }

        // Instance variables

        public List<DataEnemy> enemies = new List<DataEnemy>();
        public DataPlayer player = new DataPlayer();


    }

    // Game data to save/load
    public GameStateData gameState = new GameStateData();


    // Saves game data to XML file
    public void Save(string fileName = "GameData.xml")
    {
        // Clear existing enemy data


        // Save game data





    }

    // Load game data from XML file
    public void Load(string fileName = "GameData.xml")
    {





    }
}