using Domain.ValueObjects;
using System.Threading;

namespace WebApi.Extensions;

public static class FormFileExtensions
{
	public static async Task<Photo> ToPhotoAsync(this IFormFile file, CancellationToken cancellationToken)
	{
		byte[] fileBytes;
		using (var memoryStream = new MemoryStream())
		{
			await file.CopyToAsync(memoryStream, cancellationToken);
			fileBytes = memoryStream.ToArray();
		}

		Photo photo = new Photo(file.FileName, file.ContentType, fileBytes);

		return photo;
	}
}
