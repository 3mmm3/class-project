using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _3M.Dtos.Account;
using _3M.Repositories;
using _3M.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace _3M.Controllers
{
    [Authorize]
    public class UserProfileController : Controller
    {
        private readonly UserprofileRepository _userProfileRepository;
        private readonly NotificationManager _notificationManager;

        public UserProfileController(UserprofileRepository userProfileRepository,
            NotificationManager notificationManager)
        {
            _userProfileRepository = userProfileRepository;
            _notificationManager = notificationManager;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var userIdClaim = User.Claims.FirstOrDefault(i => i.Type.Contains("nameidentifier"));
            if (userIdClaim == null || !Guid.TryParse(userIdClaim.Value, out var userId))
            {
                return Forbid();
            }

            var userProfile = await _userProfileRepository.GetUserProfileAsync(userId) ??
              new UserProfileDto()
              {
                  UserId = userId
              };

            return View(userProfile);
        }

        [HttpPost]
        public async Task<IActionResult> Index(UserProfileDto userProfile)
        {
            if (ModelState.IsValid)
            {
                var userIdClaim = User.Claims.FirstOrDefault(i => i.Type.Contains("nameidentifier"));
                if (userIdClaim == null ||
                    !Guid.TryParse(userIdClaim.Value, out var userId) ||
                    userId != userProfile.UserId)
                {
                    return Forbid();
                }

                await _userProfileRepository.RegisterUserProfileAsync(userProfile);
                userProfile = await _userProfileRepository.GetUserProfileAsync(userProfile.UserId);

                _notificationManager.Set(new Notification("پروفایل کاربری شما با موفقیت بروزرسانی شد.", "success"));
            }
            return View(userProfile);
        }
    }
}