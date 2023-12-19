using AutoMapper.Internal.Mappers;

namespace DbmsApp;

public static class Extensions
{
	public static string ToValues(this DateTime time)
	{
		return $"{time.Year}, {time.Month}, {time.Day}, {time.Hour}, {time.Minute}, 0 ,0";
	}
}