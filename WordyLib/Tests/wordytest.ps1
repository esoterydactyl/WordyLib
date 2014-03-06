import-module C:\Users\david\Documents\GitHub\WordyLib\WordyLib\bin\Debug\WordyLib.dll
$url = "http://www.cnn.com"

[WordyLib.Wordy]::getKeywords($url)


start-sleep 5

[WordyLib.WordyStats]::getKeywordStats($url)