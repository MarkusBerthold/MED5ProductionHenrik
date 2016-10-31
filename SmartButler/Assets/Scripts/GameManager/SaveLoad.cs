using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace Assets.Scripts.GameManager{
    public static class SaveLoad
    {
        [System.Serializable]//Important! Every custom class that needs to be serialized has to be marked like this!
        public class Game
        { //don't need ": Monobehaviour" because we are not attaching it to a game object
            public string SavegameName;//used as the file name when saving as well as for loading a specific savegame
            public string TestString;//just a test variable of data we want to keep
        }


        /// <summary>
        /// Saves a .sav file to a specified path
        /// Takes a Game object
        /// </summary>
        /// <param name="saveGame"></param>
        public static void Save(Game saveGame)
        {

            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Create("Assets/Savegames/" + saveGame.SavegameName + ".sav"); //you can call it anything you want, including the extension. The directories have to exist though.
            bf.Serialize(file, saveGame);
            file.Close();
            Debug.Log("Saved Game: " + saveGame.SavegameName);

        }

        /// <summary>
        /// Loads a .sav file
        /// takes a string of the Game object to load
        /// </summary>
        /// <param name="gameToLoad"></param>
        /// <returns></returns>
        public static Game Load(string gameToLoad)
        {
            if (File.Exists("Assets/Savegames/" + gameToLoad + ".sav"))
            {
                BinaryFormatter bf = new BinaryFormatter();
                FileStream file = File.Open("Assets/Savegames/" + gameToLoad + ".gd", FileMode.Open);
                Game loadedGame = (Game)bf.Deserialize(file);
                file.Close();
                Debug.Log("Loaded Game: " + loadedGame.SavegameName);
            }
            else
            {
                Debug.Log("File doesn't exist!");
                return null;
            }

            return null;
        }
    }
}