using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEditor.Experimental.RestService;

public static class DataSaver
{
    // Start is called before the first frame update
   public static void saveData(LeaderboardController score)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/data.score";
        FileStream stream = new FileStream(path, FileMode.Create);


        SavingManger data = new SavingManger(score);

        formatter.Serialize(stream, data);
        stream.Close();

    }
    public static SavingManger LoadScore()
    {
        string path = Application.persistentDataPath + "/data.score";
        if(File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            SavingManger score = formatter.Deserialize(stream) as SavingManger;
            stream.Close();
            return score;
        }
        else
        {
            Debug.LogError("save file was not found in" + path);
            return null;
        }
    }
}
