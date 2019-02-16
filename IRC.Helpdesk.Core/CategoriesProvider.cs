using System;
using System.Collections.Generic;
using System.Linq;

namespace IRC.Helpdesk.Core
{
    public class CategoriesProvider : ICategoriesProvider
    {
        #region Public Properties

        /// <summary>
        /// Main categories.
        /// </summary>
        public List<string> MainCategories { get; set; } = new List<string>{
            "Hardware",
            "Software",
            "Account",
            "Security",
            "Internet",
            "Others"
        };

        /// <summary>
        /// List of software issues common between all softwares.
        /// </summary>
        public static List<string> SoftwareIssues = new List<string> { "Installation", "Update", "Malfunction", "Tech Question", "Others" };

        /// <summary>
        /// Dictionary of secondary categories and their associated details.
        /// </summary>
        public Dictionary<string, Dictionary<string, List<string>>> SecondaryCategories { get; set; } = new Dictionary<string, Dictionary<string, List<string>>> {
            {"Hardware", new Dictionary<string,List<string>>()
                {
                    {"Printer" , new List<string>{ "Print Access", "Scan Access", "Toner Low", "Not Printing", "Not Scanning", "Malfunction", "Others" } }
                    ,
                    {"Laptop", new List<string>{ "Configuration", "Internet issue", "Scan Access", "Printer Access", "Change Password", "Forgot Password", "Dispose", "Tech Question", "Others"} }
                    ,
                    {"Mobile", new List<string>{ "Internet issue", "Application Setup", "Malfuntion", "Tech Question", "Others" } }
                    ,
                    {"Desktop", new List<string>{ "Configuration", "Internet issue", "Scan Access", "Printer Access", "Change Password", "Forgot Password", "Dispose", "Tech Question", "Others"} }
                }
            }
            ,
            {"Software", new Dictionary<string, List<string>>{
                    {"MS Office", SoftwareIssues },
                    {"Outlook", SoftwareIssues },
                    {"Skype", SoftwareIssues },
                    {"Google Chrome", SoftwareIssues },
                    {"WebEx", SoftwareIssues },
                    {"Mozilla Firefox", SoftwareIssues },
                }
            }
            ,
            {"Account", new Dictionary<string, List<string>>{
                    {"Creation",null },
                    {"Password Reset", null },
                    {"Enrollment", null },
                    {"Unlock", null },
                    {"Others", null }
                }
            }
            ,
            {"Security", new Dictionary<string, List<string>>{
                    {"Email", null },
                    {"Windows", null },
                    {"Network", null },
                    {"Internet", null },
                    {"Spam Email", null },
                    {"Phishing Attack", null }
                }
            }
            ,
            {"Internet", new Dictionary<string, List<string>>{
                {"No Internet", new List<string>{"Single person", "Multiple", "Office"} },
                }
            }
        };

        #endregion

        #region Public Methods

        public void Load()
        {
            throw new NotImplementedException();
        }

        public List<string> GetSecondaryCategories(string mainCategory)
        {
            if (string.IsNullOrWhiteSpace(mainCategory))
                return null;
            return this.SecondaryCategories[mainCategory].Keys.ToList();
        }

        public List<string> GetDetailsList(string mainCategory, string secondaryCategory)
        {
            if (string.IsNullOrWhiteSpace(mainCategory))
                return null;
            if (string.IsNullOrWhiteSpace(secondaryCategory))
                return null;
            return this.SecondaryCategories[mainCategory][secondaryCategory];
        }

        #endregion
    }
}
