# Fable Elasticsearch Demo

Saw this blog post about PureScript where the sample code implemented an Elasticsearch API to delete outdated indices:
http://blog.leifbattermann.de/2020/01/26/purescript-case-study-and-guide-for-newcomers/

I was curious of how this sample would translate into F# code, and it turned out to be quite elegant!

## Requirements

* [dotnet SDK](https://www.microsoft.com/net/download/core) 3.0 or higher
* [node.js](https://nodejs.org) with [npm](https://www.npmjs.com/)
* An F# editor like Visual Studio, Visual Studio Code with [Ionide](http://ionide.io/) or [JetBrains Rider](https://www.jetbrains.com/rider/).

## Building and running the app

* Install JS dependencies: `npm install`
* Go into test: `dotnet build`
* Go to root folder: `npm run test`
