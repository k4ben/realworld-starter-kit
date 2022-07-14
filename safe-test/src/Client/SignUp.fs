module SignUp

open Elmish
open Fable.Remoting.Client
open Shared
open Feliz
open Nav

type Model = { Todos: Todo list; Input: string }

type Msg =
    | GotTodos of Todo list
    | SetInput of string
    | AddTodo
    | AddedTodo of Todo
    | Reset

let todosApi =
    Remoting.createApi ()
    |> Remoting.withRouteBuilder Route.builder
    |> Remoting.buildProxy<ITodosApi>

let init () : Model * Cmd<Msg> =
    let model = { Todos = []; Input = "" }

    let cmd = Cmd.OfAsync.perform todosApi.getTodos () GotTodos

    model, cmd

let update (msg: Msg) (model: Model) : Model * Cmd<Msg> =
    match msg with
    | GotTodos todos -> { model with Todos = todos }, Cmd.none
    | SetInput value -> { model with Input = value }, Cmd.none
    | AddTodo ->
        let todo = Todo.create model.Input

        let cmd = Cmd.OfAsync.perform todosApi.addTodo todo AddedTodo

        { model with Input = "" }, cmd
    | AddedTodo todo -> { model with Todos = model.Todos @ [ todo ] }, Cmd.none
    | Reset -> { model with Todos = [] }, Cmd.none

let signUp = Html.div [
    prop.classes ["auth-page"]
    prop.children [
        Html.div [
            prop.classes ["container page"]
            prop.children [
                Html.div [
                    prop.classes ["row"]
                    prop.children [
                        Html.div [
                            prop.classes ["col-md-6 offset-md-3 col-xs-12"]
                            prop.children [
                                Html.h1 [
                                    prop.classes ["text-xs-center"]
                                    prop.text "Sign up"
                                ]
                                Html.p [
                                    prop.classes ["text-xs-center"]
                                    prop.children [
                                        Html.a [
                                            prop.href ""
                                            prop.text "Have an account?"
                                        ]
                                    ]
                                ]
                                Html.ul [
                                    prop.classes ["error-messages"]
                                    prop.hidden true
                                    prop.children [
                                        Html.li "That email is already taken"
                                    ]
                                ]
                                Html.form [
                                    Html.fieldSet [
                                        prop.classes ["form-group"]
                                        prop.children [
                                            Html.input [
                                                prop.classes ["form-control form-control-lg"]
                                                prop.type' "text"
                                                prop.placeholder "Your Name"
                                            ]
                                            Html.input [
                                                prop.classes ["form-control form-control-lg"]
                                                prop.type' "text"
                                                prop.placeholder "Email"
                                            ]
                                            Html.input [
                                                prop.classes ["form-control form-control-lg"]
                                                prop.type' "password"
                                                prop.placeholder "Password"
                                            ]
                                            Html.button [
                                                prop.classes ["btn btn-lg btn-primary pull-xs-right"]
                                                prop.text "Sign up"
                                            ]
                                        ]
                                    ]
                                ]
                            ]
                        ]
                    ]
                ]
            ]
        ]
    ]
]

let mainElement = React.fragment [
    nav
    signUp
]

let view (model: Model) (dispatch: Msg -> unit) = mainElement;

