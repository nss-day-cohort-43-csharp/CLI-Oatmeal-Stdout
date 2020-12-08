using System;
using System.Collections.Generic;
using TabloidCLI.Models;
using System.Text;
using TabloidCLI.Repositories;

namespace TabloidCLI.UserInterfaceManagers
{
    public class PostManager : IUserInterfaceManager
    {
        private readonly IUserInterfaceManager _parentUI;
        private PostRepository _postRepository;
        private string _connectionString;
        private AuthorRepository _authorRepository;
        private BlogRepository _blogRepository;

        public PostManager(IUserInterfaceManager parentUI, string connectionString)
        {
            _parentUI = parentUI;
            _postRepository = new PostRepository(connectionString);
            _connectionString = connectionString;
            _authorRepository = new AuthorRepository(connectionString);
            _blogRepository = new BlogRepository(connectionString);
        }

        public IUserInterfaceManager Execute()
        {
            Console.WriteLine("Post Menu");
            Console.WriteLine("1) List Posts");
            Console.WriteLine("2) Add Post");
            Console.WriteLine("4) Edit Post");
            Console.WriteLine("5) Remove Post");
            Console.WriteLine("0) Go Back");

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
                    Console.WriteLine("Sorry, that doesn't seem to be working. Please select something else.");
                    //Edit();
                    return this;
                case "4":
                    Console.WriteLine("Sorry, that doesn't seem to be working. Please select something else.");
                    //Remove();
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
            List<Post> posts = _postRepository.GetAll();
            foreach (Post post in posts)
            {
                Console.WriteLine($"- {post.Title}");
                Console.WriteLine($"- {post.Url}");
                Console.WriteLine($"- {post.PublishDateTime}");
                Console.WriteLine("");

            }
        }

        private void Add()
        {
            Console.WriteLine("New Post");
            Post post = new Post();

            Console.Write("Title: ");
            post.Title = Console.ReadLine();

            Console.Write("Url: ");
            post.Url = Console.ReadLine();

            Console.Write("Date/Time: ");
            post.PublishDateTime = DateTime.Parse(Console.ReadLine());

            Console.WriteLine("Please select an Author: ");
            List<Author> authors = _authorRepository.GetAll();
            foreach (Author author in authors)
            {
                Console.WriteLine($"{author.Id} {author.FullName}");
            }
            post.Author = authors[int.Parse(Console.ReadLine()) - 1];

            Console.WriteLine("Please select a Blog: ");
            List<Blog> blogs = _blogRepository.GetAll();
            foreach (Blog blog in blogs)
            {
                Console.WriteLine($"{blog.Id} {blog.Title}");
            }
            post.Blog = blogs[int.Parse(Console.ReadLine()) - 1];

            _postRepository.Insert(post);
        }

            
    }
}
