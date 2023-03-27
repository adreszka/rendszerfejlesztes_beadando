import React, { Component } from 'react';
import { useNavigate } from "react-router-dom";
import './Login.css';

export class Login extends Component {
    static displayName = Login.name;

    login = () => {
        useNavigate("/home");
    }

    render() {
        return (
            <div>
                <div className="bg">
                    <h1>Bejelentkezés</h1>
                    <fieldset className="input-field" id="name">
                        <legend>Felhasználónév:</legend>
                        <input type="text"></input>
                    </fieldset>
                    <fieldset className="input-field" id="pass">
                        <legend>Jelszó:</legend>
                        <input type="text"></input>
                    </fieldset>
                    <button onClick={this.login}>Bejelentkezés</button>
                </div>
            </div>
        );
    }
}