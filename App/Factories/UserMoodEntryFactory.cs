// Factories/UserMoodEntryFactory.cs
using System;
using System.Collections.Generic;
using Mooder.Models;

namespace Mooder.Factories
{
  public static class UserMoodEntryFactory
  {
    private static Random _random = new Random();

    private static readonly string[] Notes = new[]
    {
            "Had a great day at work.",
            "Feeling down due to bad weather.",
            "An average day, nothing special.",
            "Excited about the weekend!",
            "Had a wonderful time with friends.",
            "Work was very stressful.",
            "Enjoyed a relaxing evening.",
            "Went for a nice walk.",
            "Had a productive day.",
            "Feeling a bit under the weather.",
            "Went to a fun party.",
            "Had a quiet day at home.",
            "Got some good news today.",
            "Had a tough day.",
            "Feeling very motivated.",
            "Feeling a bit anxious.",
            "Spent time with family.",
            "Had a great workout.",
            "Feeling optimistic about the future.",
            "Feeling very tired.",
            "Had an amazing meal.",
            "Feeling very creative today.",
            "Spent the day reading a good book.",
            "Had a pleasant surprise.",
            "Feeling very grateful.",
            "Had a long day at work.",
            "Feeling very calm and relaxed.",
            "Had a busy day running errands.",
            "Spent time doing hobbies.",
            "Feeling a bit lonely."
        };

    public static UserMoodEntry Create(DateTime date, DateTime time, UserMood mood, string notes)
    {
      return new UserMoodEntry
      {
        Date = date,
        Time = time,
        Mood = mood,
        Notes = notes
      };
    }

    public static List<UserMoodEntry> GenerateEntries(int numberOfEntries)
    {
      var entries = new List<UserMoodEntry>();
      for (int i = 0; i < numberOfEntries; i++)
      {
        var date = new DateTime(2024, 1, 1).AddDays(_random.Next(0, 150));
        var time = date.AddHours(_random.Next(0, 24)).AddMinutes(_random.Next(0, 60));
        var mood = (UserMood)_random.Next(1, 6);
        var notes = Notes[_random.Next(Notes.Length)];

        entries.Add(Create(date, time, mood, notes));
      }
      return entries;
    }
  }
}
