using System;
using System.Collections.Generic;
using System.Text;
using TabloidCLI.Models;

namespace TabloidCLI.UserInterfaceManagers
{
    class JournalManager : IUserInterfaceManager
    {
        private readonly IUserInterfaceManager _parentUI;
        private JournalRepository _journalRepository;
        private string _connectionString;

        public JournalManager(IUserInterfaceManager parentUI, string connectionString)
        {
            _parentUI = parentUI;
            _journalRepository = new JournalRepository(connectionString);
            _connectionString = connectionString;
        }
        public IUserInterfaceManager Execute()
        {
            Console.WriteLine("Journal Menu");
            Console.WriteLine(" 1) List Journal Entries");
            Console.WriteLine(" 2) Add New Journal Entry");
            //Console.WriteLine(" 3) Edit Journal Entry");
            Console.WriteLine(" 4) Remove Journal Entry");
            Console.WriteLine(" 0) Return to Main Menu");
            Console.Write("> ");
            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    List();
                    return this;
                case "2":
                    Add();
                    return this;
                case "4":
                    Remove();
                    return this;
                case "0":
                    return _parentUI;
                default:
                    Console.WriteLine("Invalid Selection");
                    return this;
            }
        }

    //Get list of journals
        private void List()
        {
            List<Journal> entries = _journalRepository.GetAll();
            foreach (Journal entry in entries)
            {
                Console.WriteLine($"Entry {entry.Id}: {entry.Title}");
                Console.WriteLine($"Created on: {entry.CreateDateTime}");
                Console.WriteLine($"Entry Content: {entry.Content}");
                Console.WriteLine("");
            }
        }

    //Add a new journal
        private void Add()
        {
            Console.WriteLine("New Journal");
            Journal journal = new Journal();

            Console.Write("Entry Title: ");
            journal.Title = Console.ReadLine();

            journal.CreateDateTime = DateTime.Now;

            Console.Write("What's on your mind?");
            journal.Content = Console.ReadLine();

            _journalRepository.Insert(journal);
        }
     //Delete an entry
        private void Remove()
        {
            Journal selectedEntry = Choose("What journal entry do you want to delete?");
            if (selectedEntry != null)
            {
               _journalRepository.Delete(selectedEntry.Id);
            }
        }
     //Select one
        private Journal Choose(string prompt = null)
    //Ask for selection
        {
            if (prompt == null)
            {
                prompt = "Select an entry";
            }

            Console.WriteLine(prompt);

            List<Journal> entries = _journalRepository.GetAll();

            for (int i = 0; i < entries.Count; i++)
            {
                Journal journal = entries[i];
                Console.WriteLine($" {i + 1}) {journal.Title}");
            }
            Console.Write("> ");
    //Read the input of selected entry

            string input = Console.ReadLine();
 
            try
            {
                int choice = int.Parse(input);
                return entries[choice - 1];
            }
            catch (Exception)
            {
                Console.WriteLine("Invalid Selection");
                return null;
            }
        }
    }
}
