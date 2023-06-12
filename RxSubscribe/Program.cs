using System;
using System.Reactive.Subjects;
//https://www.c-sharpcorner.com/article/understanding-subject-behaviorsubject-replaysubject/

class Program
{
    //regular subjects - data may be 'lost'
    static void Main(string[] args)
    {
        Console.WriteLine("Publish Subject");

        //topic of discussion / television station / publisher
        //no name. for e.g. cooking, sports
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

        Console.WriteLine("Behavior Subject");
        // BehaviorSubject.
        // 'A' is an initial value. If there is a Subscription 
        // after it, it would immediately get the value 'A'.

        //intial broadcast -- see start screen

        //ignores repeats in Behavior Subject
        var beSubject = new BehaviorSubject<string>("a");

        beSubject.OnNext("a.1");
        beSubject.OnNext("a.1");

        var obj1 = beSubject.Subscribe(value => {
            Console.WriteLine("Receiver 1: " + value, value);
            //set up the subscriber
            //should get first value onwards
        });

        var obj2 = beSubject.Subscribe(value => {
            Console.WriteLine("Receiver 2: " + value, value);
            //set up the subscriber
            //should get first value onwards
        });

        //next values
        beSubject.OnNext("b");
        beSubject.OnNext("c");

       

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

        obj1.Dispose();
        obj2.Dispose();

    }
}
