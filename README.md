# AutoBookingFinder
### *Needs refactoring (Indefinitely postponed as project is not currently in use)
* C# .net
* Android Debug Bridge (ADB)
* Command Line (CMD)

## Purpose
**The aim of this app was to automate the process of checking through a specific booking website for available tests, and alerting the user ASAP if one was found.
This had to be done constantly as the tests were in high demand and would only become available for seconds before being booked by someone.**

## Implementation
My PC's MAC address was banned from the website for no apparent reason even before this project, having the option to either spoof the MAC address or come up 
with some creative alternative, I chose the latter. I decided to automate the process on my phone via the PC & .net, C#.

After connecting my android phone with debugging enabled:
1. Used ADB to send input directly to my phone with command line prompts, the sequence of checking for avaiable tests on the target website.
2. Copied the final web page text to clipboard, and sent this data back to PC via the android app Clipper and ADB.
3. Assessed the contents to determine results & proceed accordingly; e.g. if a test booking was found, play an alert sound, if not repeat the process.
4. Experimented with automatic repetition time randomistions and session length constraints to avoid getting flagged.

**Above steps were automated via a C# .net program (Rebooker.cs)**

## Final thoughts
I realise this methodology was not the most optimal nor the most fail-safe for achieving the required result, however,
it was the quickest to implement with my knowledge at the time. Since I needed a booking from the target website ASAP, 
time efficiency took priority over optimisation and perfection. In the end this app succeeded in alerting me of an available booking that popped up.


