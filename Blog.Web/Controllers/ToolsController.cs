namespace Blog.Web.Controllers
{
    using System.Security.Claims;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;
    using Data.Models.RequestModels.Tool;
    using Services.Interfaces;

    [Route("tools")]
    public class ToolsController : Controller
    {
        private readonly IUserService _userService;
        private readonly IToolService _toolService;

        public ToolsController(IToolService toolService,
            IUserService userService)
        {
            _toolService = toolService;
            _userService = userService;
        }

        private string? CurrentUserId => User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        [HttpGet]
        public async Task<IActionResult> Dashboard(string? search)
        {

            if (string.IsNullOrEmpty(search))
            {
                var tools = await _toolService.GetToolPreviewModelBundleAsync();

                return View(tools);
            }

            else
            {
                var tools = await _toolService.FindToolsPreviewModelBundleAsync(search);
                return View(tools);
            }
        }

        [HttpGet("details/{id}")]
        public async Task<IActionResult> Tool(string id)
        {
            var toolViewModel = await _toolService.GetToolViewModelByIdAsync(id);

            if (CurrentUserId is not null)
                toolViewModel.IsFavorite = await _userService.IsFavoriteToolAsync(CurrentUserId, id);
                 
            return View(toolViewModel);
        }

        [HttpGet("latest")]
        public async Task<IActionResult> Latest(string? search)
        {
            if (string.IsNullOrEmpty(search))
            {
                var toolsPreviewModelBundle = await _toolService.GetTodaysToolPreviewModelBundleAsync();

                return View(toolsPreviewModelBundle);
            }

            else
            {
                var toolsPreviewModelBundle = await _toolService.FindTodaysToolsPreviewModelBundleAsync(search);
                return View(toolsPreviewModelBundle);
            }
        }

        [Authorize(Roles = "admin")]
        [HttpGet("create")]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [Authorize(Roles = "admin")]
        [HttpPost("create")]
        public async Task<IActionResult> Create(ToolCreateModel toolCreateModel)
        {
            if (CurrentUserId is null)
                throw new UnauthorizedAccessException();

            if (ModelState.IsValid)
            {
                await _toolService.CreateAsync(toolCreateModel, CurrentUserId);

                return RedirectToAction(nameof(Dashboard));
            }

            return View(toolCreateModel);
        }

        [Authorize(Roles = "admin")]
        [HttpGet("edit/{id}")]
        public async Task<IActionResult> Edit(string? id)
        {
            if (id == null)
                return BadRequest();

            var toolEditViewModel = await _toolService.GetToolEditModelByIdAsync(id);

            if (toolEditViewModel == null)
                return NotFound();

            return View(toolEditViewModel);
        }

        [Authorize(Roles = "admin")]
        [HttpPost("edit/{id}")]
        public async Task<IActionResult> Edit(ToolEditModel toolEditModel, string? id)
        {
            if (id == null)
                return BadRequest();

            if (CurrentUserId is null)
                throw new UnauthorizedAccessException();

            try
            {

                await _toolService.EditAsync(toolEditModel, id, CurrentUserId);

                return RedirectToAction(nameof(Dashboard));
            }
            catch (UnauthorizedAccessException /* dex */)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
            }
   
            return View(toolEditModel);
        }

        [Authorize(Roles = "admin")]
        [HttpGet("delete/{id}")]
        public async Task<IActionResult> Delete(string? id)
        {
            if (id == null)
                return BadRequest();

            var toolDeleteViewModel = await _toolService.GetToolDeleteViewModelByIdAsync(id);

            return View(toolDeleteViewModel);
        }

        [Authorize(Roles = "admin")]
        [HttpPost("delete/{id}")]
        public async Task<IActionResult> DeleteTool(string? id)
        {
            if (id == null)
                return BadRequest();

            if (CurrentUserId is null)
                throw new UnauthorizedAccessException();

            try
            {
                await _toolService.DeleteAsync(id, CurrentUserId);

               return RedirectToAction(nameof(Dashboard));
            }
            catch (UnauthorizedAccessException /* dex */)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
            }

            return RedirectToAction(nameof(Dashboard));
        }
    }
}