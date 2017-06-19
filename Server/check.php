<?php
function anti_injection($input){
      $clean=strip_tags(addslashes(trim($input)));
      $clean=str_replace("'","\'",$clean);
      $clean=str_replace('"','\"',$clean);
      $clean=str_replace(';','\;',$clean);
      $clean=str_replace('--','\--',$clean);
      $clean=str_replace('+','\+',$clean);
      $clean=str_replace('(','\(',$clean);
      $clean=str_replace(')','\)',$clean);
      $clean=str_replace('=','\=',$clean);
      $clean=str_replace('>','\>',$clean);
      $clean=str_replace('<','\<',$clean);
      return $clean;
}

	//default password is test
	$password = "test";

	$salt = md5(sha1($password));			//you can create a table and save all into database
	$password = sha1($password.$salt);

	$test_string = $_POST['password'];
    $off = (strip_tags(addslashes($_POST['username'])));
    $key = anti_injection($off);
    $username = $key;
    
    //default user is test
    if ((sha1($test_string.$salt) == $password) && ($username == "test")){
    	session_start();
		$_SESSION['user']['id'] = 3;
		header("location: ./attacker_panel.php"); // Redirecting To Other Page
    }
    else {
    		header("refresh:0;url=./index.html");
    		echo '<script>alert("Username Or Password not correct");</script>';
            exit;
         }

?>
