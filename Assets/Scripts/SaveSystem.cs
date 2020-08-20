using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{

    public static void SaveBall(GameManager ball) {

        BinaryFormatter formatter = new BinaryFormatter();

        string path = Application.persistentDataPath + "balls.fun";
        FileStream stream = new FileStream(path, FileMode.Create);

        BallData data = new BallData(ball);

        formatter.Serialize(stream, data);

        stream.Close();

        Debug.Log("Saved.");

    }

    public static BallData LoadBall() {

        string path = Application.persistentDataPath + "balls.fun";

        if (File.Exists(path)){
            BinaryFormatter formatter = new BinaryFormatter();

            FileStream stream = new FileStream(path, FileMode.Open);

            BallData data = formatter.Deserialize(stream) as BallData;

            stream.Close();

            Debug.Log("Loaded.");

            return data;
        }
        else {

            Debug.LogError("Save File not found in " + path);
            return null;
        }
    }
}
