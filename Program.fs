// For more information see https://aka.ms/fsharp-console-apps
open System
open System.Net
open System.Net.Http

for i = 1 to 10 do
  use client = new HttpClient()
  client.DefaultRequestVersion <- HttpVersion.Version20
  // client.DefaultRequestVersion <- HttpVersion.Version30
  client.DefaultVersionPolicy <- HttpVersionPolicy.RequestVersionExact

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
  |> Array.map (fun (n, res) -> printfn($"{n} V:{res.Version} S:{res.StatusCode}"))
  |> ignore
