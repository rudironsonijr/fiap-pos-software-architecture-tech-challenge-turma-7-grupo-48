namespace Helpers;

public static class MathHelper
{
	public static int CalculatePaginateSkip(int? page, int? limit)
	{
		if (page == 0 || page == null)
		{
			page = 1;
		}

		if (limit == 0 || limit == null)
		{
			limit = int.MaxValue;
		}

		var skip = (page.Value  - 1) * limit.Value;

		return skip;
	}
}
