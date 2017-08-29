# ChromeFSharp

A basic example of using CefSharp to allow interaction between a React frontend and an F#/.NET backend.

## Prerequisites

* Node.js for compiling the front-end
* Visual Studio with F# for compiling the back-end

## Getting Started

1. In the `ChromeFSharp.Resource` directory, run: `npm install`. This will install the dependencies of the front-end.
2. Once the dependencies have been installed, run: `npm run build`. This will compile the front-end into an optimized HTML app in `ChromeFSharp.Resource/build`.
3. Build `ChromeFSharp.sln`. This will produce `ChromeFSharp.exe` that will open the `index.html` of the front-end on launch.
