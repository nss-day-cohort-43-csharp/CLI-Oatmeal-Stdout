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
            //Console.WriteLine(" 2) Add New Journal Entry");
            //Console.WriteLine(" 3) Edit Journal Entry");
            //Console.WriteLine(" 4) Remove Journal Entry");
            //Console.WriteLine(" 0) Return to Main Menu");
            //Console.Write("> ");
            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    List();
                    return this;
                default:
                    Console.WriteLine("Invalid Selection");
                    return this;
            }
        }
        private void List()
        {
            List<Journal> entries = _journalRepository.GetAll();
            foreach (Journal entry in entries)
            {
                Console.WriteLine(entries);
            }
        }
    }
}
