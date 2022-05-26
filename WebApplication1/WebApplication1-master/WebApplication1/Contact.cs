namespace WebApplication1;
using System.ComponentModel.DataAnnotations;
    public class Contact
    {
        public string id { get; set; }
        public string name { get; set; }

        public string server { get; set; }

        public string last { get; set; }

        public string lastdate { get; set; }
        public List<Message> messages { get; set; }

        public int countMessages { get; set; }

    
        public Contact()
        {
        messages = new List<Message>();
        }
        public Contact(string id, string name, string last, string server, string lastDate)
        {
        this.id = id;
        this.name = name;   
        this.last = last;
        this.server = server;           
        this.lastdate = lastDate;
        messages = new List<Message>();
        countMessages = 0;
    }
}
