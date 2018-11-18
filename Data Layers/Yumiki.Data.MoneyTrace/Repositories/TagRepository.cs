using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using Yumiki.Commons.Dictionaries;
using Yumiki.Commons.Helpers;
using Yumiki.Data.MoneyTrace.Interfaces;
using Yumiki.Entity.MoneyTrace;
using Yumiki.Entity.MoneyTrace.ServiceObjects;

namespace Yumiki.Data.MoneyTrace.Repositories
{
    public abstract class TagRepository : MoneyTraceRepository
    {
        /// <summary>
        /// Save tags in "Tag A, Tag B, Tag C" string to TB_Tag table.
        /// </summary>
        /// <param name="tags">Tags with ',' separated char.</param>
        /// <param name="userID">User who creates tags.</param>
        protected void SaveTags(string tags, Guid userID)
        {
            if (!string.IsNullOrWhiteSpace(tags))
            {
                string[] tagList = tags.Split(new char[] { CommonValues.SeparateCharComma }, StringSplitOptions.RemoveEmptyEntries);

                //From existed tags to get the new tags which are not in db.
                IEnumerable<TB_Tag> existedTags = Context.TB_Tag.Where(c => tagList.Any(d => d.ToLower() == c.TagName.ToLower())).AsEnumerable();
                IEnumerable<string> newTags = tagList.Where(c => !existedTags.Any(d => d.TagName.Equals(c, StringComparison.InvariantCultureIgnoreCase)));

                foreach (string tag in newTags)
                {
                    TB_Tag dbTag = new TB_Tag();
                    dbTag.TagName = tag;
                    dbTag.UserID = userID;

                    Context.TB_Tag.Add(dbTag);
                }
            }
        }


        /// <summary>
        /// Get tags from keyword.
        /// </summary>
        /// <param name="keyword">Keyword to filter tag results.</param>
        /// <returns>List of tags after filter.</returns>
        public List<string> GetTags(string keyword)
        {
            return Context.TB_Tag.Where(c => c.TagName.Contains(keyword)).Select(c => c.TagName).ToList();
        }
    }
}
