namespace ChromeFSharp

open System.Windows.Forms
open CefSharp
open CefSharp.WinForms
open System
open System.IO

type MainForm() as this =
    inherit Form()

    let invokeOnUiThread (f : 'a -> 'b) (arg : 'a) : 'b =
        if this.InvokeRequired then
            let delegate' () = f arg
            this.Invoke(Func<'b> delegate') |> unbox
        else
            f arg
    
    do
        this.Width <- 1280
        this.Height <- 1024

        let settings = new CefSettings()
        
#if DEBUG
        // Read from the filesystem in Debug mode to allow refresh without compiling.
        // This assumes that we're running in 'bin/$Configuration', and that the
        // resource output is in the default location.
        let appSource = AppScheme.FileSystem (Path.Combine(Application.StartupPath, "../../../ChromeFSharp.Resource/build"))
#else
        let appSource = AppScheme.Resource ("build/", typeof<ChromeFSharp.Resource.EmptyClass>.Assembly)
#endif

        settings.RegisterScheme <| AppScheme.make appSource

        Cef.EnableHighDPISupport()
        Cef.Initialize(settings, performDependencyCheck=true, browserProcessHandler=null) |> ignore

        let browser = new ChromiumWebBrowser("app://local/index.html")
        browser.RegisterAsyncJsObject("myobject", MyObject())

        this.Controls.Add browser
        browser.Dock <- DockStyle.Fill
        
#if DEBUG
        browser.IsBrowserInitializedChanged.Add (fun e ->
            if e.IsBrowserInitialized then browser.ShowDevTools()
            )
#endif

        // Change the title of the form when the page title changes
        fun (e : TitleChangedEventArgs) ->
            this.Text <- e.Title
        |> invokeOnUiThread
        |> browser.TitleChanged.Add

        // Cleanup on shutdown
        this.FormClosing.Add (fun _ ->
            Cef.Shutdown()
            browser.Dispose()
            settings.Dispose()
            )
