using System;
namespace EMS_Common.Variables
{
	public class APIResponse
	{
        public bool IsSuccess { get; set; } = false;
        public string Message { get; set; } = string.Empty;
        public dynamic? Data { get; set; }
    }
}

