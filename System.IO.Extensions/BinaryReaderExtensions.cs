using System.Text;

namespace System.IO.Extensions
{
	public static class BinaryReaderExtensions
	{
		private const int Int24ByteCount = 3;

		/// <summary>
		/// Reads a 3-byte signed integer from the current stream and advances the current
		/// position of the stream by three bytes.
		/// </summary>
		/// <param name="reader">The <see cref="BinaryReader"/> to read.</param>
		/// <returns>A 3-byte signed integer from the current stream.</returns>
		/// <exception cref="EndOfStreamException">The end of the stream is reached.</exception>
		/// <exception cref="ObjectDisposedException">The stream is closed.</exception>
		/// <exception cref="IOException">An I/O error occurred.</exception>
		public static int ReadInt24(this BinaryReader reader)
		{
			Span<byte> bytes = stackalloc byte[Int24ByteCount];
			reader.Read(bytes);
			return bytes[0] | bytes[1] << 8 | (sbyte)bytes[2] << 16;
		}

		/// <summary>
		/// Reads a 3-byte unsigned integer from the current stream and advances the current
		/// position of the stream by three bytes.
		/// </summary>
		/// <param name="reader">The <see cref="BinaryReader"/> to read.</param>
		/// <returns>A 3-byte unsigned integer from the current stream.</returns>
		/// <exception cref="EndOfStreamException">The end of the stream is reached.</exception>
		/// <exception cref="ObjectDisposedException">The stream is closed.</exception>
		/// <exception cref="IOException">An I/O error occurred.</exception>
		public static int ReadUInt24(this BinaryReader reader)
		{
			Span<byte> bytes = stackalloc byte[Int24ByteCount];
			reader.Read(bytes);
			return bytes[0] | bytes[1] << 8 | bytes[2] << 16;
		}

		/// <summary>
		/// Reads a string from the current stream. The string is ended with a null character
		/// (a character with a value of zero).
		/// </summary>
		/// <param name="reader">The <see cref="BinaryReader"/> to read.</param>
		/// <returns>The string being read.</returns>
		/// <exception cref="EndOfStreamException">The end of the stream is reached.</exception>
		/// <exception cref="ObjectDisposedException">The stream is closed.</exception>
		/// <exception cref="IOException">An I/O error occurred.</exception>
		public static string ReadNullTerminatedString(this BinaryReader reader)
		{
			var stringBuilder = new StringBuilder();

			int character;
			while ((character = reader.Read()) != char.MinValue && character != -1)
			{
				stringBuilder.Append((char)character);
			}

			return stringBuilder.ToString();
		}
	}
}