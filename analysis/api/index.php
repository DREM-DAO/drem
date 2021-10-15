<?php

$files = scandir(__DIR__ . '/../processes/');

foreach($files as $file){
 var_dump($file);
}
