module Nav

open Feliz

//type NavPage = Home | SignIn | SignUp

let nav = Html.nav [
    prop.classes ["navbar"; "navbar-light"]
    prop.children [
        Html.div [
            prop.classes ["container"]
            prop.children [
                Html.a [
                    prop.classes ["navbar-brand"]
                    prop.href "index.html"
                    prop.text "conduit"
                ]
                Html.ul [
                    prop.classes ["nav"; "navbar-nav"; "pull-xs-right"]
                    prop.children [
                        Html.li [
                            prop.classes ["nav-item"]
                            prop.children [
                                Html.a [
                                   prop.classes ["nav-link"; "active"]//(if page == Home then ["nav-link"; "active"] else ["nav-link"])
                                   prop.href "/#/"
                                   prop.text "Home"
                                ]
                            ]
                        ]
                        Html.li [
                            prop.classes ["nav-item"]
                            prop.children [
                                Html.a [
                                   prop.classes ["nav-link"]
                                   prop.href "/#/login"
                                   prop.text "Sign in"
                                ]
                            ]
                        ]
                        Html.li [
                            prop.classes ["nav-item"]
                            prop.children [
                                Html.a [
                                   prop.classes ["nav-link"]
                                   prop.href "/#/register"
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