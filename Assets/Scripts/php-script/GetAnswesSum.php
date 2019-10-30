<?php
	$servername = "blitzdbinstance.cihkvhti3s6i.sa-east-1.rds.amazonaws.com";

    $dbname = "dbblitz"; 
	$username = "avlgames";
	$password = "Larissa42";
	
	
	$cd_player = $_GET["cd_player"];
	
	$conn = new mysqli($servername, $username, $password, $dbname );

	if(!$conn){
		die("Connection Failed" . mysqli_connect_error());
	}

	

	$sql = "select distinct (select sum(aa.answer)
		from   answers aa,
			   questions qq 
		where qq.question = aa.question
        and   aa.cd_player = a.cd_player
        and   qq.cd_category = 10) soma_sobrevivente,
		(select sum(aa.answer)
		from   answers aa,
			   questions qq 
		where qq.question = aa.question
        and   aa.cd_player = a.cd_player
        and   qq.cd_category = 20) soma_explorador,
        (select sum(aa.answer)
		from   answers aa,
			   questions qq 
		where qq.question = aa.question
        and   aa.cd_player = a.cd_player
        and   qq.cd_category = 30) soma_disney,
        (select sum(aa.answer)
		from   answers aa,
			   questions qq 
		where qq.question = aa.question
        and   aa.cd_player = a.cd_player
        and   qq.cd_category = 40) soma_mcmae, 
        a.cd_player,
        pi.player_name
from   answers a,
	   questions q,
       player_category p,
       player_info pi
where q.question = a.question
and   p.cd_category = q.cd_category
and	  a.cd_player = pi.cd_player
and   a.cd_player = '".$cd_player."'";
	
	
	
	
	$result = mysqli_query($conn, $sql);

	if(mysqli_num_rows($result) > 0){
		while($row = mysqli_fetch_assoc($result)){
			echo $row['soma_sobrevivente'] . "," .$row['soma_explorador']. "," .$row['soma_disney']. "," .$row['soma_mcmae']. "," .$row['cd_player'] ;
		}
	}
	

?>