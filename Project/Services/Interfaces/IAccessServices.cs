using BTLWEB.Models;
using Microsoft.AspNetCore.Mvc;

namespace BTLWEB.Services.Interfaces
{
	public interface IAccessServices
	{
		public string Login(string? usernameLogin, string? passwordLogin);
		public string Register(TaiKhoanUser u);
		public Task ForgotPassword(string username);

		public int ResetPassword(string username, string password);
	}
}
