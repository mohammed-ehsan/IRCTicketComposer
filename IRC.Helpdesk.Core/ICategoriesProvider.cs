using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IRC.Helpdesk.Core
{
    public interface ICategoriesProvider
    {
        /// <summary>
        /// Load the categories from data source.
        /// </summary>
        void Load();

        /// <summary>
        /// List of main categories.
        /// </summary>
        List<string> MainCategories { get; set; }

        /// <summary>
        /// Return list of secondary categories.
        /// </summary>
        /// <param name="mainCategory">Main category to return the secondary categories for.</param>
        /// <returns></returns>
        List<string> GetSecondaryCategories(string mainCategory);

        /// <summary>
        /// Return a list of pre-registered details.
        /// </summary>
        /// <param name="mainCategory">Selected main category</param>
        /// <param name="secondaryCategory">Selected secondary category</param>
        /// <returns></returns>
        List<string> GetDetailsList(string mainCategory, string secondaryCategory);
    }
}
