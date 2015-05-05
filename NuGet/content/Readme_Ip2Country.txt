README for Ip2Country.

Before you run your application, add the following table to your database:

CREATE TABLE IpRecords
(
	StartAddress VARBINARY(16) NOT NULL CONSTRAINT PK_IpRecords_StartAddress PRIMARY KEY,
	EndAddress VARBINARY(16) NOT NULL,
	CountryCode CHAR(2) NOT NULL
)

On application start, the Ip2Country library attempts to populate that table from imported data. If the table has any rows, it will skip the import.

Be sure to update the App_Start\Ip2CountryStartup.cs file with your Azure Storage credentials. This is where Ip2Country will look for the seed data csv file. The file itself can be downloaded for free from here: https://db-ip.com/db/#downloads. Just click the download link under the "IP address to country" column.

