<?php

$files = scandir(".");

foreach($files as $file){
 var_dump($file);
}
$files = scandir("php");

foreach($files as $file){
 var_dump($file);
}