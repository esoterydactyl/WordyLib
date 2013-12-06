$path = "C:\Users\david\Documents\GitHub\WordyLib\WordyLib\bin\Debug\WordyLib.dll"
$HTMLPath = "C:\Users\david\Documents\GitHub\WordyLib\WordyLib\bin\Debug\HtmlAgilityPack.dll"
Add-Type -Path $path
Add-Type -Path $HTMLPath

$url = "http://www.dice.com/job/result/10123970/13164?src=19&q=uae"

$webText = [WordyLib.SimpleScraper]::GetWeb($url) 
$CleanWebText = [WordyLib.SimpleParser]::stripHTML($webText)
$FilteredWebText = [WordyLib.SimpleCleaner]::degreaseString($CleanWebText)

$SimplifiedScrape = [WordyLib.Wordy]::getKeyWords($url)