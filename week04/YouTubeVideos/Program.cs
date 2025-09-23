using System;
using System.Collections.Generic;

public class Comment
{
    public string UserName { get; set; }
    public string Text { get; set; }

    public Comment(string userName, string text)
    {
        UserName = userName;
        Text = text;
    }
}

public class Video
{
    public string Title { get; set; }
    public string Author { get; set; }
    public int Length { get; set; } // in seconds
    private List<Comment> Comments { get; set; } = new List<Comment>();

    public Video(string title, string author, int length)
    {
        Title = title;
        Author = author;
        Length = length;
    }

    public void AddComment(Comment comment)
    {
        Comments.Add(comment);
    }

    public int GetNumberOfComments()
    {
        return Comments.Count;
    }

    public List<Comment> GetComments()
    {
        return Comments;
    }
}

class Program
{
    static void Main(string[] args)
    {
        List<Video> videos = new List<Video>();

        // Video 1
        Video video1 = new Video("C# Basics Tutorial", "John Doe", 600);
        video1.AddComment(new Comment("Alice", "Great video! Very helpful."));
        video1.AddComment(new Comment("Bob", "Thanks for the clear explanations."));
        video1.AddComment(new Comment("Charlie", "I learned a lot from this."));
        video1.AddComment(new Comment("Diana", "Excellent content!"));
        videos.Add(video1);

        // Video 2
        Video video2 = new Video("Advanced OOP in C#", "Jane Smith", 900);
        video2.AddComment(new Comment("Eve", "This was advanced but well-explained."));
        video2.AddComment(new Comment("Frank", "Loved the examples."));
        video2.AddComment(new Comment("Grace", "A bit too fast, but good info."));
        videos.Add(video2);

        // Video 3
        Video video3 = new Video("Building Console Apps with C#", "Mike Johnson", 450);
        video3.AddComment(new Comment("Hannah", "Simple and to the point."));
        video3.AddComment(new Comment("Ian", "Perfect for beginners."));
        video3.AddComment(new Comment("Judy", "Will try this out."));
        video3.AddComment(new Comment("Kyle", "Thanks!"));
        videos.Add(video3);

        // Video 4
        Video video4 = new Video("C# and .NET Fundamentals", "Sara Lee", 750);
        video4.AddComment(new Comment("Liam", "Comprehensive overview."));
        video4.AddComment(new Comment("Mia", "Really useful for interviews."));
        video4.AddComment(new Comment("Noah", "Bookmarked for later."));
        videos.Add(video4);

        // Display each video
        foreach (Video video in videos)
        {
            Console.WriteLine($"Title: {video.Title}");
            Console.WriteLine($"Author: {video.Author}");
            Console.WriteLine($"Length: {video.Length} seconds");
            Console.WriteLine($"Number of Comments: {video.GetNumberOfComments()}");
            Console.WriteLine("Comments:");
            foreach (Comment comment in video.GetComments())
            {
                Console.WriteLine($"- {comment.UserName}: {comment.Text}");
            }
            Console.WriteLine(); // Empty line for separation
        }
    }
}