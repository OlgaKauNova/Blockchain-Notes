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
			List<Note> notes = new List<Note>(); //Creating notes list

			notes.Add(new Note("start First note")); //Adding first note


			string command = "";
			
			Command com = Command.Show;

			while(com != Command.Exit)
			{
				Console.Clear();

				//Asking what the user wants to do
				Console.ForegroundColor = ConsoleColor.Green;
				Console.WriteLine("Actions: ");

				Console.ForegroundColor = ConsoleColor.White;
				Console.WriteLine("0 - Show notes \n1 - Show full notes \n2 - Add note \n3 - Validate notes \n4 - Exit \n");

				Console.ForegroundColor = ConsoleColor.Green;
				Console.WriteLine("Your choise: ");

				Console.ForegroundColor = ConsoleColor.White;
				command = Console.ReadLine();
				Console.WriteLine("\n");

				try
				{
					//If user enters some of the actions number than we convert it to Command type
					com = (Command)Convert.ToInt32(command); 
				}
				catch(Exception e)
				{
					//If the input wasn't correct we just show all notes
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
						Console.ForegroundColor = ConsoleColor.Green;
						Console.WriteLine("Enter new note text: ");

						Console.ForegroundColor = ConsoleColor.White;
						string text = Console.ReadLine();

						AddNote(text, notes);
						break;

					case Command.Validate:
						ValidateNotes(notes);
						break;

					case Command.Exit:
						Console.ForegroundColor = ConsoleColor.Blue;
						Console.WriteLine("Good Bye!");
						Console.ForegroundColor = ConsoleColor.White;
						break;
				}

				Console.ReadKey();
			}
		}

		///<summary>Checking if any of the notes was changed</summary>
		static void ValidateNotes(List<Note> notes)
		{
			for(int i = 0; i < notes.Count; i++)
			{
				if(i == 0) //Do not check first note
				{
					Console.ForegroundColor = ConsoleColor.Green;
					Console.WriteLine("0 - Valid");
				}
				else
				{
					Console.ForegroundColor = ConsoleColor.White;
					
					//If current note's hash part of the text equal hash of the previous note
					//then it's okay
					if(@notes[i].PreviousHash == @notes[i - 1].HashString) 
					{
						Console.ForegroundColor = ConsoleColor.Green;
						Console.WriteLine(i + " - Valid");
					}
					else //Otherwise we say that something was changed
					{
						Console.WriteLine($"\n\n | {notes[i].PreviousHash} | {notes[i - 1].HashString} | \n\n");
						Console.ForegroundColor = ConsoleColor.Red;
						Console.WriteLine(i + " - Changed");
					}
				}
			}

			Console.WriteLine("\n");
		}

		static void AddNote(string text, List<Note> notes)
		{
			notes.Add(new Note(notes[notes.Count - 1].HashString + Note.Splitter + text));
		}

		static void WriteAllNotes(List<Note> notes)
		{
			Console.ForegroundColor = ConsoleColor.Green;
			Console.WriteLine("Your notes: ");

			Console.ForegroundColor = ConsoleColor.White;
			for(int i = 0; i < notes.Count; i++)
			{
				WriteNote(notes[i]);
			}

			Console.WriteLine("");
		}

		static void WriteFullNotes(List<Note> notes)
		{
			Console.ForegroundColor = ConsoleColor.Green;
			Console.WriteLine("Your notes: ");

			Console.ForegroundColor = ConsoleColor.White;
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
			Console.WriteLine($"Text: {n.Text} \nClearText: {n.ClearText}  \nHashString: {n.HashString} \n\n");
		}
	}
}
