using System;
using System.Collections.Generic;
using UnityEngine;
using Melanchall.DryWetMidi.Interaction;
using Melanchall.DryWetMidi.MusicTheory;

public class Lane : MonoBehaviour
{
    [Header("Lane Settings")]
    public NoteName noteRestriction;
    public KeyCode input;
    public GameObject notePrefab;

    private List<double> timeStamps = new List<double>();

    // Queue keeps notes perfectly ordered
    private Queue<Note> spawnedNotes = new Queue<Note>();

    private int spawnIndex = 0;
    private int inputIndex = 0;

    public void SetTimeStamps(Melanchall.DryWetMidi.Interaction.Note[] notes)
    {
        foreach (var note in notes)
        {
            if (note.NoteName == noteRestriction)
            {
                MetricTimeSpan metricTime = TimeConverter.ConvertTo<MetricTimeSpan>(
                    note.Time,
                    SongManager.midiFile.GetTempoMap()
                );

                double timeStamp =
                    metricTime.Minutes * 60d +
                    metricTime.Seconds +
                    metricTime.Milliseconds / 1000d;

                timeStamps.Add(timeStamp);
            }
        }
    }

    private void Update()
    {
        SpawnNotes();
        HandleInput();
        HandleMisses();
    }

    private void SpawnNotes()
    {
        if (spawnIndex >= timeStamps.Count)
            return;

        double songTime = SongManager.GetAudioSourceTime();

        if (songTime >= timeStamps[spawnIndex] - SongManager.Instance.noteTime)
        {
            GameObject obj = Instantiate(notePrefab, transform);

            Note note = obj.GetComponent<Note>();
            note.assignedTime = (float)timeStamps[spawnIndex];

            spawnedNotes.Enqueue(note);

            spawnIndex++;
        }
    }

    private void HandleInput()
    {
        if (inputIndex >= timeStamps.Count)
            return;

        if (!Input.GetKeyDown(input))
            return;

        if (spawnedNotes.Count == 0)
        {
            ScoreManager.Miss();
            return;
        }

        double audioTime =
            SongManager.GetAudioSourceTime() -
            (SongManager.Instance.inputDelayInMilliseconds / 1000.0);

        double expectedTime = timeStamps[inputIndex];
        double difference = Math.Abs(audioTime - expectedTime);

        if (difference <= SongManager.Instance.marginOfError)
        {
            Hit();
        }
        else
        {
            Debug.Log($"Bad hit timing: {difference:F3}s");
            ScoreManager.Miss();
        }
    }

    private void HandleMisses()
    {
        if (inputIndex >= timeStamps.Count)
            return;

        double audioTime =
            SongManager.GetAudioSourceTime() -
            (SongManager.Instance.inputDelayInMilliseconds / 1000.0);

        double expectedTime = timeStamps[inputIndex];

        if (audioTime > expectedTime + SongManager.Instance.marginOfError)
        {
            Miss();
        }
    }

    private void Hit()
    {
        ScoreManager.Hit();

        if (spawnedNotes.Count > 0)
        {
            Note note = spawnedNotes.Dequeue();

            if (note != null)
            {
                Destroy(note.gameObject);
            }
        }

        Debug.Log($"Hit note {inputIndex}");

        inputIndex++;
    }

    private void Miss()
    {
        ScoreManager.Miss();

        if (spawnedNotes.Count > 0)
        {
            Note note = spawnedNotes.Dequeue();

            if (note != null)
            {
                Destroy(note.gameObject);
            }
        }

        Debug.Log($"Missed note {inputIndex}");

        inputIndex++;
    }
}