import React from 'react';
import { useNavigate } from "react-router-dom";
import './Login.css';
import {Client} from "../ApiServices.ts";
import { LoginUser } from '../ApiServices.ts';

export function Login() {
    const navigate = useNavigate();
    const client = new Client();
    const login = () => {
        console.log("asd")
        client.login({"username":"admin","password":"admin"}).then((val) => {
            console.log(val);
            if(val != "")
                navigate('/teszt');
        })
        .catch((err) => console.log(err));;
    
    }

    return (
        <div>
            <div className="bg">
                <h1>Login</h1>
                <form onSubmit={login}>
                    <fieldset className="input-field">
                        <legend>Username:</legend>
                        <input type="text" id="name"></input>
                    </fieldset>
                    <fieldset className="input-field">
                        <legend>Password:</legend>
                        <input type="text" id="pass"></input>
                    </fieldset>
                    <button>Login</button>
                </form>
            </div>
        </div>
    );
}