namespace StackOverflowPost
{
    using System;
    using System.Text;

    public class Post
    {
        public Post(string title, string description)
        {
            this.Title = title;
            this.Description = description;
            this.DateTime = DateTime.Now;
            this.Vote = 0;
        }

        public string Title { get; private set; }

        public string Description { get; private set; }

        public DateTime DateTime { get; private set; }

        public int Vote { get; private set; }

        public void UpVote()
        {
            this.Vote++;
        }

        public void DownVote()
        {
            this.Vote--;
        }

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.AppendLine($"Post:");
            stringBuilder.AppendLine($"  Title: {this.Title}");
            stringBuilder.AppendLine($"  Description: {this.Description}");
            stringBuilder.AppendLine($"  Posted on: {this.DateTime}");
            stringBuilder.Append($"Votes: {this.Vote}");

            return stringBuilder.ToString().TrimEnd();
        }
    }
}
