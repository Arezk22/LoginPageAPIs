using LoginPageAPIs.DTOs;
using LoginPageAPIs.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MimeKit;
using System.Text;

public class EmailController : ControllerBase
{
    private readonly EmailService _emailService;
    private readonly AppDbContext _context;

    public EmailController(EmailService emailService , AppDbContext context)
    {
        _emailService = emailService;
        _context = context;
    }


    [HttpPost("send-report/{childId}")]
    public async Task<IActionResult> SendEmailReport(int childId)
    {
        // 1. جلب بيانات الطفل
        var child = await _context.LoginRequests.FindAsync(childId);
        if (child == null)
            return NotFound("Child not found");

        // 2. جلب الدرجات
        var scores = await _context.Scores
            .Where(s => s.ChildId == childId)
            .ToListAsync();

        if (!scores.Any())
            return NotFound("No scores found for this child");

        var scoreReport = new StringBuilder();
        scoreReport.AppendLine("Scores Report:\n");

        foreach (var score in scores)
        {
            scoreReport.AppendLine(
                $"Exercise: {score.ExerciseName}, Score: {score.Mark}, Date: {score.Date:yyyy-MM-dd}");
        }

        var reports = scores.Select(s => new Report
        {
            ChildName =child.ChildName,
            ExerciseName = s.ExerciseName,
            Mark = s.Mark,
            Date = s.Date,
            ChildId = child.Id
        }).ToList();

        _context.Reports.AddRange(reports);
        await _context.SaveChangesAsync();

        var recipientEmail = child.Email;
        var subject = $"Score Report for {child.ChildName}";

        // 👇 هنا بقي تبعت الإيميل
        bool emailWasSent = await _emailService.SendEmailReportAsync(recipientEmail, subject, scoreReport.ToString());

        if (!emailWasSent)
            return Ok("Email sent successfully to " + recipientEmail);
        else
            return StatusCode(500, $"Failed to send email to {recipientEmail}.");
    }


}
