//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using LoginPageAPIs.Models;

//namespace LoginPageAPIs.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class ScoresController : ControllerBase
//    {
//        private readonly AppDbContext _context;

//        public ScoresController(AppDbContext context)
//        {
//            _context = context;
//        }

//        // GET: api/Scores
//        [HttpGet]
//        public async Task<ActionResult<IEnumerable<Score>>> GetScores()
//        {
//            return await _context.Scores.ToListAsync();
//        }

//        // GET: api/Scores/5
//        [HttpGet("{id}")]
//        public async Task<ActionResult<Score>> GetScore(int id)
//        {
//            var score = await _context.Scores.FindAsync(id);

//            if (score == null)
//            {
//                return NotFound();
//            }

//            return score;
//        }

//        // PUT: api/Scores/5
//        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
//        [HttpPut("{id}")]
//        public async Task<IActionResult> PutScore(int id, Score score)
//        {
//            if (id != score.Id)
//            {
//                return BadRequest();
//            }

//            _context.Entry(score).State = EntityState.Modified;

//            try
//            {
//                await _context.SaveChangesAsync();
//            }
//            catch (DbUpdateConcurrencyException)
//            {
//                if (!ScoreExists(id))
//                {
//                    return NotFound();
//                }
//                else
//                {
//                    throw;
//                }
//            }

//            return NoContent();
//        }

//        // POST: api/Scores
//        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
//        [HttpPost]
//        public async Task<ActionResult<Score>> PostScore(Score score)
//        {
//            _context.Scores.Add(score);
//            await _context.SaveChangesAsync();

//            return CreatedAtAction("GetScore", new { id = score.Id }, score);
//        }

//        // DELETE: api/Scores/5
//        [HttpDelete("{id}")]
//        public async Task<IActionResult> DeleteScore(int id)
//        {
//            var score = await _context.Scores.FindAsync(id);
//            if (score == null)
//            {
//                return NotFound();
//            }

//            _context.Scores.Remove(score);
//            await _context.SaveChangesAsync();

//            return NoContent();
//        }

//        private bool ScoreExists(int id)
//        {
//            return _context.Scores.Any(e => e.Id == id);
//        }
//    }
//}


using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LoginPageAPIs.Models;
using LoginPageAPIs.DTOs;

namespace LoginPageAPIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScoresController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ScoresController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Scores
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Score>>> GetScores()
        {            
            var scores = await _context.Scores.Select(s => new ScoreReadDto
            {

                ChildName = s.Child.ChildName,
                FatherName = s.Child.FatherName,
                ExerciseName = s.ExerciseName,
                Mark = s.Mark,
                Email = s.Child.Email,
                Date = s.Date
            })
                .ToListAsync();
            return Ok(scores);
        }

        // GET: api/Scores/Child/5
        [HttpGet("Child/{childId}")]
        public async Task<ActionResult<IEnumerable<ScoreReadDto>>> GetScoresForChild(int childId)
        {
            var scores = await _context.Scores
                .Where(s => s.ChildId == childId)
                .Select(s => new ScoreReadDto
                {
                    ChildName = s.Child.ChildName,
                    FatherName = s.Child.FatherName,
                    ExerciseName = s.ExerciseName,
                    Mark = s.Mark,
                    Email=s.Child.Email,
                    Date = s.Date
                })
                .ToListAsync();

            if (scores == null || !scores.Any())
                return NotFound($"No scores found for childId = {childId}");

            return Ok(scores);
        }

        // POST: api/Scores
        [HttpPost]
        public async Task<ActionResult<Score>> PostScore(ScoreCreateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var child = await _context.LoginRequests.FindAsync(dto.ChildId);
            if (child == null)
                return NotFound("Child not found");

            var score = new Score
            {
                ChildId = dto.ChildId,
                ExerciseName = dto.ExerciseName,
                Mark = dto.Mark,
                Date = DateTime.Now
            };

            _context.Scores.Add(score);
            await _context.SaveChangesAsync();

            return Ok(new { Message = "Score recorded successfully", Score = score });
        }


        // DELETE: api/Scores/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteScore(int id)
        {
            var score = await _context.Scores.FindAsync(id);
            if (score == null)
                return NotFound();

            _context.Scores.Remove(score);
            await _context.SaveChangesAsync();

            return Ok("Score deleted");
        }
    }
}
