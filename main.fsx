open System.Threading.Tasks
open System
[1 .. 10]
|> Seq.map (fun n -> task { 
    do! Task.Delay(1000 / n)
    printfn($"{n}");
    return n})
|> Seq.map Async.AwaitTask
|> Async.Parallel
|> Async.RunSynchronously
|> Array.map (fun n -> printfn($"{n}"))