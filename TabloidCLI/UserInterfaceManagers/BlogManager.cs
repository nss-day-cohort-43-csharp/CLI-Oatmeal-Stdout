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
            Console.WriteLine();
            Console.WriteLine("Blog Menu");
            Console.WriteLine(" 1) List Blogs");
            Console.WriteLine(" 2) Blog Details");
            Console.WriteLine(" 3) Add Blog");
            Console.WriteLine(" 4) Edit Blog");
            Console.WriteLine(" 5) Remove Blog");
            Console.WriteLine(" 0) Go Back");
            Console.Write("> ");
            string choice = Console.ReadLine();


            switch (choice)
            {
                case "1":
                    List();
                    return this;
                case "2":
                    Console.WriteLine("We're sorry, that function is unavailable at this time");
                    Console.WriteLine("Please make another selection");
                    Console.WriteLine();
                    return this;
                case "3":
                    Add();
                    Console.WriteLine();
                    return this;
                case "4":
                    Edit();
                    Console.WriteLine();
                    return this;
                case "5":
                    Remove();
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
                    Console.WriteLine("=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=");
                    Console.WriteLine();
                    Console.WriteLine($"- {b.Title}");
                    Console.WriteLine($"{b.Url}");
                    Console.WriteLine();
            }
        }

        private Blog Choose(string prompt = null)
        {
            if(prompt == null)
            {
                prompt = "Please make a selection";
            }

            Console.WriteLine(prompt);

            List<Blog> blogs = _blogRepository.GetAll();

            for(int i = 0; i < blogs.Count; i++ )
            {
                Blog blog = blogs[i];
                Console.WriteLine($"{i + 1}.) {blog.Title}");
            }

            Console.WriteLine(">");

            string input = Console.ReadLine();

            try
            {
                int choice = int.Parse(input);
                return blogs[choice - 1];
            }
            catch(Exception ex)
            {
                Console.WriteLine("Invalid Selection");
                return null;
            }
        }
        private void Add()
        {
            bool testToken = true;
            string url = null;
            string title = null;
            while (testToken)
            {

                Console.WriteLine("What is the title of the blog?");
                title = Console.ReadLine();
                if (!string.IsNullOrEmpty(title))
                {
                    break;
                }
                else
                {
                    Console.WriteLine("All Blogs must have a title, try again");
                }
            }

            while (testToken)
            {

                Console.WriteLine("What is the URL of the blog?");
                    url = Console.ReadLine();
                if (!string.IsNullOrEmpty(url))
                {
                    url = Console.ReadLine();
                    break;
                }
                else
                {
                    Console.WriteLine("All Blogs must have a URL, try again");
                }
            }
            Blog blog = new Blog
                {
                    Title = title,
                    Url = url
                };
                _blogRepository.Insert(blog); 
        }
        

        private void Edit()
        {
            Blog blogToEdit = Choose("What blog would you like to update?");

            if(blogToEdit == null)
            {
                return;
            }

            Console.WriteLine($"Current Title: {blogToEdit.Title}");
            Console.WriteLine("What would you like to update the title to?");
            Console.WriteLine(">");
            string title = Console.ReadLine();
            if(!String.IsNullOrWhiteSpace(title))
            {
                blogToEdit.Title = title;
            }

            Console.WriteLine("Please type the new URL");
            Console.WriteLine(">");
            string url = Console.ReadLine();
            if(!String.IsNullOrWhiteSpace(url))
            {
                blogToEdit.Url = url;
            }

            _blogRepository.Update(blogToEdit);
            
        }

        private void Remove()
        {
            Blog blogToDelete = Choose("What blog would you like to delete?");
            if (blogToDelete != null)
            {
                _blogRepository.Delete(blogToDelete.Id);
            }
        }
    }
}
