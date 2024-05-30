using NUnit.Framework;
using Mooder.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace MooderTest.ModelTests
{
    public class UserMoodEntryTests
    {
        [Test]
        public void IdPropertyTest()
        {
            var userMoodEntry = new UserMoodEntry();
            userMoodEntry.Id = 1;

            Assert.That(userMoodEntry.Id, Is.EqualTo(1));
        }

        [Test]
        public void DatePropertyTest()
        {
            var userMoodEntry = new UserMoodEntry();
            var date = DateTime.Now;
            userMoodEntry.Date = date;

            Assert.That(userMoodEntry.Date, Is.EqualTo(date));
        }

        [Test]
        public void TimePropertyTest()
        {
            var userMoodEntry = new UserMoodEntry();
            var time = DateTime.Now;
            userMoodEntry.Time = time;

            Assert.That(userMoodEntry.Time, Is.EqualTo(time));
        }

        [Test]
        public void MoodPropertyTest()
        {
            var userMoodEntry = new UserMoodEntry();
            userMoodEntry.Mood = UserMood.Happy;

            Assert.That(userMoodEntry.Mood, Is.EqualTo(UserMood.Happy));
        }

        [Test]
        public void NotesPropertyTest()
        {
            var userMoodEntry = new UserMoodEntry();
            userMoodEntry.Notes = "This is a test note.";

            Assert.That(userMoodEntry.Notes, Is.EqualTo("This is a test note."));
        }

        // [Test]
        // public void RequiredDateValidationTest()
        // {
        // }

    }
}
