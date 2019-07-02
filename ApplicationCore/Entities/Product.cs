using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Entities
{
	internal class Product:BaseEntity<int>
	{
		public string Name { get; set; }
		public string ImageUri { get; set; }
		public decimal Price { get; set; }
		public string Description { get; set; }
	}
}
