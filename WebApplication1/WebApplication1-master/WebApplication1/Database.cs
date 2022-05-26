namespace WebApplication1
{
    public static class Database
    {
        public static List<User> users = new List<User>();

        public static void createDatabase()
        {

            //---------------------------------------------------------------------------//
            List<Contact> contacts2 = new List<Contact>();

            User SagivU = new User("SagivU", "1111", "Sag", "a", "s1", contacts2);
            users.Add(SagivU);

            User Uri = new User("Uri", "1111", "Uriel", "a", "s1");
            users.Add(Uri);

            Contact uri = new Contact("Uri","Uri uri ", "Today", "Localhost:6166", "15-5-5");
            Contact omri = new Contact("omri", "Uri uri ", "Today", "Localhost:6166", "15-5-5");
            Contact sagiv = new Contact("Sagiv", "Uri uri ", "Today", "Localhost:6166", "15-5-5");

            SagivU.Contacts.Add(uri);
            Uri.Contacts.Add(sagiv);

            SagivU.Contacts.Add(omri);
            Uri.Contacts.Add(omri);

            //--------------------------------------------------------------------------//


        }

        public static void Insert(User user)
        {
            users.Add(user);
        }
        public static void Remove(User user)
        {
            users.Remove(user);
        }
        public static void addContact(string name, Contact contact)
        {
            if(contact == null)
            {
                throw new ArgumentNullException();
            }
            if (users.Exists(x => x.Name == name))
            {
                users.Find(x => x.Name == name).Contacts.Add(contact);
            }
        }

        public static void addContactFromString(string name, String contact, String nickName, String server)
        {
            if (contact == null)
            {
                throw new ArgumentNullException();
            }
            if (users.Exists(x => x.Name == name))
            {
                Contact userFriend = new Contact(contact, nickName, null, server, null); 
                users.Find(x => x.Name == name).Contacts.Add(userFriend);
            }
        }

        public static void putContactFromString(string name, string id, String nickName, String server)
        {
            if (id == null)
            {
                throw new ArgumentNullException();
            }
            if (users.Exists(x => x.Name == name))
            {
                if (users.Find(x => x.Name == name).Contacts.Exists(x=> x.id == id))
                {
                    Contact rm = users.Find(x => x.Name == name).Contacts.Find(x => x.id == id);
                    rm.name = nickName;
                    rm.server = server;
                }
            }
        }

        public static void addMessage(string name, string contactID, Message message)
        {
            if (users.Exists(x => x.Name == name))
            {
               if (users.Find(x => x.Name == name).Contacts.Exists(x=> x.id == contactID))
                {
                    users.Find(x => x.Name == name).Contacts.Find(x => x.id == contactID).messages.Add(message);
                }
            }
        }



        public static void delContact(string name, string contactName)
        {
            
            if (users.Exists(x => x.Name == name))
            {
                Contact rm = users.Find(x => x.Name == name).Contacts.Find(x=>x.id == contactName);
                if (rm != null)
                {
                    users.Find(x => x.Name == name).Contacts.Remove(rm);
                }
            }
        }

        public static void addTransfer(string username, string contactName, string mess)
        {
            DateTime d = DateTime.Now;
            string time = d.ToString();
            Contact c = Database.users.Find(x => x.Name == username).Contacts.Find(x => x.id == contactName);
            Message message = new Message(c.countMessages, mess, time, false);
            c.messages.Add(message);
            c.last = mess;
            c.lastdate = time;
            c.countMessages++;
        }

        public static void addInvitation(string username, string contactName, string server)
        {
            User u = Database.users.Find(x => x.Name == username);
            Contact c = new Contact(contactName, contactName, null, server, null);
            u.Contacts.Add(c);
        }
    }
}
