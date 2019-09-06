using Microsoft.AspNet.Identity;
using System.Threading.Tasks;
using System.Web.Mvc;
using TanCruzDentalInventorySystem.BusinessService.BusinessServiceInterface;
using TanCruzDentalInventorySystem.ViewModels;

namespace TanCruzDentalInventorySystem.Controllers
{
	[Authorize]
	public class BusinessPartnerController : Controller
	{
		private IBusinessPartnerService _businessPartnerService;

		public BusinessPartnerController(IBusinessPartnerService businessPartnerService)
		{
			_businessPartnerService = businessPartnerService;
		}

		[HttpGet]
		public async Task<ActionResult> Index()
		{
			var businessPartners = await _businessPartnerService.GetBusinessPartnerList();

			return View(businessPartners);
		}

		[HttpGet]
		public async Task<ActionResult> BusinessPartnerRecord(string businessPartnerId)
		{
			var businessPartner = await _businessPartnerService.GetBusinessPartner(businessPartnerId);

			return View(businessPartner);
		}

		[Authorize(Roles = "Editor")]
		public async Task<ActionResult> CreateBusinessPartner()
		{
			var businessPartnerId = await _businessPartnerService.CreateBusinessPartner(User.Identity.GetUserId());
			var businessPartnerForm = await _businessPartnerService.GetBusinessPartnerForm(businessPartnerId);

			return View(businessPartnerForm);
		}

		[HttpPost]
		[Authorize(Roles = "Editor")]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> SaveBusinessPartnerRecord(BusinessPartnerFormViewModel businessPartnerForm)
		{
			if (ModelState.IsValid)
			{
				businessPartnerForm.BusinessPartner.UserId = User.Identity.GetUserId();
				var recordsSaved = await _businessPartnerService.SaveBusinessPartner(businessPartnerForm.BusinessPartner);

				if (recordsSaved >= 1)
				{
					var businessPartner = await _businessPartnerService.GetBusinessPartner(businessPartnerForm.BusinessPartner.BusinessPartnerId);
					return View("BusinessPartnerRecord", businessPartner);
				}
				ModelState.AddModelError(string.Empty, "There was a problem and the BusinessPartner was not saved.");
			}

			businessPartnerForm = await _businessPartnerService.GetBusinessPartnerForm(businessPartnerForm.BusinessPartner.BusinessPartnerId);
			return View("EditBusinessPartnerRecord", businessPartnerForm);
		}

		[HttpGet]
		[Authorize(Roles = "Editor")]
		public async Task<ActionResult> EditBusinessPartnerRecord(string businessPartnerId)
		{
			var businessPartnerForm = await _businessPartnerService.GetBusinessPartnerForm(businessPartnerId);

			return View(businessPartnerForm);
		}
	}
}