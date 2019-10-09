using System;
using System.Text;
using System.Security.Cryptography;

namespace blockchain
{
	class Note
	{
		private string text;

		private byte[] hash;

		public Note(string text)
		{
			this.text = text;

			SHA256 sha = SHA256.Create();

			byte[] textBytes = Encoding.Default.GetBytes(text);

			this.hash = sha.ComputeHash(textBytes);
		}

		public string Text { get { return this.text; } }

		public string ClearText
		{
			get
			{
				return this.text.Remove(0, this.text.IndexOf(' ') + 1);
			}
		}

		public string PreviousHash
		{
			get
			{
				return this.text.Remove(this.text.IndexOf(' '));
			}
		}

		public string HashString
		{
			get
			{
				return Encoding.Default.GetString(this.hash);
			}
		}

		public byte[] Hash { get { return this.hash; } }

	}
}