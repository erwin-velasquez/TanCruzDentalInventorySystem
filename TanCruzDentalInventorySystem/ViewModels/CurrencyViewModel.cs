using System.ComponentModel.DataAnnotations;

namespace TanCruzDentalInventorySystem.ViewModels
{
	public class CurrencyViewModel
	{
        [Display(Name = "Currency")]
        public string CurrencyId { get; set; }
		public string CurrencyName { get; set; }
		public string CurrencyDescription { get; set; }
	}

	public class CurrencyRateViewModel
	{

	}
}