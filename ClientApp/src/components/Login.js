import React from 'react';
import { useNavigate } from "react-router-dom";
import './Login.css';

export function Login() {
    const navigate = useNavigate();

    const auth = () => {
        navigate('/teszt');
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
                <button onClick={auth}>Login</button>
            </div>
        </div>
    );
}