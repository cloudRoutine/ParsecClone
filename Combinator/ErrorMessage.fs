namespace ParsecClone.CombinatorBase

open System
open System.Diagnostics
open System.Collections.Generic

module ErrorMessage =




    type Expected   = string
    type Unexpected = string
    type Message    = string
    type Other      = Object 

    type NestedError =
        {
            Position    : int64
            UserState   : Object
            Messages    : ErrorMessage list
        }                    
    and CompoundError = 
        {
            LabelOfCompound        : String
            NestedErrorPosition    : int64
            NestedErrorUserState   : Object
            NestedErrorMessages    : ErrorMessage list
        } 
    and ErrorMessage =
        | Expected      of Expected                       
        | Unexpected    of Unexpected                     
        | Message       of Message                        
        | NestedError   of NestedError                    
        | CompoundError of CompoundError                  
        | Other         of Other                          


        
    let addErrorMessage ( msg    : ErrorMessage       ) 
                        ( errors : ErrorMessage list  ) =
        match msg with
        | Expected x        ->  msg::errors
        | Unexpected x      ->  msg::errors
        | Message  x        ->  msg::errors
        | NestedError x     ->  let err = { x with Messages = errors }
                                [NestedError err]
        | CompoundError x   ->  let err = { x with NestedErrorMessages = errors }
                                [CompoundError err]
        | Other x           ->  msg::errors


//    type ParserError (position:int64, userState:obj, messages: ErrorMessage list ) =
//        
//        member this.Position    = position
//        member this.UserState   = userState
//        member this.Messages    = messages
//
    type ParserError  =
        struct
            val Position    : int64
            val UserState   : obj
            val Messages    : ErrorMessage list
            new( position:int64, userState:obj, messages:ErrorMessage list ) =
                {
                    Position  = position
                    UserState = userState
                    Messages  = messages
                }
        end

//    type ParserError  =
//        {
//            Position  : int64
//            UserState : obj
//            Messages  : ErrorMessage list
//        }