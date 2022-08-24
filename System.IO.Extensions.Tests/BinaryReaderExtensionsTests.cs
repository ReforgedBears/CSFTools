namespace System.IO.Extensions.Tests
{
	public class BinaryReaderExtensionsTests
	{
		[Fact]
		public void ReadInt24()
		{
			ReadOnlySpan<byte> bytes = stackalloc byte[] { 0x50, 0x41, 0x4B };
			using var resourceReader = new BinaryReader(input: new MemoryStream(bytes.ToArray()));

			int int24 = resourceReader.ReadInt24();

			Assert.Equal(0x4B4150, int24);
		}

		[Fact]
		public void ReadInt24_FFFFFF_ReturnsMinusOne()
		{
			ReadOnlySpan<byte> bytes = stackalloc byte[] { 0xFF, 0xFF, 0xFF };
			using var resourceReader = new BinaryReader(input: new MemoryStream(bytes.ToArray()));

			int int24 = resourceReader.ReadInt24();

			Assert.Equal(-1, int24);
		}

		[Fact]
		public void ReadInt24_000000_ReturnsZero()
		{
			ReadOnlySpan<byte> bytes = stackalloc byte[] { 0x00, 0x00, 0x00 };
			using var resourceReader = new BinaryReader(input: new MemoryStream(bytes.ToArray()));

			int int24 = resourceReader.ReadInt24();

			Assert.Equal(0x000000, int24);
		}

		[Fact]
		public void ReadUInt24()
		{
			ReadOnlySpan<byte> bytes = stackalloc byte[] { 0x50, 0x41, 0x4B };
			using var resourceReader = new BinaryReader(input: new MemoryStream(bytes.ToArray()));

			int int24 = resourceReader.ReadUInt24();

			Assert.Equal(0x4B4150, int24);
		}

		[Fact]
		public void ReadUInt24_FFFFFF_ReturnsMaxValue()
		{
			ReadOnlySpan<byte> bytes = stackalloc byte[] { 0xFF, 0xFF, 0xFF };
			using var resourceReader = new BinaryReader(input: new MemoryStream(bytes.ToArray()));

			int int24 = resourceReader.ReadUInt24();

			Assert.Equal(0xFFFFFF, int24);
		}

		[Fact]
		public void ReadUInt24_000000_ReturnsZero()
		{
			ReadOnlySpan<byte> bytes = stackalloc byte[] { 0x00, 0x00, 0x00 };
			using var resourceReader = new BinaryReader(input: new MemoryStream(bytes.ToArray()));

			int int24 = resourceReader.ReadUInt24();

			Assert.Equal(0x000000, int24);
		}

		[Fact]
		public void ReadNullTerminatedString()
		{
			ReadOnlySpan<byte> bytes = stackalloc byte[] { 0x50, 0x41, 0x4B, 0x00 };
			using var resourceReader = new BinaryReader(input: new MemoryStream(bytes.ToArray()));

			string str = resourceReader.ReadNullTerminatedString();

			Assert.Equal("PAK", str);
		}

		[Fact]
		public void ReadNullTerminatedString_EndOfStream_ReadToEnd()
		{
			ReadOnlySpan<byte> bytes = stackalloc byte[] { 0x50, 0x41, 0x4B };
			using var resourceReader = new BinaryReader(input: new MemoryStream(bytes.ToArray()));

			string str = resourceReader.ReadNullTerminatedString();

			Assert.Equal("PAK", str);
		}
	}
}