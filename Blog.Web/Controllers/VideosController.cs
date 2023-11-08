namespace Blog.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.Extensions.Caching.Memory;
    using Microsoft.EntityFrameworkCore.Storage;
    using System;
    using System.Threading.Tasks;
    using Data.Entities;
    using Data.Models.RequestModels.Video;
    using Services.Interfaces;
    using System.Security.Claims;

    [Route("Videos")]
    public class VideosController : Controller
    {
        private readonly IVideoService _videoService;
        private readonly IMemoryCache _memoryCache;

        public VideosController(IVideoService videoService, IMemoryCache memoryCache)
        {
            _videoService = videoService;
            _memoryCache = memoryCache;
        }

        [HttpGet]
        [Route("Dashboard")]
        public async Task<IActionResult> Dashboard()
        {
            var cacheKey = "VideoPreviewModelBundle";
            var videoPreviewModelBundle = await _memoryCache.GetOrCreateAsync(cacheKey, async entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10);
                return await _videoService.GetVideoPreviewModelBundleAsync();
            });

            return View(videoPreviewModelBundle);
        }

        [HttpGet("{id}")]
        [Route("Video/{id}")]
        public async Task<IActionResult> Video(string id)
        {
            var cacheKey = $"VideoViewModel_{id}";
            var videoViewModel = await _memoryCache.GetOrCreateAsync(cacheKey, async entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10);
                return await _videoService.GetVideoViewModelByIdAsync(id);
            });

            return View(videoViewModel);
        }

        [Authorize(Roles = "admin")]
        [HttpGet]
        [Route("Create")]
        public IActionResult Create()
        {
            return View();
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> Create(VideoCreateModel videoCreateModel)
        {
            if (!ModelState.IsValid)
            {
                return View("Create");
            }

            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value
                ?? throw new UnauthorizedAccessException();

            await _videoService.CreateAsync(videoCreateModel, userId);

            return RedirectToAction("Dashboard");
        }

        [Authorize(Roles = "admin")]
        [HttpGet]
        [Route("Edit/{id}")]
        public async Task<IActionResult> Edit(string? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            var videoEditViewModel = await _videoService.GetVideoEditViewModelByIdAsync(id);


            if (videoEditViewModel == null)
            {
                return NotFound();
            }


            return View(videoEditViewModel);
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        [Route("Edit/{id}")]
        public async Task<IActionResult> Edit(VideoEditModel videoEditModel, string? id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            var videoEditViewModel = await _videoService.GetVideoEditViewModelByIdAsync(id);

            try
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value
                    ?? throw new UnauthorizedAccessException();

                await _videoService.EditAsync(videoEditModel, id, userId);

                return RedirectToAction("Dashboard");
            }
            catch (RetryLimitExceededException /* dex */)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
            }

            return View(videoEditViewModel);
        }

        [Authorize(Roles = "admin")]
        [HttpGet]
        [Route("Delete/{id}")]
        public async Task<IActionResult> Delete(string? id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            var videoDeleteViewModel = await _videoService.GetVideoDeleteViewModelByIdAsync(id);

            return View(videoDeleteViewModel);

        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        [Route("Delete/{id}")]
        public async Task<IActionResult> DeleteVideo(string? id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            try
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value
                    ?? throw new UnauthorizedAccessException();

                await _videoService.DeleteAsync(id, userId);

                return RedirectToAction("Dashboard");
            }
            catch (RetryLimitExceededException /* dex */)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
            }

            return RedirectToAction("Dashboard");
        }
    }
}
