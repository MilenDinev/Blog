namespace Blog.Web.Controllers
{
    using Services.Interfaces;
    using System.Security.Claims;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;
    using Data.Models.RequestModels.PricingStrategy;

    [Route("PricingStrategies")]
    public class PricingStrategiesController : Controller
    {
        private readonly IPricingStrategyService _pricingStrategyService;

        public PricingStrategiesController(IPricingStrategyService pricingStrategyService)
        {
            _pricingStrategyService = pricingStrategyService;
        }


        [Authorize(Roles = "admin")]
        [HttpGet]
        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> Create(PricingStrategyCreateModel pricingStrategyCreateModel)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value
                ?? throw new UnauthorizedAccessException();

            if (ModelState.IsValid)
            {
                await _pricingStrategyService.CreateAsync(pricingStrategyCreateModel, userId);

                return RedirectToAction("Index", "Home");
            }

            return View(pricingStrategyCreateModel);
        }
    }
}
