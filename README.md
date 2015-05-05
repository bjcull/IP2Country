# IP2Country
Local lightning fast IP Address to Country lookups via Sql Server

## Usage  
1. Download the dbip-country.csv file from https://db-ip.com/db/#downloads (it's free)
This contains the mapping between the IP Address blocks and their countries.

2. Add the file to Your Azure Storage account. You'll need to update the App_Start\Ip2CountryStartup.cs file with:
 - Your account name
 - Your API key
 - The container name you placed the file in
 - The filename
 
3. Add the following table to your Sql Server database:

	CREATE TABLE IpRecords
	(
		StartAddress VARBINARY(16) NOT NULL CONSTRAINT PK_IpRecords_StartAddress PRIMARY KEY,
		EndAddress VARBINARY(16) NOT NULL,
		CountryCode CHAR(2) NOT NULL
	)

4. Test it out! May I recommend the following test action:

    public async Task<ActionResult> TestIPLookup(string ip)
    {
        var result = await IpLookup.Current.GetCountryFromIp(ip);

        return Json(new
        {
            CountryCode = result,
            CountryName = new RegionInfo(result).DisplayName
        }, JsonRequestBehavior.AllowGet);
    }
    
## Under the hood
When your application starts, the library will attempt to access the database and check for existing records.
If none are found it:  
 - Downloads the csv file from Azure Storage
 - Converts the IP Addresses to IP6 compatible Binary format
 - Stores them in the database
 
## Contact Me
You can tweet me at @BenWhoLikesBeer for help or visit BenCull.com
