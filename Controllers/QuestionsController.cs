using Microsoft.AspNetCore.Mvc;
using QuizzyAPI.Models;
using Microsoft.EntityFrameworkCore;

[Route("api/[controller]")]
[ApiController]
public class QuestionsController : ControllerBase
{
    private readonly ApplicationDbContext _dbContext;

    public QuestionsController(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<QuestionData>>> GetQuestions([FromQuery] string category, [FromQuery] string type, [FromQuery] string difficulty)
    {
        var questions = await _dbContext.Questions
            .Where(q => q.Category == category && q.Difficulty == difficulty && q.Type == type)
            .ToListAsync();

        if (!questions.Any())
        {
            return NotFound("Keine Fragen gefunden.");
        }

        return Ok(questions);
    }
}
