module Index

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

let generateTag (text: string) : ReactElement =
    Html.a [
        prop.href "#"
        prop.classes ["tag-pill tag-default"]
        prop.text text
    ]

let homePage = Html.div [
   prop.classes ["home-page"]
   prop.children [
       Html.div [
           prop.classes ["banner"]
           prop.children [
               Html.div [
                   prop.classes ["container"]
                   prop.children [
                       Html.h1 [
                           prop.classes ["logo-font"]
                           prop.text "conduit"
                       ]
                       Html.p "A place to share your knowledge."
                   ]
               ]
           ]
       ]
       Html.div [
           prop.classes ["container page"]
           prop.children [
               Html.div [
                   prop.classes ["row"]
                   prop.children [
                       Html.div [
                           prop.classes ["col-md-9"]
                           prop.children [
                               Html.div [
                                   prop.classes ["feed-toggle"]
                                   prop.children [
                                       Html.ul [
                                           prop.classes ["nav nav-pills outline-active"]
                                           prop.children [
                                                Html.li [
                                                    prop.classes ["nav-item"]
                                                    prop.children [
                                                        Html.a [
                                                            prop.classes ["nav-link disabled"]
                                                            prop.href ""
                                                            prop.text "Your Feed"
                                                            prop.hidden true
                                                        ]
                                                        Html.a [
                                                            prop.classes ["nav-link active"]
                                                            prop.href ""
                                                            prop.text "Global Feed"
                                                        ]
                                                    ]
                                                ]
                                           ]
                                       ]
                                   ]
                               ]
                               Html.div [
                                   prop.classes ["article-preview"]
                                   prop.children [
                                       Html.div [
                                           prop.classes ["article-meta"]
                                           prop.children [
                                               Html.a [
                                                   prop.href ""
                                                   prop.children [
                                                       Html.img [
                                                           prop.src "http://i.imgur.com/Qr71crq.jpg"
                                                       ]
                                                   ]
                                               ]
                                               Html.div [
                                                   prop.classes ["info"]
                                                   prop.children [
                                                       Html.a [
                                                           prop.href ""
                                                           prop.text "Eric Simons"
                                                       ]
                                                       Html.span [
                                                           prop.classes ["date"]
                                                           prop.text "January 20th"
                                                       ]
                                                   ]
                                               ]
                                               Html.button [
                                                   prop.classes ["btn btn-outline-primary btn-sm pull-xs-right"]
                                                   prop.children [
                                                       Html.i [
                                                           prop.classes ["ion-heart"]
                                                       ]
                                                       Html.text " 29"
                                                   ]
                                               ]
                                           ]
                                       ]
                                       Html.a [
                                           prop.href ""
                                           prop.classes ["preview-link"]
                                           prop.children [
                                               Html.h1 "How to build webapps that scale"
                                               Html.p "This is the description for the post."
                                               Html.span "Read more..."
                                           ]
                                       ]
                                   ]
                               ]
                               Html.div [
                                   prop.classes ["article-preview"]
                                   prop.children [
                                       Html.div [
                                           prop.classes ["article-meta"]
                                           prop.children [
                                               Html.a [
                                                   prop.href ""
                                                   prop.children [
                                                       Html.img [
                                                           prop.src "http://i.imgur.com/N4VcUeJ.jpg"
                                                       ]
                                                   ]
                                               ]
                                               Html.div [
                                                   prop.classes ["info"]
                                                   prop.children [
                                                       Html.a [
                                                           prop.href ""
                                                           prop.text "Albert Pai"
                                                       ]
                                                       Html.span [
                                                           prop.classes ["date"]
                                                           prop.text "January 20th"
                                                       ]
                                                   ]
                                               ]
                                               Html.button [
                                                   prop.classes ["btn btn-outline-primary btn-sm pull-xs-right"]
                                                   prop.children [
                                                       Html.i [
                                                           prop.classes ["ion-heart"]
                                                       ]
                                                       Html.text " 32"
                                                   ]
                                               ]
                                           ]
                                       ]
                                       Html.a [
                                           prop.href ""
                                           prop.classes ["preview-link"]
                                           prop.children [
                                               Html.h1 "The song you won't ever stop singing. No matter how hard you try."
                                               Html.p "This is the description for the post."
                                               Html.span "Read more..."
                                           ]
                                       ]
                                   ]
                               ]
                           ]
                       ]
                       Html.div [
                           prop.classes ["col-md-3"]
                           prop.children [
                               Html.div [
                                   prop.classes ["sidebar"]
                                   prop.children [
                                       Html.p "Popular Tags"
                                       Html.div [
                                           prop.classes ["tag-list"]
                                           prop.children [
                                               generateTag("programming")
                                               generateTag("javascript")
                                               generateTag("emberjs")
                                               generateTag("angularjs")
                                               generateTag("react")
                                               generateTag("mean")
                                               generateTag("node")
                                               generateTag("rails")
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

let signIn = Html.div [
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
                                    prop.text "Sign in"
                                ]
                                Html.p [
                                    prop.classes ["text-xs-center"]
                                    prop.children [
                                        Html.a [
                                            prop.href ""
                                            prop.text "Need an account?"
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
                                                prop.placeholder "Email"
                                            ]
                                            Html.input [
                                                prop.classes ["form-control form-control-lg"]
                                                prop.type' "password"
                                                prop.placeholder "Password"
                                            ]
                                            Html.button [
                                                prop.classes ["btn btn-lg btn-primary pull-xs-right"]
                                                prop.text "Sign in"
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
    homePage
]

let view (model: Model) (dispatch: Msg -> unit) = mainElement;