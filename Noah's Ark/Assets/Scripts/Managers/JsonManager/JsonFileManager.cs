using System.IO;
using UnityEngine;

public class JsonFileManager : MonoBehaviour
{
    /// <summary>
    /// Reads json data from path/options/optionType
    /// </summary>
    /// <param name="optionType">Option name</param>
    /// <returns>Json Data, null when there is no file</returns>
    static public string Read(string optionType)
    {
        if (!File.Exists($"{Application.persistentDataPath}/options/{optionType}")) // 파일 존제 채크
        {
            return null;
        }

        return File.ReadAllText($"{Application.persistentDataPath}/options/{optionType}");
    }

    static public void Write(string optionType, string json)
    {
        if (!File.Exists($"{Application.persistentDataPath}/options/{optionType}")) // 파일 존제 안할 시 파일 생성
        {
            Directory.CreateDirectory($"{Application.persistentDataPath}/options/"); // mkdir
            File.Create($"{Application.persistentDataPath}/options/{optionType}").Close(); // touch
            Debug.Log("Created folder at: " + Application.persistentDataPath + "/options/"); // nvim
        }


        StreamWriter outputFile = new StreamWriter($"{Application.persistentDataPath}/options/{optionType}"); // 파일에 작성
        outputFile.WriteLine(json);
        Debug.Log("Saved at : " + Application.persistentDataPath);
        outputFile.Close();
    }
}