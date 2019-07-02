using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Exceptions
{
	public class ProductNotFoundException : Exception
	{
		public ProductNotFoundException(int productId) : base($"product with id {productId} not found")
		{
		}

		public ProductNotFoundException(string message) : base(message)
		{
		}

		public ProductNotFoundException(string message, Exception innerException) : base(message, innerException)
		{
		}
	}
}
