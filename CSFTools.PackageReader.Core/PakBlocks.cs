using System.IO;
using System.IO.Extensions;

namespace CSFTools.PackageReader
{
	internal enum PakCompression
	{
		None = 0x41,
		Deflate = 0x43,
	}

	internal enum PakGameVersion
	{
		Demo = 4,
		Release = 5,
	}

	internal struct PakEntry
	{
		public string Name;
		public int CompressedDataStart;
		public int CompressedSize;
		public int Unknown1;
		public int Unknown2;

		public static bool TryReadBlock(BinaryReader reader, out PakEntry entry)
		{
			entry = default;

			entry.Name = reader.ReadNullTerminatedString();
			entry.CompressedDataStart = reader.ReadInt32();
			entry.CompressedSize = reader.ReadInt32();
			entry.Unknown1 = reader.ReadInt32();
			entry.Unknown2 = reader.ReadInt32();

			return true;
		}
	}

	internal struct PakHeader
	{
		public const int Signature = 0x4B4150;

		public PakCompression Compression;
		public PakGameVersion Version;
		public int Unknown; // seems always 1
		public int EntryCount;

		public static bool TryReadBlock(BinaryReader reader, out PakHeader header)
		{
			header = default;

			if (reader.ReadInt24() != Signature)
				return false;

			header.Compression = (PakCompression)reader.ReadByte();
			header.Version = (PakGameVersion)reader.ReadInt32();
			header.Unknown = reader.ReadInt32();
			header.EntryCount = reader.ReadInt32();

			return true;
		}
	}
}