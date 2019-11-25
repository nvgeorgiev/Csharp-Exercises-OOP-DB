namespace StackOverflowPost
{
    using System;

    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Welcome to STACKOVERFLOW" + Environment.NewLine);

            Console.WriteLine("Post title:");
            string title = Console.ReadLine();

            Console.WriteLine(Environment.NewLine + "Post description:");
            string description = Console.ReadLine();

            var post = new Post(title, description);

            Console.WriteLine(Environment.NewLine + "Use \"up\" and \"down\" to cast your vote for this post.");
            Console.WriteLine("Use \"end\" to close this post." + Environment.NewLine);

            while (true)
            {
                string inputVote = Console.ReadLine().ToLower();

                if (inputVote == "end")
                {
                    break;
                }

                if (inputVote == "up")
                {
                    post.UpVote();
                }
                else if (inputVote == "down")
                {
                    post.DownVote();
                }
                else
                {
                    Console.WriteLine("Invalid Input!");
                }
            }

            Console.WriteLine(Environment.NewLine + post);
        }
    }
}
