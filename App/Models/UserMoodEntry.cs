using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Mooder.Models;
public class UserMoodEntry
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Please enter a date.")]
    [DataType(DataType.Date)]
    public DateTime Date { get; set; }

    [Required(ErrorMessage = "Please enter a time.")]
    [DataType(DataType.Time)]
    public DateTime Time { get; set; }

    [Required(ErrorMessage = "Please select a mood.")]
    public UserMood Mood { get; set; }

    [Required(ErrorMessage = "Please enter a note.")]
    public string? Notes { get; set; }

}