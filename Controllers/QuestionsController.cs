using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuizzyAPI.Models;

[Route("api/[controller]")]
[ApiController]
public class QuestionsController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public QuestionsController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: api/questions
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Question>>> GetQuestions()
    {
        return await _context.Questions.ToListAsync();
    }

    [HttpPost]
    public async Task<ActionResult<Question>> PostQuestion(Question question)
    {
        _context.Questions.Add(question);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetQuestions), new { id = question.Id }, question);
    }
}
