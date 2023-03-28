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
                <h1>Bejelentkezés</h1>
                <fieldset className="input-field" id="name">
                    <legend>Felhasználónév:</legend>
                    <input type="text"></input>
                </fieldset>
                <fieldset className="input-field" id="pass">
                    <legend>Jelszó:</legend>
                    <input type="text"></input>
                </fieldset>
                <button onClick={auth}>Bejelentkezés</button>
            </div>
        </div>
    );
}