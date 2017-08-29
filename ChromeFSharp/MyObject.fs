namespace ChromeFSharp

open CefSharp

type MyObject() =
    member val CurrentValue = 0 with get, set
    member val Callback : IJavascriptCallback option = None with get, set

    member this.Register (callback: IJavascriptCallback) =
        this.Callback <- Some callback

    member this.Increment () =
        this.CurrentValue <- this.CurrentValue + 1

        match this.Callback with
        | Some cb ->
            // TODO: do we want to wait for this to finish before returning?
            cb.ExecuteAsync this.CurrentValue
            |> ignore
        | None -> ()
