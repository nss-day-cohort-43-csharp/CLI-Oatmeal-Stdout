using System;
using System.Collections.Generic;
using TabloidCLI.Models;
using TabloidCLI.Repositories;

namespace TabloidCLI.UserInterfaceManagers
{
    internal class BlogDetailManager : IUserInterfaceManager
    {
        //create classes that will be used to pull the repositories needed for the blog section
        private IUserInterfaceManager _parentUI;
        private BlogRepository _blogRepository;
        private int _blogId;

        //create a constructor that brings in a parentUI, the connectionString, and blogId parameters
        public BlogDetailManager(IUserInterfaceManager parentUI, string connectionString, int blogId)
        {
            //the constructor then assigns the fields with passed in parameter values
            _parentUI = parentUI;
            _blogRepository = new BlogRepository(connectionString);
        }

        public IUserInterfaceManager Execute()
        {
            // Blog blog = _blogRepository.Get(_blogId);
            return null;
        }
    }
}
