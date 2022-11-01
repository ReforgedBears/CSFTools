using System.Security.Cryptography;

namespace System.IO.Extensions.Tests
{
	public class BinaryReaderExtensionsTests
	{
		[Theory]
		[InlineData(0x4B4150, 0x50, 0x41, 0x4B)]
		[InlineData(0x000000, 0x00, 0x00, 0x00)]
		[InlineData(-1, 0xFF, 0xFF, 0xFF)]
		public void ReadInt24(int expected, byte low, byte mid, byte high)
		{
			byte[] buffer = new byte[] { low, mid, high, };
			using var binaryReader = new BinaryReader(new MemoryStream(buffer));

			int actual = binaryReader.ReadInt24();

			Assert.Equal(expected, actual);
		}

		[Theory]
		[InlineData(0x4B4150, 0x50, 0x41, 0x4B)]
		[InlineData(0x000000, 0x00, 0x00, 0x00)]
		[InlineData(0xFFFFFF, 0xFF, 0xFF, 0xFF)]
		public void ReadUInt24(int expected, byte low, byte mid, byte high)
		{
			byte[] buffer = new byte[] { low, mid, high, };
			using var binaryReader = new BinaryReader(new MemoryStream(buffer));

			int actual = binaryReader.ReadUInt24();

			Assert.Equal(expected, actual);
		}

		[Theory]
		[InlineData("PAK", 0x50, 0x41, 0x4B, 0x00)]
		[InlineData("PAKA", 0x50, 0x41, 0x4B, 0x41)]
		public void ReadNullTerminatedString(string expected, byte low, byte lowmid, byte highmid, byte high)
		{
			byte[] buffer = new byte[] { low, lowmid, highmid, high, };
			using var binaryReader = new BinaryReader(new MemoryStream(buffer));

			string actual = binaryReader.ReadNullTerminatedString();

			Assert.Equal(expected, actual);
		}
	}
}