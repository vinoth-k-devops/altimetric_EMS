using System;
namespace EMS_Domain.Model
{
	public class Response
	{
		public bool IsSuccess { get; set; } = false;
		public string Message { get; set; } = string.Empty;
		public dynamic? Data { get; set; }
	}
}

