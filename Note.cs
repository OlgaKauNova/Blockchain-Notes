using System;
using System.Text;
using System.Security.Cryptography;

namespace blockchain
{
	///<Summary>Note based on blockchain technology</Summary>
	class Note
	{
		public static string Splitter = "_splitter_";
		private string text;
		private byte[] hash;

		public Note(string text)
		{
			this.text = text;
			this.hash = ComputeHash(text);
		}

		private byte[] ComputeHash(string text)
		{
			SHA256 sha = SHA256.Create(); //Creating SHA256 object
			byte[] textBytes = Encoding.Default.GetBytes(text); //Converting text to bytes

			return sha.ComputeHash(textBytes); //Computing bytes to hash
		}

		///<Summary>Full text of the note</Summary>
		public string Text { get { return this.text; } }

		///<Summary>Clear text of the note withous hash of the previous block</Summary>
		public string ClearText
		{
			get
			{
				return this.text.Remove(0, this.text.IndexOf(Note.Splitter) + Note.Splitter.Length);
			}
		}

		///<Summary>Hash of the previous block</Summary>
		public string PreviousHash
		{
			get
			{
				return this.text.Remove(this.text.IndexOf(Note.Splitter));
			}
		}

		///<Summary>Hash of this block</Summary>
		public string HashString
		{
			get
			{
				//We need to compute hash every time we checking it
				//So we will be sure it is valid
				var hash = ComputeHash(this.text); 
				return Encoding.Default.GetString(hash);
			}
		}

		///<Summary>Hash of this block as bytes</Summary>
		public byte[] Hash { get { return this.hash; } }

	}
}