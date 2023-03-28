import React from 'react';
import { useNavigate } from "react-router-dom";
import "./Admin.css";

export function Admin() {
    const register = () => {

    }

    return (
        <div>
            <div class="bg">
                <h1>Felhaszn�l� regisztr�l�sa</h1>
                <fieldset className="input-field">
                    <legend>Felhaszn�l�n�v:</legend>
                    <input type="text" id="name"></input>
                </fieldset>
                <fieldset className="input-field">
                    <legend>Jelsz�:</legend>
                    <input type="text" id="pass"></input>
                </fieldset>
                <select className="list">
                    <option>Rakt�ros</option>
                    <option>Rakt�r vezet�</option>
                    <option>Szakember</option>
                </select><br></br>
                <button onClick={register}>Regisztr�l�s</button>
            </div>
        </div>
    );
}