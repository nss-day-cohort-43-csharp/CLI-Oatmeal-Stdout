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
                   // Edit();
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

        private void Add()
        {
            //Add a new tag
            Console.WriteLine();
            Console.WriteLine("Create a new tag");
            Console.WriteLine(" =-=-=-=-=-=-=-=");
            Console.WriteLine();
            Tag tag = new Tag();
            Console.Write("Tag name: ");
            tag.Name = Console.ReadLine();

                _tagRepository.Insert(tag);
            }
        }

        //private void Edit()
        //{
       //     throw new NotImplementedException();
       // }

       // private void Remove()
       // {
        //    throw new NotImplementedException();
       // }
    }

