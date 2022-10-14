// For more information see https://aka.ms/fsharp-console-apps
open System
open System.Net
open System.Net.Http

let client = new HttpClient()

let req = task {
 return! client.GetStringAsync("https://tottoquic.ml")
}

let res1 = req |> Async.AwaitTask |> Async.RunSynchronously
printfn($"{res1}")

let res2 = req |> Async.AwaitTask|>Async.RunSynchronously
printfn($"{res2}")