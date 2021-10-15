<?php

$files = scandir(".");

foreach($files as $file){
 var_dump($file);
}
$files = scandir("processes");

foreach($files as $file){
 var_dump($file);
}