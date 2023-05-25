using System;
using System.Reactive.Subjects;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Publish Subject");
        var subject = new Subject<int>();

        var obs1 = subject.Subscribe(x => Console.WriteLine($"Obs 1: {x}"));

        subject.OnNext(1);
        subject.OnNext(2);

        var obs2 = subject.Subscribe(x => Console.WriteLine($"Obs 2: {x}"));
        subject.OnNext(3);

        obs1.Dispose();
        obs2.Dispose();

        Console.WriteLine("Behavior Subject");
        var behaviorSubject = new BehaviorSubject<int>(1);

        var obs3 = behaviorSubject.Subscribe(x => Console.WriteLine($"Obs 1: {x}"));
        behaviorSubject.OnNext(2);

        var obs4 = behaviorSubject.Subscribe(x => Console.WriteLine($"Obs 2: {x}"));
        behaviorSubject.OnNext(3);
        behaviorSubject.OnCompleted();

        obs3.Dispose();
        obs4.Dispose();

        /*
         * Here's a simplified explanation of how a replay subject works:
           Values are emitted: A replay subject can receive and store values over time. 
           These values can be emitted by a data source or generated within the program.
           Subscribers subscribe: Subscribers can subscribe to the replay subject at any time, 
           even after values have already been emitted.

           Replay of past events: When a new subscriber subscribes to the replay subject, 
           it receives all the previously emitted values in the order they were emitted. 
           This allows late subscribers to access the entire history of events, not just the events that 
           occur after their subscription.

           Continuous stream: After the initial replay of past events, the replay subject continues 
           to behave like a regular subject. It emits new values to all subscribers as they are generated
        */

        Console.WriteLine("Replay Subject");
        var replaySubject = new ReplaySubject<int>(100); //buffer size
        replaySubject.OnNext(1);
        replaySubject.OnNext(2);
        replaySubject.OnNext(3);

        var obs5 = replaySubject.Subscribe(x => Console.WriteLine($"Obs 1: {x}"));
        replaySubject.OnNext(4);

        var obs6 = replaySubject.Subscribe(x => Console.WriteLine($"Obs 2: {x}"));
        replaySubject.OnNext(5);

        obs5.Dispose();
        obs6.Dispose();
    }
}
