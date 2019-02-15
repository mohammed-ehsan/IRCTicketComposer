using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using Outlook = Microsoft.Office.Interop.Outlook;

namespace IRC_Helpdesk_Message_Composer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        #region Public Properties

        public string Subject { get; set; }

        public string To { get; set; } = "helpdesk@rescue.org";

        public List<string> MainCategories { get; set; } = new List<string>{
            "Hardware",
            "Software",
            "Account",
            "Security",
            "Internet",
            "Others"
        };

        public static List<string> SoftwareIssues = new List<string> { "Installation", "Update", "Malfunction", "Tech Question", "Others" };

        public event PropertyChangedEventHandler PropertyChanged;

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
        
        public string MainCategory { get; set; }
        public string SubCategory { get; set; }
        public string Details { get; set; }

        #endregion

        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = this;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(this.MainCategory))
            {
                MessageBox.Show("Please select main category.","Note",MessageBoxButton.OK,MessageBoxImage.Exclamation);
                return;
            }
            if (string.IsNullOrWhiteSpace(this.SubCategory))
            {
                MessageBox.Show("Please select sub category.","Note",MessageBoxButton.OK,MessageBoxImage.Exclamation);
                return;
            }
                
            var app = new Outlook.Application();
            
            var mail = app.CreateItem(Outlook.OlItemType.olMailItem) as Outlook.MailItem;
            var i = mail.GetInspector;
            mail.To = this.To;
            string subject = string.Empty;
            if (string.IsNullOrWhiteSpace(this.Details))
                subject = this.SubCategory;
            else
                subject = SubCategory + " - " + Details;
            mail.Subject =  subject;
            mail.HTMLBody = string.Format(@"<P>Dear IT Team,<br><br>
                Please I need help with the following issue:<br>
                <b>Main Category:</b> {0}<br>
                <b>Secondary Category:</b> {1}<br>
                <b>Details:</b> {2}<br><br>
                Regards.</P>", MainCategory,SubCategory,Details) + mail.HTMLBody;
            mail.Display(false);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var app = new Outlook.Application();
            var mail = app.CreateItem(Outlook.OlItemType.olMailItem) as Outlook.MailItem;
            mail.To = this.To;
            var i = mail.GetInspector;
            mail.Display(false);
        }
    }
}
