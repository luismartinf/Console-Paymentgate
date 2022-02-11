using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoryEbox_example5
{
    class Program_example5
    {
        static void Main(string[] args)
        {
            
            List<Story> stories = new List<Story>();
            Console.WriteLine("Enter the number of stories and stories name, authorName, genre, noOfChapters, noOfLikes, noOfReads");
            int num_stories = Convert.ToInt32(Console.ReadLine());
            for (int n_str = 0; n_str < num_stories; n_str++)
            {
                string storie = Console.ReadLine();
                Story story = Story.createStory(storie);
                stories.Add(story);
            }
            Console.WriteLine($"Enter a type to sort: \n 1.Sort by Name \n 2.Sort by No. of Reads");
            int s_type = Convert.ToInt32(Console.ReadLine());
            switch (s_type)
            {
                case 1:

                    if (stories.Count == 1)
                    {
                        Console.WriteLine("name \t authorName\t genre\t noOfChapters\t noOfLikes \t noOfReads");
                        Story story = stories[0];
                        Console.WriteLine($"{story.Name} \t {story.AuthorName}\t {story.Genre}\t {story.NoOfChapters}\t {story.NoOfLikes} \t {story.NoOfReads}");
                    }
                    else
                    {
                        List<Story> ordered_list = new List<Story>();
                        ordered_list.Add(stories[0]);
                        foreach (var element in stories.GetRange(1, stories.Count-1))
                        {
                            int order_comp = 0;
                            int order = 0;
                            while (order_comp <= 0)
                            { for (order = 0; order < ordered_list.Count; order++)
                                  { order_comp=element.CompareTo(ordered_list[order]); }
                            }
                            ordered_list.Insert(order,element);

                        }
                        foreach (var item in ordered_list)
                        {
                            Console.WriteLine("name \t authorName\t genre\t noOfChapters\t noOfLikes \t noOfReads");
                            Console.WriteLine($"{item.Name} \t {item.AuthorName}\t {item.Genre}\t {item.NoOfChapters}\t {item.NoOfLikes} \t {item.NoOfReads}");
                        }
                    }
                    break;

                case 2:

                    if (stories.Count == 1)
                    {
                        Console.WriteLine("name \t authorName\t genre\t noOfChapters\t noOfLikes \t noOfReads");
                        Story story = stories[0];
                        Console.WriteLine($"{story.Name} \t {story.AuthorName}\t {story.Genre}\t {story.NoOfChapters}\t {story.NoOfLikes} \t {story.NoOfReads}");
                    }
                    else
                    {
                        List<Story> ordered_list = new List<Story>();
                        ordered_list.Add(stories[0]);
                        foreach (var element in stories.GetRange(1, stories.Count-1))
                        {
                            int order_comp = 0;
                            int order = 0;
                            while (order_comp <= 0)
                            {
                                for (order = 0; order < ordered_list.Count; order++)
                                {
                                    NoOfReadsComparator noOfReads_elemt = new NoOfReadsComparator(element.Name, element.AuthorName, element.Genre, element.NoOfChapters, element.NoOfLikes, element.NoOfReads);
                                    NoOfReadsComparator noOfReads_order = new NoOfReadsComparator(ordered_list[order].Name, ordered_list[order].AuthorName, ordered_list[order].Genre, ordered_list[order].NoOfChapters, ordered_list[order].NoOfLikes, ordered_list[order].NoOfReads);
                                    order_comp = noOfReads_elemt.CompareTo(noOfReads_order);
                                }
                            
                            }
                            ordered_list.Insert(order, element);

                        }
                        foreach (var item in ordered_list)
                        {
                            Console.WriteLine("name \t authorName\t genre\t noOfChapters\t noOfLikes \t noOfReads");
                            Console.WriteLine($"{item.Name} \t {item.AuthorName}\t {item.Genre}\t {item.NoOfChapters}\t {item.NoOfLikes} \t {item.NoOfReads}");
                        }
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
