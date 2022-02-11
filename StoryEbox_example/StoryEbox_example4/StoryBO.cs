using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoryEbox_example4
{
    class StoryBO
    {
        public List<Story> findStory(List<Story> storyList, string authorName)
        {
            
            List<Story> author_list = new List<Story>();
             foreach (var story in storyList)
            {
                if (story.AuthorName.Contains(authorName))
                {
                    author_list.Add(story);
                }
            }
            return author_list;
        }

        public List<Story> findStory(List<Story> storyList, int noOfLikes)
        {
            List<Story> likes_list = new List<Story>();
            foreach (var story in storyList)
            {
                if (story.NoOfLikes > noOfLikes)
                {
                    likes_list.Add(story);
                }
            }
            return likes_list;
        }
    }
}
