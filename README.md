# Traffic Test Project

This project is a windows forms C# project that runs on a factory line in Hendersonville Lamp Plant.

Equipment controlled:
 
- ITECH IT 7321 AC power supply (USB Interface)
	- Init
	- Get/Set Volts
	- Get/Set Frequency
	- Power On
	- Power Off

- ITECH IT6922A DC Power supply (USB Interface)
	- Init
	- Get/Set Volts
	- Get/Set Amps
	- Power On
	- Power Off

- NPA101 Power Analyzer (USB Interface)
	- Init
	- Set Mode (AC or DC)
	- Get Volts
	- Get Amps
	- Get Watts
	- Get Power Factor
	- Get Frequency

- StellarNet BlueWave Spectrometer
	- Get Spectrum Reading (380nm-780nm)
	- Autorange
	- Set Range
	- Init

- USB Temperature Sensor
	- Get Temp


The application will have a simpler user interface where the user can set the voltage to run the lamp at, select AC or DC and then start the test. 
Once the test is started, the application will:
1. Close the light curtain
2. ramp up the voltage to the set value over a period of time
3. then take a reading of the lamp using the spectrometer.

Once the reading is complete, it will ramp the voltage back down, open the light curtain and complete the test.

Readings will be stored in excel and will also be sent to an SQL server database via simple SQL calls