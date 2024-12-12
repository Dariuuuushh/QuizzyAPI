using System.ComponentModel.DataAnnotations;

public class Question
{
    [Key]
    public int Id { get; set; }
    public string Text { get; set; }
    public List<string> Options { get; set; }
    public int CorrectOptionIndex { get; set; }
}
