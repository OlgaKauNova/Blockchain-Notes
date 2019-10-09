using System;
using System.Collections.Generic;

namespace blockchain
{
	enum Command
	{
		Show,
		ShowFull,
		Add,
		Validate, 
		Exit
	}

	class Program
	{
		static void Main(string[] args)
		{
			List<Note> notes = new List<Note>();

			notes.Add(new Note("start  "));


			string command = "";
			
			Command com = Command.Show;

			while(com != Command.Exit)
			{
				Console.WriteLine("Actions: ");
				Console.WriteLine("0 - Show notes \n1 - Show full notes \n2 - Add note \n3 - Validate notes \n4 - Exit \n");
				command = Console.ReadLine();

				try
				{
					com = (Command)Convert.ToInt32(command);
				}
				catch(Exception e)
				{
					com = Command.Show;
					Console.WriteLine(e.Message);
				}

				switch(com)
				{
					case Command.Show:
						WriteAllNotes(notes);
						break;

					case Command.ShowFull:
						WriteFullNotes(notes);
						break;

					case Command.Add:
						Console.WriteLine("Enter new note text: ");
						string text = Console.ReadLine();

						AddNote(text, notes);
						break;

					case Command.Validate:
						ValidateNotes(notes);
						break;

					case Command.Exit:
						Console.WriteLine("Good Bye!");
						break;
				}
			}

			
		}

		static void ValidateNotes(List<Note> notes)
		{
			for(int i = 0; i < notes.Count; i++)
			{
				if(i == 0)
				{
					Console.WriteLine("0 - Valid");
				}
				else
				{
					if(notes[i].PreviousHash == notes[i - 1].HashString)
					{
						Console.WriteLine(i + " - Valid");
					}
					else
					{
						Console.WriteLine((i - 1) + " - Changed");
						break;
					}
				}
			}
			Console.WriteLine("");
		}

		static void AddNote(string text, List<Note> notes)
		{
			notes.Add(new Note(notes[notes.Count - 1].HashString + " " + text));
		}

		static void WriteAllNotes(List<Note> notes)
		{
			Console.WriteLine("Your notes: ");
			for(int i = 0; i < notes.Count; i++)
			{
				WriteNote(notes[i]);
			}

			Console.WriteLine("");
		}

		static void WriteFullNotes(List<Note> notes)
		{
			Console.WriteLine("Your notes: ");
			for(int i = 0; i < notes.Count; i++)
			{
				WriteNoteFull(notes[i]);
			}

			Console.WriteLine("");
		}

		static void WriteNote(Note n)
		{
			Console.WriteLine(n.ClearText);
		}

		static void WriteNoteFull(Note n)
		{
			Console.WriteLine($"Text: {n.Text} | ClearText: {n.ClearText} | HashString: {n.HashString}");
		}
	}
}
