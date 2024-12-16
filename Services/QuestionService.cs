using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using QuizzyAPI.Models;

public class QuestionService
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<QuestionService> _logger;
    private readonly ApplicationDbContext _context;

    public QuestionService(HttpClient httpClient, ILogger<QuestionService> logger, ApplicationDbContext context)
    {
        _httpClient = httpClient;
        _logger = logger;
        _context = context;
    }

    public async Task<List<QuestionData>> GetQuestionsFromApiAsync()
    {
        var response = await _httpClient.GetStringAsync("https://opentdb.com/api.php?amount=50&difficulty=medium");

        var apiResponse = JsonConvert.DeserializeObject<ApiResponse>(response);

        var newQuestions = new List<QuestionData>(); 

        foreach (var data in apiResponse?.Results ?? new List<QuestionData>())
        {
            
            var existingQuestion = await _context.Questions
                .FirstOrDefaultAsync(q => q.Question == data.Question);

            if (existingQuestion == null) 
            {
                var question = new QuestionData
                {
                    Question = data.Question,
                    CorrectAnswer = data.CorrectAnswer,
                    IncorrectAnswers = data.IncorrectAnswers,
                    Category = data.Category,
                    Type = data.Type,
                    Difficulty = data.Difficulty,
                };

                newQuestions.Add(question);
            }
            else
            {
                _logger.LogInformation("Frage bereits vorhanden: {Question}", data.Question);
            }
        }

        if (newQuestions.Any())
        {
            await _context.Questions.AddRangeAsync(newQuestions);
            await _context.SaveChangesAsync();
        }

        return newQuestions;
    }

}
