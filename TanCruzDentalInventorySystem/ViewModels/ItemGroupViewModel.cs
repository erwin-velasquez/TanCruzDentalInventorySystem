using System;

namespace TanCruzDentalInventorySystem.ViewModels
{
	public class ItemGroupViewModel
	{
		public string ItemGroupId { get; set; }
		public string ItemGroupName { get; set; }
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