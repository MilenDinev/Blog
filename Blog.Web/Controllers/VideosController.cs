namespace Blog.Web.Controllers
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore.Storage;
    using Data.Entities;
    using Data.Models.RequestModels.Video;
    using Data.Models.ViewModels.Video;
    using Services.Interfaces;

    [Route("Videos")]
    public class VideosController : Controller
    {
        IVideoService videoService;
        UserManager<User> userManager;

        public VideosController(IVideoService videoService, UserManager<User> userManager)
        {
            this.videoService = videoService;
            this.userManager = userManager;
        }

        [Route("Dashboard")]
        public async Task<IActionResult> Dashboard()
        {
            var videoPreviewModelBundle = await videoService.GetVideoPreviewModelBundleAsync();
            return View(videoPreviewModelBundle);
        }

        [Route("Video/{id}")]
        [HttpGet("{id}")]
        public async Task<IActionResult> Video(string id)
        {
            var videoViewModel = await this.videoService.GetVideoViewModelByIdAsync(id);

            return View(videoViewModel);
        }

        [Route("Create")]
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [Route("Create")]
        [HttpPost]
        public async Task<IActionResult> Create(VideoCreateModel videoCreateModel)
        {

            if (!ModelState.IsValid)
            {
                return View("Create");
            }


            var videoViewModel = new VideoViewModel
            {
                Title = videoCreateModel.Title,
                ImageUrl = videoCreateModel.ImageUrl,
                Url = videoCreateModel.Url,
                UploadDate = DateTime.UtcNow.ToString("dd/MM/yyyy")                
            };

            var user = await this.userManager.GetUserAsync(this.User);
            string userId = user.Id;
            await this.videoService.CreateAsync(videoCreateModel, userId);

            return View("Created", videoViewModel);
        }

        [Route("Edit/{id}")]
        [HttpGet]
        public async Task<IActionResult> Edit(string? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            var videoEditViewModel = await this.videoService.GetVideoEditViewModelByIdAsync(id);


            if (videoEditViewModel == null)
            {
                return NotFound();
            }
  

            return View(videoEditViewModel);
        }

        [Route("Edit/{id}")]
        [HttpPost]
        public async Task<IActionResult> Edit(VideoEditModel videoEditModel, string? id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            var videoEditViewModel = await this.videoService.GetVideoEditViewModelByIdAsync(id);

            try
            {
                var user = await this.userManager.GetUserAsync(this.User);
                string userId = user.Id;
                await this.videoService.EditAsync(videoEditModel, id, userId);

                return RedirectToAction("Dashboard");
            }
            catch (RetryLimitExceededException /* dex */)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
            }

            return View(videoEditViewModel);
        }

        [Route("Delete/{id}")]
        [HttpGet]
        public async Task<IActionResult> Delete(string? id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            var videoDeleteViewModel = await this.videoService.GetVideoDeleteViewModelByIdAsync(id);

            return View(videoDeleteViewModel);

        }

        [Route("Delete/{id}")]
        [HttpPost]
        public async Task<IActionResult> DeleteVideo(string? id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            var isVideoExists = await this.videoService.AnyByIdAsync(id);

            if (!isVideoExists)
            {
                return NotFound();
            }

            try
            {
                var user = await this.userManager.GetUserAsync(this.User);
                string userId = user.Id;
                await this.videoService.DeleteAsync(id, userId);

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
