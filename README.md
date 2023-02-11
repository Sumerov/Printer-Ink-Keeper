# About
Are you printing once in a year and your Ink cartridges are getting dried out often? This app will prevent this!
By printing once a week or more often your Ink nozzle will get wet again and your cartridge will not dry out. 

I was using printer like 2 times in a year, but approximately after 6 months from purchasing Ink I´ve got dried out. Well 17€ per few pages is little bit expensive. So I came with this solution: by leaving negligible amount of ink per weak and a paper wasted I am able to use cartridge for another year :) 

## How it works?
The intension is to run app on windows startup. The app will create config file in which the date, interval (in days) and printer app path is stored. 

## Installation
1. Adobe acrobat reader is tested as printing app. However any other printing app can be used. Only what is needed is a path to the exe file.
2. Copy all files from bin/Debug/netcoreapp3.1 into startup folder (C:\Users\[USERNAME]\AppData\Roaming\Microsoft\Windows\Start Menu\Programs\Startup)
3. Run InkKeeper.exe for the first time. The config.cfg file will be generated for the first time. 
4. Open config.cfg and update intervals, printer app path and if you want to print immediately update the date to the past.

## buy me a coffee
https://www.patreon.com/sumikus