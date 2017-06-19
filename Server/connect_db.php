<?php
	$con=mysql_connect("localhost","root","");	 //change you connection options
    $db=mysql_select_db("my_db",$con);     //change your database
    
    if (!$con){
     die ("Colud not connect to server: ".mysql_error());
    }
    
    if(!$db){
     die ("Could not connect to database ".mysql_error());
    }
?>