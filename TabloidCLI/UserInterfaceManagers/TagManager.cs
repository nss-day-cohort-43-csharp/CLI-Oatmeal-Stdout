using System;
using System.Collections.Generic;
using System.Text;
using TabloidCLI.Models;

namespace TabloidCLI.UserInterfaceManagers
{
    public class TagManager : IUserInterfaceManager
    {
        private readonly IUserInterfaceManager _parentUI;
        private readonly TagRepository _tagRepository;
        private string _connectionString;

        public TagManager(IUserInterfaceManager parentUI, string connectionString)
        {
            _parentUI = parentUI;
            _connectionString = connectionString;
            _tagRepository = new TagRepository(connectionString);

        }

        public IUserInterfaceManager Execute()
        {
            Console.WriteLine();
            Console.WriteLine("Tag Menu");
            Console.WriteLine(" 1) List Tags");
            Console.WriteLine(" 2) Add Tag");
            Console.WriteLine(" 3) Edit Tag");
            Console.WriteLine(" 4) Remove Tag");
            Console.WriteLine(" 0) Go Back");

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
                case "3":
                     Edit();
                    return this;
                case "4":
                    //  Remove();
                    return this;
                case "0":
                    return _parentUI;
                default:
                    Console.WriteLine("Invalid Selection");
                    return this;
            }
        }
        //To select an option
        private Tag Choose(string prompt = null)
        //Ask for selection
        {
            if (prompt == null)
            {
                prompt = "Select a tag";
            }

            Console.WriteLine(prompt);

            List<Tag> tags = _tagRepository.GetAll();

            for (int i = 0; i < tags.Count; i++)
            {
                Tag tag = tags[i];
                Console.WriteLine($" {i + 1}) {tag.Name}");
            }
            Console.Write("> ");
            //Read the input of selected entry

            string input = Console.ReadLine();

            try
            {
                int choice = int.Parse(input);
                return tags[choice - 1];
            }
            catch (Exception)
            {
                Console.WriteLine("Invalid Selection");
                return null;
            }
        }

        //List all tags
        private void List()
        {
            List<Tag> tags = _tagRepository.GetAll();
            foreach (Tag tag in tags)
            {
                Console.WriteLine(" =-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=");
                Console.WriteLine();
                Console.WriteLine($"Tag Name:{tag.Name}");
                Console.WriteLine();

            }
        }
        //Add a new tag
        private void Add()
        {
            Console.WriteLine();
            Console.WriteLine("Create a new tag");
            Console.WriteLine(" =-=-=-=-=-=-=-=");
            Console.WriteLine();
            Tag tag = new Tag();
            Console.Write("Tag name: ");
            tag.Name = Console.ReadLine();

            _tagRepository.Insert(tag);
        }

        private void Edit()
        {
            Tag selectedTag = Choose("What tag would you like to edit?");
            if (selectedTag == null)
            {
                return;
            }
            Console.WriteLine();
            Console.WriteLine("What's the new tag?: ");
            string newName = Console.ReadLine();
            Console.WriteLine();
            //IsNullOrWhiteSpace will check if the string is null, empty or is only spaces
            if (!string.IsNullOrWhiteSpace(newName))
            {
                selectedTag.Name = newName;
            }
            //save to repo
            _tagRepository.Update(selectedTag);
        }
    }
}
      

       // private void Remove()
       // {
        //    throw new NotImplementedException();
       // }
    

