<?php

$files = scandir("..");

foreach($files as $file){
 var_dump($file);
}