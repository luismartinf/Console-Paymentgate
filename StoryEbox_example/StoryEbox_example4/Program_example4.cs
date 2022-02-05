using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoryEbox_example4
{
    class Program_example4
    {
        static void Main(string[] args)
        {
            StoryBO storyBO = new StoryBO();
            List<Story> stories = new List<Story>();
            Console.WriteLine("Enter the number of stories and stories name, authorName, genre, noOfChapters, noOfLikes, noOfReads");
            int num_stories = Convert.ToInt32(Console.ReadLine());
            for (int n_str = 0; n_str < num_stories; n_str++)
            {
                string storie = Console.ReadLine();
                string[] storie_array = storie.Split(',');
                string name = storie_array[0];
                string authorName = storie_array[1];
                string genre = storie_array[2];
                int noOfChapters = Convert.ToInt32(storie_array[3]);
                int noOfLikes = Convert.ToInt32(storie_array[4]);
                int noOfReads = Convert.ToInt32(storie_array[5]);
                Story story = new Story(name, authorName, genre, noOfChapters, noOfLikes, noOfReads);
                stories.Add(story);
            }
            Console.WriteLine($"Enter a search type: \n 1.By author\n 2.By number of likes");
            int s_type = Convert.ToInt32(Console.ReadLine());
            switch (s_type)
            {
                case 1:
                    string author = Console.ReadLine();
                    List<Story> author_list = storyBO.findStory(stories,author);
                    if (author_list.Count > 0)
                    {
                        Console.WriteLine("name \t authorName\t genre\t noOfChapters\t noOfLikes \t noOfReads");
                        foreach (var story in author_list)
                        { Console.WriteLine($"{story.Name} \t {story.AuthorName}\t {story.Genre}\t {story.NoOfChapters}\t {story.NoOfLikes} \t {story.NoOfReads}"); }
                    }
                    else
                    { Console.WriteLine("No such story is present"); }
                    break;
                case 2:
                    int min_likes = Convert.ToInt32(Console.ReadLine());
                    List<Story> like_list = storyBO.findStory(stories, min_likes);
                    if (like_list.Count > 0)
                    { 
                        Console.WriteLine("name \t authorName\t genre\t noOfChapters\t noOfLikes \t noOfReads");
                        foreach (var story in like_list) 
                        { Console.WriteLine($"{story.Name} \t {story.AuthorName}\t {story.Genre}\t {story.NoOfChapters}\t {story.NoOfLikes} \t {story.NoOfReads}"); }
                    }
                    else
                    {
                        Console.WriteLine("No such story is present");
                    }
                  
                    break;
                default:
                    Console.WriteLine("Invalid choice");
                    break;
            }

            _ = Console.ReadKey();
        }

    }
}
