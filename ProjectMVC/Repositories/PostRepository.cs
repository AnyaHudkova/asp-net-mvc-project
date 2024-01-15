using Microsoft.EntityFrameworkCore;
using ProjectMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectMVC.Repositories
{
    public class PostRepository
    {
        public AppDbContext context { get; set; }
        private DbSet<Post> table = null;
        List<Post> posts = new List<Post>();
        private UserRepository userRepository;

        public PostRepository(AppDbContext context)
        {
            this.context = context;
            table = context.Set<Post>();
            userRepository = new UserRepository(context);
        }
        public void Add(Post postModel)
        {
            postModel.Date = DateTime.Now;
            table.Add(postModel);
            context.SaveChanges();
        }
        public void DeletePost(int postId)
        {
            table.Remove(table.Find(postId));
            context.SaveChanges();
        }
        public List<Post> GetPostsByUserId(int user_id)
        {
            this.posts.Clear();
            foreach (Post post in table)
            {
                if(post.User_id == user_id)
                {
                    posts.Add(post);
                }
            }
            if(posts.Count > 1)
            {
                posts.Sort(delegate (Post x, Post y){return y.Date.CompareTo(x.Date);});
            }
            return posts;
        }
        public List<Post> GetAllPosts()
        {
            this.posts.Clear();
            foreach (Post post in table)
            {
               posts.Add(post);
            }
            if (posts.Count > 1)
            {
                posts.Sort(delegate (Post x, Post y) { return y.Date.CompareTo(x.Date); });
            }
            return posts;
        }

        public List<PostsViewModel> GetPostsAndUsers()
        {
            List<PostsViewModel> list = new List<PostsViewModel>();
            posts = GetAllPosts();

            foreach(Post post in posts)
            {
                User user = userRepository.GetUserById(post.User_id);
                PostsViewModel model = new PostsViewModel();
                model.Post = post;
                model.User = user;
                list.Add(model);
            }
            return list;
        }
    }
}
