using Newtonsoft.Json;
using QuizzyAPI.Models;

public class QuestionBackgroundService : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<QuestionBackgroundService> _logger;
    private readonly HttpClient _httpClient;

    public QuestionBackgroundService(
        IServiceProvider serviceProvider,
        ILogger<QuestionBackgroundService> logger,
        HttpClient httpClient)
    {
        _serviceProvider = serviceProvider;
        _logger = logger;
        _httpClient = httpClient;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                await PopulateDatabaseWithQuestionsAsync();

                await Task.Delay(TimeSpan.FromHours(1), stoppingToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Fehler beim Abrufen der Fragen");
            }
        }
    }

    private async Task PopulateDatabaseWithQuestionsAsync()
    {
        var response = await _httpClient.GetStringAsync("https://opentdb.com/api.php?amount=50");

        var apiResponse = JsonConvert.DeserializeObject<ApiResponse>(response);

        // Hier implementierst du die Logik, um die Daten in deine DB zu speichern
        using (var scope = _serviceProvider.CreateScope())
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            foreach (var questionData in apiResponse?.Results ?? new List<QuestionData>())
            {
                var question = new QuestionData
                {
                    Question = questionData.Question,
                    CorrectAnswer = questionData.CorrectAnswer,
                    IncorrectAnswers = questionData.IncorrectAnswers,
                    Difficulty = questionData.Difficulty,
                    Type = questionData.Type,
                    Category = questionData.Category,
                };

                if (!dbContext.Questions.Any(q => q.Question == question.Question)) // Verhindere doppelte Fragen
                {
                    dbContext.Questions.Add(question);
                }
            }

            await dbContext.SaveChangesAsync();
        }
    }
}
