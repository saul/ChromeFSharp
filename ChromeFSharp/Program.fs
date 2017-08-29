namespace ChromeFSharp

open System
open System.Windows.Forms
        
module Program =
    
    [<EntryPoint>]
    [<STAThread>]
    let main argv = 
        Application.EnableVisualStyles()
        Application.SetCompatibleTextRenderingDefault false
 
        use form = new MainForm()
 
        Application.Run form
        0
