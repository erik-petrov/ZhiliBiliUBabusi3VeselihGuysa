using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using System.Web;
using Microsoft.VisualBasic.ApplicationServices;

namespace ZhiliBiliUBabusi3VeselihGuysa
{
    public enum SuguEnum
    {
        Male,
        Female
    }
    [Table("Players")]
    public class User
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [MaxLength(50)]
        public string Name { get; set; }
        public string Email { get; set; }
        public SuguEnum Sugu { get; set; } //1 - male, 0 - female
        public int Vanus { get; set; }
        public string Password { get; set; }
        public ICollection<MatchScore> Match { get; set; }
        public ICollection<MathScore> Math { get; set; }
        public User(string name, string email, SuguEnum sugu, int vanus, string password)
        {
            Name = name;
            Email = email;
            Sugu = sugu;
            Vanus = vanus;
            Password = password;
        }
        public static void CreatePlayer(string name, string email, SuguEnum sugu, int vanus, string password, ApplicationContext db)
        {
            db.Users.Add(new User(name, email, sugu, vanus, password));
            db.SaveChanges();
        }
        public static void Rename(string oldName, string newName, string email)
        {
            if (newName == "") throw new ArgumentException("Uus nimi on tühi");
            using (ApplicationContext db = new ApplicationContext())
            {
                User user = db.Users.Where(u => oldName == u.Name && email == u.Email).FirstOrDefault();
                if (user != null)
                {
                    user.Name = newName;
                    db.Users.Update(user);
                    db.SaveChanges();
                }
            }
        }
        public static bool TryLogin(string email, string pass)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                try
                {
                    User user = db.Users.Where(u => email == u.Email).FirstOrDefault();
                    if (user != null)
                    {

                        if (user.Password == pass)
                        {
                            Menu.loggedEmail = user.Email;
                            Menu.loggedName = user.Name;
                            return true;
                        }
                        throw new Exception("Parool on viga!");
                        
                    }
                    else
                    {
                        throw new Exception("Ei ole seda useri");
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                    return false;
                }
            }    
        }
        public static bool TryReg(string name, string pass, string email, SuguEnum sugu, int vanus)
        {
            using(ApplicationContext db = new ApplicationContext())
            {
                try
                {
                    if (db.Users.Where(u => email == u.Email).FirstOrDefault() != null)
                    {
                        throw new Exception("Email on regisreeritud");
                    }
                    db.Users.Add(new User(name, email, sugu, vanus, pass));
                    db.SaveChanges();
                    Menu.loggedEmail = email;
                    Menu.loggedName = name;
                    return true;
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                    return false;
                }
            }
        }
        public static string ForgotPassword(string name, string email, int vanus)
        {
            string password = "";
            try
            {
                using(ApplicationContext db = new ApplicationContext())
                {
                    var user = db.Users.Where(u => email == u.Email).FirstOrDefault();
                    if (user != null)
                    {
                        if(user.Name == name && user.Vanus == vanus)
                        {
                            Clipboard.SetText(user.Password);
                            MessageBox.Show($"Teie parool on: {user.Password}\nSee parool on ka kopeeritud.");
                            return user.Password;
                        }
                    }
                    else
                    {
                        throw new Exception("Ma ei tea seda emaili");
                    }
                }
            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message);
                return "";
            }
            return password;
        }
    }
    [Table("Match")]
    public class MatchScore
    {
        public int Id { get; set; }
        public int PlayerId { get; set; }
        public User Player { get; set; }
        public int Time { get; set; }
        public MatchScore(User user, int score)
        {
            PlayerId = user.Id;
            Player = user;
            Time = score;
        }
        public MatchScore()
        {
            PlayerId = 0;
            Player = null;
            Time = 0;
        }
    }
    [Table("Math")]
    public class MathScore
    {
        public int Id { get; set; }
        public int PlayerId { get; set; }
        public User Player { get; set; }
        public int Time { get; set; }
        public MathScore(User user, int score)
        {
            PlayerId = user.Id;
            Player = user;
            Time = score;
        }
        public MathScore()
        {
            PlayerId = 0;
            Player = null;
            Time = 0;
        }
    }
}
