import React from 'react';
import { useNavigate } from "react-router-dom";
import './Login.css';
import Client from "../ApiServices.ts";
import { LoginUser } from '../ApiServices';

export function Login() {
    const navigate = useNavigate();

    const lpogin = () => {
        navigate('/teszt');
        user = new LoginUser();
        user.username = "";
        user.password = "";
        Client.login()
    }

    return (
        <div>
            <div className="bg">
                <h1>Login</h1>
                <fieldset className="input-field">
                    <legend>Username:</legend>
                    <input type="text" id="name"></input>
                </fieldset>
                <fieldset className="input-field">
                    <legend>Password:</legend>
                    <input type="text" id="pass"></input>
                </fieldset>
                <button onClick={login}>Login</button>
            </div>
        </div>
    );
}