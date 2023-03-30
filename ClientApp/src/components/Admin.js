import React from 'react';
import { useNavigate } from "react-router-dom";
import "./Admin.css";
import { Client } from "../ApiServices.ts";

export function Admin() {
    var client = new Client();

    const register = (event) => {
        event.preventDefault();
        const data = new FormData(event.target);

        client.user({
            "username": data.get('name'),
            "password": data.get('pass'),
            "role": data.get('list')
        }).then((val) => {
            if (val == true) {
                window.alert("Successful registration");
            } else {
                window.alert("The username is already in use!");
            }
        })
        .catch((error) => { console.log(error) });
    }

    return (
        <div>
            <div className="bg">
                <h1>User registration</h1>
                <form onSubmit={register}>
                    <fieldset className="input-field">
                        <legend>Username:</legend>
                        <input type="text" id="name" name="pass"></input>
                    </fieldset>
                    <fieldset className="input-field">
                        <legend>Password:</legend>
                        <input type="text" id="pass" name="pass"></input>
                    </fieldset>
                    <select className="list" id="list" name="list">
                        <option value="Warehouseman">Warehouse man</option>
                        <option value="WarehouseManager">Warehouse manager</option>
                        <option value="Specialist">Specialist</option>
                    </select><br></br>
                    <button>Registration</button>
                </form>
            </div>
        </div>
    );
}