import React from 'react';
import { useNavigate } from "react-router-dom";
import "./Admin.css";

export function Admin() {
    const register = () => {

    }

    return (
        <div>
            <div class="bg">
                <h1>Felhasználó regisztrálása</h1>
                <fieldset className="input-field">
                    <legend>Felhasználónév:</legend>
                    <input type="text" id="name"></input>
                </fieldset>
                <fieldset className="input-field">
                    <legend>Jelszó:</legend>
                    <input type="text" id="pass"></input>
                </fieldset>
                <select className="list">
                    <option>Raktáros</option>
                    <option>Raktár vezetõ</option>
                    <option>Szakember</option>
                </select><br></br>
                <button onClick={register}>Regisztrálás</button>
            </div>
        </div>
    );
}