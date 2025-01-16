using System.IO;
using System;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Diagnostics;
using System.Media;

namespace Nem.Automation;
class Rebooker
{
    static string version = "1.1";
    static string clipboard, batchAddress, resetAddress, startAddress;
    static int interval = 44000, maxSessionDuration = 800000, currentSessionTime = 0, lastEvent = -1, batchExecutionTime = 10000;
    static int sessionCount = 0;
    static float intervalRMulti = 1.5f;

    const int EVENT_BOOKING = 1, EVENT_CAPTCHA = 2, EVENT_FAILED = 3, EVENT_BASIC = 4;

    public static void Main(string[] args)
    {
        // Each file corresponds to a sequence of actions on the android device
        // that are executed via ADB through the windows command line

        // The typical sequence to search for a booking
        batchAddress = "/c " + Directory.GetCurrentDirectory() + "\\Rebooker.bat";
        // The typical sequence to reset to to the starting web page
        resetAddress = "/c " + Directory.GetCurrentDirectory() + "\\Rebooker-Reset.bat";

        startAddress = "/c " + Directory.GetCurrentDirectory() + "\\RUN Rebooker.bat";

        clipboard = Directory.GetCurrentDirectory() + "\\log.txt";
        Console.WriteLine("Welcome to rebooker " + version);

        Process reset = Process.Start("CMD.exe", resetAddress);
        Thread.Sleep(20000);
        reset.Kill();
        while (true)
        {
            if (currentSessionTime > maxSessionDuration)
            {
                //Avoid getting flagged due to session length
                Console.WriteLine("!! WARNING: Session too long");
                SystemSounds.Exclamation.Play();
                Thread.Sleep(5000);
                break;
            }
            else
            {
                Console.Clear();
                Iterate();
                WaitProgress();
            }
        }
        Process start = Process.Start("CMD.exe", startAddress);
        Thread.Sleep(5000);
        start.Kill();
    }

    private static void WaitProgress()
    {
        //Introducing randomisation to the sequence to avoid getting flagged
        int targetInterval = interval;
        var rand = new Random();
        targetInterval += rand.Next((int)((intervalRMulti - 1f) * interval) + 1);
        int progress = 0;
        while (progress < targetInterval)
        {
            Console.Clear();
            Console.WriteLine("Last event: ");
            PrintStatus(lastEvent);
            Console.WriteLine("Next attempt in " + (targetInterval - progress) / 1000);
            Console.WriteLine("Current Session Time: " + currentSessionTime / 1000);
            Console.WriteLine("Max Session Time: " + maxSessionDuration / 1000);
            Console.WriteLine("Session Count: " + sessionCount);
            Thread.Sleep(1000);
            progress += 1000;
            currentSessionTime += 1000;
        }
    }

    private static void Iterate()
    {
        File.Create(clipboard).Close();
        //? Run adb commands from batch
        Console.WriteLine(batchAddress);
        Process batch = Process.Start("CMD.exe", batchAddress);
        Thread.Sleep(batchExecutionTime);
        currentSessionTime += batchExecutionTime;
        batch.Kill();

        // Read the android clipboard data populated in the local file via ADB & Clipper
        StreamReader sr = new StreamReader(clipboard);
        string pageContent = sr.ReadToEnd();
        sr.Close();

        switch (ParsePage(pageContent))
        {
            case EVENT_BOOKING:
                lastEvent = EVENT_BOOKING;
                PrintStatus(EVENT_BOOKING);
                Execute_Booking();
                break;

            case EVENT_CAPTCHA:
                lastEvent = EVENT_CAPTCHA;
                PrintStatus(EVENT_CAPTCHA);
                Execute_Captcha();
                break;

            case EVENT_FAILED:
                lastEvent = EVENT_FAILED;
                PrintStatus(EVENT_FAILED);
                Execute_Failed();
                break;

            case -1:
                lastEvent = EVENT_BASIC;
                PrintStatus(EVENT_BASIC);
                break;
        }
    }

    private static int ParsePage(string content)
    {
        if (content.Contains("result=0"))
        {
            return EVENT_FAILED;
        }
        else if (content.Contains("Incapsula"))
        {
            return EVENT_CAPTCHA;
        }
        // Edit to which months are desired for the booking
        else if (content.Contains("invalid1") ||
                 content.Contains("invalid2") ||
                 content.Contains("November"))
        {
            return EVENT_BOOKING;
        }
        return -1;
    }

    private static void Execute_Booking()
    {
        while (true)
        {
            RingAlarm(EVENT_BOOKING);
            Thread.Sleep(4000);
            currentSessionTime += 4000;
        }
    }
    private static void Execute_Captcha()
    {
        while (true)
        {
            RingAlarm(EVENT_CAPTCHA);
            currentSessionTime += 2000;
            Thread.Sleep(2000);
        }
    }
    private static void Execute_Failed()
    {
        for (int i = 0; i < 4; i++)
        {
            RingAlarm(EVENT_FAILED);
            currentSessionTime += 2000;
            Thread.Sleep(2000);
        }
    }

    private static void PrintStatus(int e)
    {
        switch (e)
        {
            case EVENT_BOOKING:
                Console.WriteLine("\n------------------------" +
                                  "\n------BOOKING FOUND-----\n" +
                                  "------------------------\n");
                break;
            case EVENT_CAPTCHA:
                Console.WriteLine("\n------------------------" +
                                  "\n------Captcha found-----\n" +
                                  "------------------------\n");
                break;
            case EVENT_FAILED:
                Console.WriteLine("\n------------------------" +
                                  "\n--Error retrieving clip-\n" +
                                  "------------------------\n");
                break;
            case EVENT_BASIC:
                Console.WriteLine("\n------------------------" +
                                  "\n-------Basic page-------\n" +
                                  "------------------------\n");
                break;
        }
    }

    private static void RingAlarm(int type)
    {
        switch (type)
        {
            case EVENT_BOOKING:
                //todo
                System.Media.SoundPlayer player = new System.Media.SoundPlayer(Directory.GetCurrentDirectory() + "\\BOOKING-ALARM.wav");
                player.Play();
                break;
            case EVENT_CAPTCHA:
                SystemSounds.Hand.Play();
                //todo
                break;
            case EVENT_FAILED:
                SystemSounds.Exclamation.Play();
                //todo
                break;
        }
    }
}


