using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class AudioData
{
    public int MasterVolume;
    public int MusicVolume;
    public int EffectsVolume;
}

public class ConfigurationManager
{

    static string GetConfigFolder()
    {

        string path = Path.Combine(Application.persistentDataPath, "Configs");

        if(!Directory.Exists(path)) Directory.CreateDirectory(path);

        return path;
    }

    static string GetAudioFilePath()
    {
        return Path.Combine(GetConfigFolder(), "Audio.json");
    }

    public static void SaveAudioToFile(AudioConfiguration configuration)
    {
        AudioData data = new AudioData();
        data.MasterVolume = configuration.MasterVolume;
        data.MusicVolume = configuration.MusicVolume;
        data.EffectsVolume = configuration.EffectsVolume;

        string saveData = JsonUtility.ToJson(data, true);
        
        string path = GetAudioFilePath();
        StreamWriter writer = new StreamWriter(path, false);
        writer.WriteLine(saveData);
        writer.Close();

        Debug.Log("Audio saved to " + path);
    }

    public static void LoadAudioFromFile(AudioConfiguration configuration)
    {

        string path = GetAudioFilePath();

        try {

            StreamReader reader = new StreamReader(path); 
            string gameStatus = reader.ReadToEnd();
            reader.Close();

            AudioData data = JsonUtility.FromJson<AudioData>(gameStatus);
            configuration.MasterVolume = data.MasterVolume;
            configuration.EffectsVolume = data.EffectsVolume;
            configuration.MusicVolume = data.MusicVolume;

        }
        catch (System.Exception)
        {

            Debug.Log("Audio config not found. Loading default...");
            configuration.Default();

        }

    }

}
