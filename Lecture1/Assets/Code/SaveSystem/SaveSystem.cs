using UnityEngine;
using System.Collections;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace GameProgramming3D.SaveLoad
{
	/// <summary>
	/// The class which handles saving and loading the game data.
	/// </summary>
	public static class SaveSystem
	{
		/// <summary>
		/// Name (and extension) of the save file.
		/// </summary>
		private const string SaveFileName = "save.dat";

		/// <summary>
		/// The full path of the save file.
		/// </summary>
		public static string SaveFilePath
		{
			get { return Path.Combine( Application.persistentDataPath, SaveFileName ); }
		}

		/// <summary>
		/// Serializes the object and saves it to disk.
		/// </summary>
		/// <param name="objectToSave">The object to be saved.</param>
		public static void Save( object objectToSave )
		{
			// Binary formatter serializes data into binary which can be stored to disk.
			BinaryFormatter bf = new BinaryFormatter();
			
			// Binary formatter stores serialization result into stream so let's create a
			// memory stream for that purpose.
			MemoryStream ms = new MemoryStream();
			
			// BinaryFormatter.Serilaize method actually serializes the object. Result is
			// stored to ms Stream.
			bf.Serialize( ms, objectToSave );
			
			// File.WriteAllBytes writes serialized bytes into a file. Bytes can be
			// acquired from stream by calling its GetBuffer method.
			File.WriteAllBytes( SaveFilePath, ms.GetBuffer() );
		}

		/// <summary>
		/// Loads saved data from SaveFilePath.
		/// </summary>
		/// <typeparam name="T">The type of the save file. Must be a class</typeparam>
		/// <returns>The deserialized object which contains saved data.</returns>
		public static T Load< T >() where T : class
		{
			// We can load file only if it exists.
			if ( File.Exists( SaveFilePath ) )
			{
				// File.ReadAllBytes reads bytes from a file and returns them as a byte array.
				byte[] data = File.ReadAllBytes( SaveFilePath );
				// Since we used BinaryFormatter to serialize object, we must use it also to
				// deserialize it.
				BinaryFormatter bf = new BinaryFormatter();
				// Lets create a MemoryStream which contains our serialized bytes
				MemoryStream ms = new MemoryStream( data );
				// and deserialize saved data from that stream.
				object saveData = bf.Deserialize( ms );

				// Return saved data.
				return (T) saveData;
			}

			// If file doesn't exist, just return default value of type T.
			return default (T);
		}

		/// <summary>
		/// Checks if save file exists
		/// </summary>
		/// <returns><c>True</c> if save file exists, <c>false</c> otherwise</returns>
		public static bool DoesSaveExist()
		{
			return File.Exists( SaveFilePath );
		}
	}
}
