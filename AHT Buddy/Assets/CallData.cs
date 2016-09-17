using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace AHT_Buddy
{
    public class CallData : ObservableCollection<Customer>
    {
        public CallData() : base() { }

    }
    public class Customer
    {
        private string email;
        private string ticket;
        private string name;
        private string account;
        private string contact;
        private string device;
        private string issue;
        private string resolution;
        private string next;
        private string attempt;
        private bool chronic;
        private string timestamp;
        private int problemcode;
        private int causecode;
        private int solutioncode;

        public string Email { get { return email; } set { email = value; } }
        public string Ticket { get { return ticket; } set { ticket = value; } }
        public string Name { get { return name; } set { name = value; } }
        public string Account { get { return account; } set { account = value; } }
        public string Contact { get { return contact; } set { contact = value; } }
        public string Device { get { return device; } set { device = value; } }
        public string Issue { get { return issue; } set { issue = value; } }
        public string Resolution { get { return resolution; } set { resolution = value; } }
        public string Next { get { return next; } set { next = value; } }
        public string Attempt { get { return attempt; } set { attempt = value; } }
        public bool Chronic { get { return chronic; } set { chronic = value; } }
        public int ProblemCode { get { return problemcode; } set { problemcode = value; } }
        public int CauseCode { get { return causecode; } set { causecode = value; } }
        public int SolutionCode { get { return solutioncode; } set { solutioncode = value; } }
        public string TimeStamp { get { return timestamp; } set { timestamp = value; } }

        public Customer(
            string Email,
            string Ticket,
            string Name,
            string Account,
            string Contact,
            string Device,
            string Issue,
            string Resolution,
            string Next,
            string Attempt,
            bool Chronic,
            int ProblemCode,
            int CauseCode,
            int SolutionCode,
            string TimeStamp)
        {
            this.email = Email;
            this.ticket = Ticket;
            this.name = Name;
            this.account = Account;
            this.contact = Contact;
            this.device = Device;
            this.issue = Issue;
            this.resolution = Resolution;
            this.next = Next;
            this.attempt = Attempt;
            this.Chronic = Chronic;
            this.problemcode = ProblemCode;
            this.causecode = CauseCode;
            this.solutioncode = SolutionCode;
            this.timestamp = TimeStamp;
        }

    }
}
