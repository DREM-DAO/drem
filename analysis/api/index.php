<?php

$files = scandir($dir = __DIR__ . '/../processes/');

foreach($files as $file){
 $content = file_get_contents($dir.$file);
 $contentzip = gzcompress($content);
 $b64 = base64url_encode(substr($contentzip,2));
 echo "<h2>$file</h2>";
 echo "$b64"
}


function base64url_encode( $data ){
  return rtrim( strtr( base64_encode( $data ), '+/', '-_'), '=');
}