using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ApplicationCore.Entities
{
	public class Product:BaseEntity<int>
	{
		[Required]
		public string Name { get; set; }

		[Required]
		public string ImageUri { get; set; }

		[Required]
		[Column(TypeName = "decimal(5,2)")]
		public decimal Price { get; set; }
		public string Description { get; set; }
	}
}
