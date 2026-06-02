using MelonLoader;
using System;

namespace CMS21Together.UI;

/// <summary>
/// Helper class for loading and managing game data and assets
/// </summary>
public static class DataHelper
{
	/// <summary>
	/// Loads a custom texture from embedded resources
	/// </summary>
	public static UnityEngine.Texture2D LoadCustomTexture(string resourcePath)
	{
		try
		{
			var stream = LoadContent(resourcePath);
			if (stream == null)
				throw new System.IO.FileNotFoundException($"Resource not found: {resourcePath}");

			var buffer = new byte[stream.Length];
			stream.Read(buffer, 0, (int)stream.Length);

			var texture = new UnityEngine.Texture2D(1, 1);
			UnityEngine.ImageConversion.LoadImage(texture, buffer);
			return texture;
		}
		catch (Exception ex)
		{
			MelonLogger.Error($"Failed to load custom texture '{resourcePath}': {ex.Message}");
			throw;
		}
	}

	/// <summary>
	/// Loads content from embedded resources
	/// </summary>
	public static System.IO.Stream LoadContent(string resourcePath)
	{
		try
		{
			var assembly = System.Reflection.Assembly.GetExecutingAssembly();
			return assembly.GetManifestResourceStream(resourcePath);
		}
		catch (Exception ex)
		{
			MelonLogger.Error($"Failed to load resource '{resourcePath}': {ex.Message}");
			return null;
		}
	}
}
