using System;
using System.Reactive.Subjects;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Publish Subject");

        //topic of discussion / television station / publisher
        //no name. for e.g. cooking, sports
        //just an int
        var subject = new Subject<string>();

        //watching - subscriber (observation)
        var obs1 = subject.Subscribe(x => Console.WriteLine($"Kevin: {x}"));

        subject.OnNext("apple");
        subject.OnNext("orange"); 

        // second sunscriber
        var obs2 = subject.Subscribe(x => Console.WriteLine($"Mei: {x}"));
        subject.OnNext("grapefruit"); 

        obs1.Dispose();
        obs2.Dispose();

        // BehaviorSubject.
        // 'A' is an initial value. If there is a Subscription 
        // after it, it would immediately get the value 'A'.

        //intial broadcast -- see start screen
        var beSubject = new BehaviorSubject<string>("a");

        beSubject.Subscribe(value => {
            Console.WriteLine("Subscription received the value " + value, value);

            // Subscription received B. It would not happen
            // for an Observable or Subject by default.
        });

        beSubject.OnNext("b");

        beSubject.OnNext("c");
        // Subscription received C.

        beSubject.OnNext("d");
        // Subscription received D.

        beSubject.Dispose();
        //obs4.Dispose();

        // replay subject
        // like a tv program replayed

        Console.WriteLine("Replay Subject");
        var replaySubject = new ReplaySubject<string>(100); //buffer size
        replaySubject.OnNext("episode 1");
        replaySubject.OnNext("episode 2");
        replaySubject.OnNext("episode 3");

        //these 1st three were already played
        var obs5 = replaySubject.Subscribe(x => Console.WriteLine($"Kevin: {x}"));
        replaySubject.OnNext("episode 4");

        var obs6 = replaySubject.Subscribe(x => Console.WriteLine($"Mei: {x}"));
        replaySubject.OnNext("episode 5");

        obs5.Dispose();
        obs6.Dispose();

        

    }
}
