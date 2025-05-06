using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LoginPageAPIs.DTOs;
using LoginPageAPIs.Models;
using LoginPageAPIs.Helpers;
using Microsoft.CodeAnalysis.Scripting;
using Org.BouncyCastle.Crypto.Generators;
using BCrypt.Net;

namespace LoginPageAPIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly JwtService _jwtService;

        public AuthController(AppDbContext context, JwtService jwtService)
        {
            _context = context;
            _jwtService = jwtService;
        }

        // ✅ تسجيل مستخدم جديد
        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // التأكد إن الإيميل مش متسجل قبل كده
            var existingUser = await _context.LoginRequests
                .FirstOrDefaultAsync(u => u.Email == request.Email);

            if (existingUser != null)
                return BadRequest("User with this email already exists.");

            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(request.Password);
            var newUser = new LoginRequest
            {
                ChildName = request.ChildName,
                FatherName = request.FatherName,
                Email = request.Email,
                Password = hashedPassword
            };

            _context.LoginRequests.Add(newUser);
            await _context.SaveChangesAsync();

            return Ok(new { Message = "User registered successfully" });
        }

        // ✅ تسجيل الدخول وإرجاع JWT Token
        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginRequestDto request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
           

            var user = await _context.LoginRequests.FirstOrDefaultAsync(u => u.Email == request.Email);

            if (user == null || !BCrypt.Net.BCrypt.Verify(request.Password, user.Password))
                return Unauthorized("Invalid email or password.");

            // توليد التوكين
            var token = _jwtService.GenerateToken(user);

            return Ok(new
            {
                Message = "Login successful",
                Token = token,
                User = new
                {
                    user.Id,
                    user.ChildName,
                    user.FatherName,
                    user.Email
                }
            });
        }

        [HttpPost("upload-profile-image")]
        public async Task<IActionResult> UploadProfileImage(IFormFile file, int userId)
        {
            // التحقق من وجود الملف
            if (file == null || file.Length == 0)
                return BadRequest("No file uploaded.");

            // تحديد اسم الملف باستخدام Guid أو اسم المستخدم
            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);

            // تحديد المسار الكامل حيث سيتم تخزين الصورة
            var uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", fileName);

            // إنشاء المجلد إذا لم يكن موجودًا
            var directoryPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images");
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            // حفظ الملف على السيرفر
            using (var fileStream = new FileStream(uploadPath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }

            // تخزين المسار في قاعدة البيانات
            var imageUrl = "/images/" + fileName;  // هذا المسار سيكون رابطًا للوصول إلى الصورة

            // افتراض أنه لديك متغير userId كـ parameter أو جزء من body
            var user = await _context.LoginRequests.FindAsync(userId);  // أو استخدام اسم الكلاس المناسب

            if (user == null)
            {
                return NotFound("User not found.");
            }

            // تحديث المسار في الـ User
            user.ProfileImagePath = imageUrl; // افترض أن لديك خاصية ProfileImagePath في الكلاس LoginRequest

            // حفظ التغييرات في قاعدة البيانات
            await _context.SaveChangesAsync();

            // إرجاع رابط الصورة بعد تحميلها
            return Ok(new { ImageUrl = imageUrl });
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _context.LoginRequests.FindAsync(id);

            if (user == null)
                return NotFound(new { Message = "User not found." });

            _context.LoginRequests.Remove(user);
            await _context.SaveChangesAsync();

            return Ok(new { Message = "User deleted successfully." });
        }


    }
}
