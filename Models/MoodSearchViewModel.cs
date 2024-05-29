using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace Mooder.Models;

public class MoodSearchViewModel
{
    public List<UserMoodEntry>? Entries { get; set; }
    public SelectList? MoodSelectList { get; set; }
    public DateTime? SearchDate { get; set; }
    public UserMood? SearchMood { get; set; }

}