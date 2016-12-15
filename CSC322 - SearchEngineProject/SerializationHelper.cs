using System.Diagnostics.Contracts;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace CSC322_SearchEngineProject
{
	internal static class SerializationHelper
	{
		internal static T Deserialize<T>(string filePath)
		{
			Contract.Requires(filePath != null);
			Stream stream = new FileStream(filePath,
				FileMode.Open, FileAccess.Read, FileShare.Read);
			BinaryFormatter formatter = new BinaryFormatter();
			T output = (T)formatter.Deserialize(stream);
			stream.Close();
			return output;
		}
		internal static void Serialize<T>(string filePathToSerializeTo, T toSerialize)
		{
			Contract.Requires(toSerialize != null);
			Contract.Requires(filePathToSerializeTo != null);
			Stream stream = new FileStream(filePathToSerializeTo,
				FileMode.Create, FileAccess.Write, FileShare.None);
			BinaryFormatter formatter = new BinaryFormatter();
			formatter.Serialize(stream, toSerialize);
			stream.Close();
		}
	}
}
