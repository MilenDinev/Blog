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

        [HttpGet("details/{id}")]
        public async Task<IActionResult> Index(string id)
        {
            var toolViewModel = await _toolService.GetToolViewModelByIdAsync(id);

            if (CurrentUserId is not null)
                toolViewModel.IsFavorite = await _userService.IsFavoriteToolAsync(CurrentUserId, id);
                 
            return View(toolViewModel);
        }

        [HttpGet("latest")]
        public async Task<IActionResult> Latest(string? search)
        {
            var toolsPtoolModelBundle = await _toolService.GetTodaysToolPreviewModelBundleAsync();

            // this operation should be performed in service layer
            if (!string.IsNullOrEmpty(search))
                toolsPtoolModelBundle = toolsPtoolModelBundle
                    .Where(tool => tool.Title.Contains(search))
                    .ToList();

            return View(toolsPtoolModelBundle);
        }


        [Authorize(Roles = "admin")]
        [HttpGet("create")]
        [HttpPost("create")]
        public async Task<IActionResult> Create(ToolCreateModel toolCreateModel)
        {
            if (CurrentUserId is null)
                throw new UnauthorizedAccessException();

            if (ModelState.IsValid)
            {
                await _toolService.CreateAsync(toolCreateModel, CurrentUserId);

                return RedirectToAction("Index", "Home");
            }

            return View(toolCreateModel);
        }

        [Authorize(Roles = "admin")]
        [HttpGet("edit/{id}")]
        public async Task<IActionResult> Edit(string? id)
        {
            if (id == null)
                return BadRequest();

            var toolEditViewModel = await _toolService.GetToolEditViewModelByIdAsync(id);

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

                return RedirectToAction("Index", "Home");
            }
            catch (UnauthorizedAccessException /* dex */)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
            }
            var toolEditViewModel = await _toolService.GetToolEditViewModelByIdAsync(id);

            return View(toolEditViewModel);
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

               return RedirectToAction("Index", "Home");
            }
            catch (UnauthorizedAccessException /* dex */)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
            }

            return RedirectToAction("Index", "Home");
        }
    }
}