using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using Melanchall.DryWetMidi.Core;
using Melanchall.DryWetMidi.Interaction;

public class SongManager : MonoBehaviour
{
    public static SongManager Instance;

    [Header("Audio")]
    public AudioSource audioSource;
    public float songDelayInSeconds = 1f;

    [Header("Gameplay")]
    public Lane[] lanes;
    public double marginOfError = 0.1;
    public int inputDelayInMilliseconds = 0;

    [Header("Note Movement")]
    public float noteTime = 2f;
    public float noteSpawnY = 5f;
    public float noteTapY = 0f;

    [Header("MIDI")]
    public string fileLocation;

    public static MidiFile midiFile;

    public float noteDespawnY
    {
        get
        {
            return noteTapY - (noteSpawnY - noteTapY);
        }
    }

    private void Awake()
    {
        // Singleton protection
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    private void Start()
    {
        if (Application.streamingAssetsPath.StartsWith("http://") ||
            Application.streamingAssetsPath.StartsWith("https://"))
        {
            StartCoroutine(ReadFromWebsite());
        }
        else
        {
            ReadFromFile();
        }
    }

    private IEnumerator ReadFromWebsite()
    {
        string path = Path.Combine(Application.streamingAssetsPath, fileLocation);

        using (UnityWebRequest www = UnityWebRequest.Get(path))
        {
            yield return www.SendWebRequest();

#if UNITY_2020_1_OR_NEWER
            if (www.result != UnityWebRequest.Result.Success)
#else
            if (www.isNetworkError || www.isHttpError)
#endif
            {
                Debug.LogError("MIDI Load Error: " + www.error);
            }
            else
            {
                byte[] results = www.downloadHandler.data;

                using (MemoryStream stream = new MemoryStream(results))
                {
                    midiFile = MidiFile.Read(stream);
                }

                GetDataFromMidi();
            }
        }
    }

    private void ReadFromFile()
    {
        string path = Path.Combine(Application.streamingAssetsPath, fileLocation);

        if (!File.Exists(path))
        {
            Debug.LogError("MIDI file not found: " + path);
            return;
        }

        midiFile = MidiFile.Read(path);

        GetDataFromMidi();
    }

    private void GetDataFromMidi()
    {
        var notes = midiFile.GetNotes();
        var noteArray = new Melanchall.DryWetMidi.Interaction.Note[notes.Count];

        notes.CopyTo(noteArray, 0);

        foreach (Lane lane in lanes)
        {
            lane.SetTimeStamps(noteArray);
        }

        Invoke(nameof(StartSong), songDelayInSeconds);
    }

    private void StartSong()
    {
        audioSource.Play();
    }

    public static double GetAudioSourceTime()
    {
        if (Instance == null || Instance.audioSource == null || Instance.audioSource.clip == null)
            return 0;

        return (double)Instance.audioSource.timeSamples /
               Instance.audioSource.clip.frequency;
    }
}