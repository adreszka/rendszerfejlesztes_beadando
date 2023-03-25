import React, { Component } from 'react';
import './Login.css'

export class Login extends Component {
    static displayName = Login.name;

    render() {
        return (
            <div>
                <div className="bg">
                    <h1>Bejelentkezés</h1>
                    <fieldset className="input-field">
                        <legend>Felhasználónév:</legend>
                        <input type="text"></input>
                    </fieldset>
                    <fieldset className="input-field">
                        <legend>Jelszó:</legend>
                        <input type="text"></input>
                    </fieldset>
                </div>
            </div>
        );
    }
}