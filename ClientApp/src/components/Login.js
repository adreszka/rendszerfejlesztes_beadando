﻿import React from 'react';
import { useNavigate } from "react-router-dom";
import './Login.css';
import {Client} from "../ApiServices.ts";
import { LoginUser } from '../ApiServices.ts';

export function Login() {

    const navigate = useNavigate();
    const client = new Client();

    const handleLogin = (event) => {
        event.preventDefault();
        const data = new FormData(event.target);

        client.login({
            "username": data.get('name'),
            "password": data.get('pass')
        }).then((val) => {
            console.log(val);
            if(val == "WarehouseManager")
                navigate('/manager');
            if (val == "Administrator") {
                navigate('/admin');
                console.log("adsdf");
            }
            if(val == ".")
                window.alert("Invalid username or password!");
        })
        .catch((error) => { console.log(error) });
    }

    return (
        <div>
            <div className="bg">
                <h1>Login</h1>
                <form onSubmit={handleLogin}>
                    <fieldset className="input-field">
                        <legend>Username:</legend>
                        <input type="text" id="name" name="name"></input>
                    </fieldset>
                    <fieldset className="input-field">
                        <legend>Password:</legend>
                        <input type="text" id="pass" name="pass"></input>
                    </fieldset>
                    <button>Login</button>
                </form>
            </div>
        </div>
    );
}