﻿using System;
using System.Collections.Generic;

namespace TanCruzDentalInventorySystem.Models
{
	public class BusinessPartner
	{
		public string BusinessPartnerId { get; set; }
		public string BusinessPartnerName { get; set; }
		public string BusinessPartnerType { get; set; }
		public string BusinessPartnerAlias { get; set; }
		public string BusinessPartnerFirstName { get; set; }
		public string BusinessPartnerMiddleName { get; set; }
		public string BusinessPartnerLastName { get; set; }
		public IEnumerable<BusinessPartnerDetail> BusinessPartnerDetails { get; set; }
		public string UserId { get; set; }
		public DateTime? ChangedDate { get; set; }
		public long VersionTimeStamp { get; set; }
	}
}