using Dapper;
using JellyFish.Common;
using JellyFish.IService;
using JellyFish.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace JellyFish.Service
{
	public class NotiService : INotiService
	{
		List<Noti> _oNotifications = new List<Noti>();

		public List<Noti> GetNotifications(string nToUserId, bool bIsGetOnlyUnread)
		{
			_oNotifications = new List<Noti>();
			using (IDbConnection con = new SqlConnection("Server=(localdb)\\mssqllocaldb;Database=JellyFishDB;Trusted_Connection=True;MultipleActiveResultSets=True;TrustServerCertificate=True;"))
			{
				if (con.State == ConnectionState.Closed) con.Open();
				var oNotis = con.Query<Noti>("SELECT * FROM View_Notification WHERE ToUserId=" + "'" + nToUserId + "'").ToList();

				if(oNotis != null && oNotis.Count() > 0)
				{
					_oNotifications= oNotis;
				}
				return _oNotifications;
			}
		}
	}
}
