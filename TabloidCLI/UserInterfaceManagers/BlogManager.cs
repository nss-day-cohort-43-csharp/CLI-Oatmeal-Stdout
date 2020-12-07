using System;
using System.Collections.Generic;
using System.Text;
using TabloidCLI.Repositories;
using TabloidCLI.Models;

namespace TabloidCLI.UserInterfaceManagers
{
    class BlogManager : IUserInterfaceManager
    {
        private IUserInterfaceManager _parentUI;
        private BlogRepository _blogRepository;
        private string _connectionString;

        public BlogManager(IUserInterfaceManager parentUI, string connectionString)
        {
            _parentUI = parentUI;
            _blogRepository = new BlogRepository(connectionString);
            _connectionString = connectionString;

        }

        public IUserInterfaceManager Execute()
        {
            Console.WriteLine("Blog Menu");
            Console.WriteLine(" 1) List Blogs");
            Console.WriteLine(" 2) Blog Details");
            Console.WriteLine(" 3) Add Blog");
            Console.WriteLine(" 4) Edit Blog");
            Console.WriteLine(" 5) Remove Blog");
            Console.WriteLine(" 0) Go Back");

            Console.WriteLine("> ");
            string choice = Console.ReadLine();

            switch(choice)
            {
                case "1":
                    List();
                    return this;
                case "2":
                    Console.WriteLine("We're sorry, that function is unavailable");
                    Console.WriteLine("Please make another selection");
                    Console.WriteLine();
                    return this;
                case "3":
                    Add();
                    Console.WriteLine();
                    return this;
                case "4":
                    Console.WriteLine("We're sorry, that function is unavailable");
                    Console.WriteLine("Please make another selection");
                    Console.WriteLine();
                    return this;
                case "5":
                    Console.WriteLine("We're sorry, that function is unavailable");
                    Console.WriteLine("Please make another selection");
                    Console.WriteLine();
                    return this;
                case "0":
                    return _parentUI;
                default:
                    Console.WriteLine("Invalid Selection");
                    return this;
            }
        }

        //Helper Methods to reduce size of switchboard
        private void List()
            //going into the _blogRepository to utilize the GetAll function found there
        {
            List<Blog> blogs = _blogRepository.GetAll();
            foreach(Blog b in blogs)
            {
                Console.WriteLine($"{b.Id}.) {b.Title}");
                Console.WriteLine($"{b.Url}");
                Console.WriteLine();
            }
        }

        private void Add()
        {
            Console.WriteLine("What is the title of the blog?");
            string title = Console.ReadLine();

            Console.WriteLine("What is the URL for the blog?");
            string url = Console.ReadLine();

            Blog blog = new Blog
            {
                Title = title,
                Url = url
            };
            _blogRepository.Insert(blog);
        }
    }
}
