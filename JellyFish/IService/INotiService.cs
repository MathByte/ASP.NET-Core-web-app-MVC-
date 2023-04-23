using JellyFish.Models;

namespace JellyFish.IService
{
	public interface INotiService
	{
		List<Noti> GetNotifications(string nToUserId, bool bIsGetOnlyUnread);
	}
}
