module App

open Elmish
open Elmish.React
open Feliz
open Feliz.Router

#if DEBUG
open Elmish.Debug
open Elmish.HMR
#endif

type State = { CurrentUrl : string list }
type Msg = UrlChanged of string list

let init() = { CurrentUrl = Router.currentUrl() }
let update (UrlChanged segments) state = { state with CurrentUrl = segments }

let render state dispatch =
    React.router [
        router.onUrlChanged (UrlChanged >> dispatch)
        router.children [
            match state.CurrentUrl with
            | [ ] -> Index.mainElement
            | [ "login" ] -> SignIn.mainElement
            | [ "register" ] -> SignUp.mainElement
            | _ -> Html.h1 "404 Not found"
        ]
    ]

Program.mkSimple init update render

//Program.mkProgram Index.init Index.update Index.view

#if DEBUG
|> Program.withConsoleTrace
#endif
|> Program.withReactSynchronous "elmish-app"
#if DEBUG
|> Program.withDebugger
#endif
|> Program.run