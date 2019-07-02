using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp.NewDb
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var db = new BloggingContext())
            {

                //************************CREATING A BLOG, POST AND AUTHOR OBJECT************************
                //Blog b = new Blog
                //{
                //    Url = "www.tumblr.com",
                //    Posts = new List<Post>()
                //};

                //Post p1 = new Post
                //{
                //    Title = "Fencing 101",
                //    Author = new Author
                //    {
                //        Name = "Haibin"
                //    }
                //};

                //Post p2 = new Post
                //{
                //    Title = "Clam Chowder",
                //    Author = new Author
                //    {
                //        Name = "Hugh"
                //    }
                //};

                //Post p3 = new Post
                //{
                //    Title = "Fencing 201",
                //    Author = new Author
                //    {
                //        Name = "Henry"
                //    }
                //};

                //b.Posts.Add(p1);
                //b.Posts.Add(p2);
                //b.Posts.Add(p3);
                //db.Blogs.Add(b);
                //db.SaveChanges();
                //************************USING LINQ QUERY TO ADD TO A BLOG************************
                //Blog addingBlog = db.Blogs.Where(temp2 => temp2.Url.Equals("www.tumblr.com")).Single();

                //addingBlog.Posts.Add(
                //    new Post
                //    {
                //        Title = "Fencing 301",
                //        Author =
                //        new Author { Name = "Danny" }
                //    }
                //);


                //addingBlog.Posts.Add(
                //    new Post
                //    {
                //        Title = "Fencing 201",
                //        Author =
                //        new Author { Name = "Haibin" }
                //    }
                //);

                //db.SaveChanges();
                //*************************GETTING RELEVANT DATA FROM A BLOG************************
                //Blog b = db.Blogs.Where(t => t.Url.Equals("www.twitter.com")).Include(t => t.Posts).Single();
                //b.Posts.Add(new Post { Title = "Fencing 427", Author = new Author { Name = "Sam" } });

                //*************************DELETING AN AUTHOR (CHILD - ONE TO ONE) ************************
                //Author a = db.Authors.Where(author => author.Name.Equals("Li")).Single();
                //db.Authors.Remove(a); //WILL CAUSE IN ERROR WHEN TRYING TO DISPLAY POSTS

                ////FIX
                //Post p = db.Posts.Where(post => post.Title.Equals("Chicken Soup")).Single();
                //p.Author = new Author { Name = "Li" };

                //Post p = db.Posts.Where(post => post.Title.Equals("Chicken Soup")).Include(post => post.Author).Single();
                //Author a2 = new Author
                //{
                //    Name = "Li"
                //};
                //p.Author = null;
                //Console.WriteLine(p.Author.Name);
                //a2.Post = null;

                //p.Author = null; --> equivalent to: db.Authors.Remove(a);
                //So the author is deleted, however, the Post still Exists, just the Author Navigation property is now null

                //*************************DELETING A POST (PARENT - ONE TO ONE) & (CHILD - ONE TO MANY) ************************
                //Blog b = db.Blogs.Where(temp => temp.Url.Equals("www.tumblr.com")).Include(temp => temp.Posts).ThenInclude(post => post.Author).Single();
                //Post p = db.Posts.Where(post => post.Title.Equals("Fencing 101")).Single();
                //db.Posts.Remove(p);
                ////b.Posts.Remove(p); --> equivalent to db.Posts.Remove(p);

                ////The post and the author is deleted, However, the blog still remains, the post is no longer in its List<Post>

                //*************************DELETING A BlOG (PARENT - ONE TO MANY)************************
                //Blog b1 = db.Blogs.Where(temp => temp.Url.Equals("www.tumblr.com")).Include(temp => temp.Posts).ThenInclude(post=> post.Author).Single();
                //Blog b2 = db.Blogs.Where(temp => temp.Url.Equals("www.tumblr.com")).Single();// --> Still gets rid of all posts and authors
                //db.Blogs.Remove(b1);

                ////Everything: Blog, Posts, Authors are all deleted

                //*************************CHANGING THE MODEL SO THAT DELETING A POST WON'T DELETE THE AUTHOR************************
                //Say that I look back on my model and say, "Huh, I should've made the Author the parent to the post" that way, if I 
                //Delete the post, the author will still exist, definitely seems like a better design...

                //Update the model and put the foreign key inside Post instead
                //Create a new migration, update the database, and should be good

                //Uh oh, changing the model in this case causes a loss of information, in order to revert, delete the migration, and restore
                //A previous version using Update-Database [Previous Migration Name]

                //You can also edit the migration scaffolder more to prevent data loss

                //************************DO NOT FORGET SAVE CHANGES************************
                //db.SaveChanges();

                //************************ADVANCED QUERIES************************
                ////Get all posts that have "Fencing" in them
                //List<Post> p1 = db.Posts.Where(p => p.Title.Contains("Fencing")).ToList();


                //foreach (Post p in p1)
                //{
                //    Console.WriteLine(p.Title);
                //}

                //Console.WriteLine();
                ////Get all posts in a blog that have "Fencing" in them
                //List<Post> p2 = db.Blogs.Where(b => b.Url.Equals("www.tumblr.com")).Include(b => b.Posts).Single().Posts.Where(p => p.Title.Contains("Fencing")).ToList();
                //foreach (Post p in p2)
                //{
                //    Console.WriteLine(p.Title);
                //}
                //Post noBlogPost = new Post
                //{
                //    Title = "Post with no blog!"
                //};

                //db.Posts.Add(noBlogPost);
                //db.SaveChanges();

                //Get all blogs that have at least 1 post with "Fencing" in it
                //List<Blog> FencingBlogs = new List<Blog>();

                //foreach (Blog b in db.Blogs.Include(b => b.Posts).ToList())
                //{
                //    foreach( Post p in b.Posts)
                //    {
                //        if (p.Title.Contains("Fencing"))
                //        {
                //            FencingBlogs.Add(b);
                //        }
                //    }
                //}

                //foreach (Blog b in FencingBlogs)
                //{
                //    Console.WriteLine(b.Url);
                //}

                //************************DISPLAY POSTS AND AUTHORS************************
                var posts = db.Posts.Include(post => post.Author);
                var authors = db.Authors.Include(author => author.Post);
                var blogs = db.Blogs.Include(blog => blog.Posts).ThenInclude(post => post.Author);

                Console.WriteLine("Posts");
                Console.WriteLine("-----------");
                foreach (var post in posts)
                {
                    //Posts.Remove(post);
                    Console.WriteLine("{0}, written by: {1}", post.Title, post.Author.Name);
                }
                Console.WriteLine();
                Console.WriteLine("Authors");
                Console.WriteLine("-----------");
                foreach (var author in authors)
                {
                    //db.Authors.Remove(author);
                    Console.WriteLine("{0} wrote: {1}", author.Name, author.Post.Title);
                }

                //************************DISPLAY BLOGS AND ITS POSTS AND AUTHORS************************
                Console.WriteLine();
                Console.WriteLine("Blogs");
                Console.WriteLine("-----------");
                foreach (var blog in blogs)
                {
                    //db.Blogs.Remove(blog);
                    Console.WriteLine("{0} posts: ", blog.Url);
                    foreach (var post in blog.Posts)
                    {
                        //Posts.Remove(post);
                        Console.WriteLine("     Title: {0}, Author: {1}", post.Title, post.Author.Name);
                    }
                }
            }
            Console.ReadLine();
        }
    }
}