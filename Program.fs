// For more information see https://aka.ms/fsharp-console-apps
open System
open System.Net
open System.Net.Http

let client = new HttpClient()
client.DefaultRequestVersion <- HttpVersion.Version30
client.DefaultVersionPolicy <- HttpVersionPolicy.RequestVersionOrLower

let req = task {
 return! client.GetAsync("https://tottoquic.ml")
}

[1 .. 10]
|> Seq.map (fun n -> task {
    let! res = req
    return n, res
})
|> Seq.map Async.AwaitTask
|> Async.Parallel
|> Async.RunSynchronously
|> Array.map (fun (n, res) -> printfn($"{n} H:{res.Version} S:{res.StatusCode}"))
|> ignore