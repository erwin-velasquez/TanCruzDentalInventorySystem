using System;
using System.ComponentModel.DataAnnotations;

namespace TanCruzDentalInventorySystem.ViewModels
{
	public class ItemGroupViewModel
	{
		public string ItemGroupId { get; set; }
        [Display(Name = "Item Group Name")]
        public string ItemGroupName { get; set; }
        [Display(Name = "Item Group Description")]
        public string ItemGroupDescription { get; set; }
		public string UserId { get; set; }
		public DateTime? ChangedDate { get; set; }
		public long VersionTimeStamp { get; set; }
	}

	public class ItemGroupFormViewModel
	{
		public ItemGroupViewModel ItemGroup { get; set; }

	}
}