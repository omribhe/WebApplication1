using WebApplication1.Models;

namespace WebApplication1;
public static class Database
    {
        public static List<User> users = new List<User>();

        public static void createDatabase()
        {

            //---------------------------------------------------------------------------//
            List<Contact> contacts2 = new List<Contact>();

            User uri = new User("uri", "uri", "Omri123!", "localhost:3092", "", contacts2);
            users.Add(uri);

            User omri = new User("omri", "omri", "Omri123!", "localhost:3092", " ", new List<Contact>());
            users.Add(omri);

        Contact uriContact = new Contact("uri", "uri", "", "localhost:3092", "");
        Contact omriContact = new Contact("omri", "omri", "", "localhost:3092", "");
        uri.Contacts.Add(omriContact);
        omri.Contacts.Add(uriContact);

        Database.addTransfer("uri", "omri", "message1");
        Database.addTransfer("uri", "omri", "message2");
        Database.addTransfer("omri", "uri", "message1");
        Database.addTransfer("omri", "uri", "message2");


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

