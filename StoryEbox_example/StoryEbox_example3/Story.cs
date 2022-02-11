using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoryEbox_example3
{
    class Story
    {
        string name;
        string authorName;
        string genre;
        int noOfChapters;
        int noOfLikes;
        int noOfReads;

        public Story()
        {
        }

        public Story(string name, string authorName, string genre, int noOfChapters, int noOfLikes, int noOfReads)
        {
            this.name = name;
            this.authorName = authorName;
            this.genre = genre;
            this.noOfChapters = noOfChapters;
            this.noOfLikes = noOfLikes;
            this.noOfReads = noOfReads;
        }

        public string Name { get => name; set => name = value; }
        public string AuthorName { get => authorName; set => authorName = value; }
        public string Genre { get => genre; set => genre = value; }
        public int NoOfChapters { get => noOfChapters; set => noOfChapters = value; }
        public int NoOfLikes { get => noOfLikes; set => noOfLikes = value; }
        public int NoOfReads { get => noOfReads; set => noOfReads = value; }

        public static SortedList<string, int> GenreWiseCount(List<Story> stories)
        {
            SortedList<string, int> genreWiseCount = new SortedList<string, int>();
           
            foreach (var storie in stories)
            {
                if (genreWiseCount.ContainsKey(storie.Genre))
                { genreWiseCount[storie.Genre]++; }
                else
                { genreWiseCount.Add(storie.Genre, 1); }
            }
            return genreWiseCount;
        }
    }
}
