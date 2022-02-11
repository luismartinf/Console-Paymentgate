using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoryEbox_example5
{
    class Story:IComparable
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

        public static Story createStory(String detail) 
        {
            string[] storie_array = detail.Split(',');
            string name = storie_array[0];
            string authorName = storie_array[1];
            string genre = storie_array[2];
            int noOfChapters = Convert.ToInt32(storie_array[3]);
            int noOfLikes = Convert.ToInt32(storie_array[4]);
            int noOfReads = Convert.ToInt32(storie_array[5]);
            Story story = new Story(name, authorName, genre, noOfChapters, noOfLikes, noOfReads);
            return (story);
        }

        public int CompareTo(object obj)
        {
            Story comp_obj = (Story)obj;
            return this.Name.CompareTo(comp_obj.Name);
        }

    }
}
