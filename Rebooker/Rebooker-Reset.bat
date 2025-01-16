@Rem The following is a device & app specific sequence of input extracted directly
@Rem from the target device using the android developer & debugging options.
@Rem Chrome tabs
adb shell input tap 870 190
timeout /t 1
@Rem Close current tab
adb shell input tap 980 1610
timeout /t 1
@Rem New tab
adb shell input tap 120 200
timeout /t 1
@Rem Click address
adb shell input tap 520 730
timeout /t 1
@Rem Enter URL
adb shell input text "URL OMITTED FOR PRIVACY REASONS"
@Rem Press enter
adb shell input tap 980 2200
timeout /t 3
@Rem Click field1
adb shell input tap 370 820
@Rem Enter id
adb shell input text "USER OMITTED FOR PRIVACY REASONS"
@Rem Click field2
adb shell input tap 370 1030
@Rem Enter code
adb shell input text "PASSWORD OMITTED FOR PRIVACY REASONS"
@Rem Click login
adb shell input tap 480 1300
timeout /t 2
@Rem Click center
adb shell input tap 505 1160
timeout /t 2
@Rem Click field
adb shell input tap 530 790
@Rem Ctrl+a
adb shell input keyboard keycombination 114 29
@Rem Enter Location
adb shell input text "LOCATION OMITTED FOR PRIVACY REASONS"
@Rem Search
adb shell input tap 540 965
timeout /t 2
@Rem Click Location
adb shell input tap 500 1050
timeout /t 10