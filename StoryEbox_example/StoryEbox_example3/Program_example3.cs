using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoryEbox_example3
{
    class Program_example3
    {
        static void Main(string[] args)
        {
            List<Story> stories = new List<Story>();
            Console.WriteLine("Enter the number of stories and stories name, authorName, genre, noOfChapters, noOfLikes, noOfReads");
            int num_stories = Convert.ToInt32(Console.ReadLine());
            for (int n_str=0;n_str<num_stories;n_str++)
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

            SortedList<string, int> genr_count = Story.GenreWiseCount(stories);
            
            foreach(var g_count in genr_count)
            {
                Console.WriteLine($"{g_count.Key} \t {g_count.Value}");
            }
            _ = Console.ReadKey();
        }
    }
}
