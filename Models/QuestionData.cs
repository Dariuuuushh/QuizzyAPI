using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

public class QuestionData
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    [JsonProperty("question")]
    public string? Question { get; set; }
    [JsonProperty("correct_answer")]
    public string? CorrectAnswer { get; set; }
    [JsonProperty("incorrect_answers")]
    public List<string>? IncorrectAnswers { get; set; }
    [JsonProperty("category")]
    public string? Category { get; set; }
    [JsonProperty("type")]
    public string? Type { get; set; }
    [JsonProperty("difficulty")]
    public string? Difficulty { get; set; }
}
