using System;

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