@Rem The following is a device & app specific sequence of input extracted directly
@Rem from the target device using the android developer & debugging options.
@REM Press back
adb shell input tap 780 2325
timeout /t 2
@REM Click test centre
adb shell input tap 300 1050
timeout /t 2
@REM CTRL+A , CTRL+C
adb shell input keyboard keycombination 114 29
adb shell input keyboard keycombination 114 31
@REM Switch to clipper, an android app that allows retrieving the clipboard of the android device via the PC
adb shell input tap 320 2335
timeout /t 1
adb shell input tap 800 1330
timeout /t 1
@REM Copy and save the android clipboard to the PC for access
adb shell am broadcast -a clipper.get > "C:\platform-tools\log.txt"
timeout /t 1
@REM switch back
adb shell input tap 320 2335
timeout /t 1
adb shell input tap 800 1330
timeout /t 1