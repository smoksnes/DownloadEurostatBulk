# DownloadEurostatBulk
Small application for downloading files from Eurostat Bulk


Usage (windows):
* Find application under /compile/
* Run application as such: DownloadEverything.exe _URL_  _FOLDER_
* Replace URL with the URL from EuroStat
    * *Products*: https://ec.europa.eu/eurostat/estat-navtree-portlet-prod/BulkDownloadListing?dir=comext%2FCOMEXT_DATA%2FPREFERENCES
    * *Preferences*: https://ec.europa.eu/eurostat/estat-navtree-portlet-prod/BulkDownloadListing?dir=comext%2FCOMEXT_DATA%2FPRODUCTS
* Replace _FOLDER_ with your local folder where you want to download it.

Example 1:
`DownloadEverything.exe https://ec.europa.eu/eurostat/estat-navtree-portlet-prod/BulkDownloadListing?dir=comext%2FCOMEXT_DATA%2FPRODUCTS C:\YourFolder\Hack2022`
This will download all *Products* and place them in your folder *C:\YourFolder\Hack2022*

Example 2:
`DownloadEverything.exe https://ec.europa.eu/eurostat/estat-navtree-portlet-prod/BulkDownloadListing?dir=comext%2FCOMEXT_DATA%2FPRODUCTS C:\Temp`
This will download all *Preferences* and place them in your folder *C:\Temp*
