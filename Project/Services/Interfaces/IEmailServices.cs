﻿namespace BTLWEB.Services.Interfaces
{
	public interface IEmailServices
	{
		public Task SendEmailAsync(string email, string subject, string body);
	}
}
