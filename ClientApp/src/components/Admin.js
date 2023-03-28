import React from 'react';
import { useNavigate } from "react-router-dom";
import "./Admin.css";

export function Admin() {
    const register = () => {

    }

    return (
        <div>
            <div class="bg">
                <h1>User registration</h1>
                <fieldset className="input-field">
                    <legend>Username:</legend>
                    <input type="text" id="name"></input>
                </fieldset>
                <fieldset className="input-field">
                    <legend>Password:</legend>
                    <input type="text" id="pass"></input>
                </fieldset>
                <select className="list">
                    <option>Warehouse man</option>
                    <option>Warehouse manager</option>
                    <option>Specialist</option>
                </select><br></br>
                <button onClick={register}>Registration</button>
            </div>
        </div>
    );
}