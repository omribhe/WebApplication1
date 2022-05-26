namespace WebApplication1
{
    public class Message
    {
        public int id { get; set; }
        public string content { get; set; }

        public string created { get; set; }

        public bool sent { get; set; }

        public Message()
        {

        }
        public Message(int id,string content, string created, bool sent)
        {
            this.id = id;
            this.content = content;
            this.created = created;
            this.sent = sent;

        }


    }



}
